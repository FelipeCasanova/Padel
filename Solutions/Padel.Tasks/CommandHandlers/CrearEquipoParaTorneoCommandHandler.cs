using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using Padel.Tasks.Commands;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Web;
using Padel.Infrastructure.Utilities;
using Padel.Domain.Contracts.Tasks;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.CommandHandlers
{
    public class CrearEquipoParaTorneoCommandHandler : ICommandHandler<CrearEquipoParaTorneoCommand, CommandResult>
    {
        private readonly IEquipoTasks equipoTasks;

        private readonly IRepository<Usuario> usuarioRepository;
        private readonly IRepository<Categoria> categoriaRepository;
        private readonly IRepository<EquipoToCategoria> equipoToCategoriaRepository;

        public CrearEquipoParaTorneoCommandHandler(IEquipoTasks equipoTasks, IRepository<Usuario> usuarioRepository, IRepository<Categoria> categoriaRepository, 
            IRepository<EquipoToCategoria> equipoToCategoriaRepository)
        {
            this.equipoTasks = equipoTasks;

            this.usuarioRepository = usuarioRepository;
            this.categoriaRepository = categoriaRepository;
            this.equipoToCategoriaRepository = equipoToCategoriaRepository;
        }

        public CommandResult Handle(CrearEquipoParaTorneoCommand command)
        {
            EquipoToCategoria equipoToCategoria = new EquipoToCategoria();

            // Preconditions
            if (command.Jugador1Id == command.Jugador2Id)
            {
                return new CommandResult(false, "No se ha podido registrar el equipo. Inténtalo más tarde.");
            }

            if (command.CategoriaId == 0)
            {
                return new CommandResult(false, "No se puedo registrar el equipo en la categoría. Inténtalo más tarde.");
            }

            if (command.TorneoId == 0)
            {
                return new CommandResult(false, "No se puedo registrar el equipo en el torneo. Inténtalo más tarde.");
            }

            if (command.Jugador2Id == 0 && command.EquipoId == 0)
            {
                return new CommandResult(false, "Debes elegir o crear un equipo.");
            }

            // Comprobar que la categoria pertenece al torneo
            var categoria = this.categoriaRepository.Get(command.CategoriaId);
            if (categoria.Torneo.Id != command.TorneoId)
            {
                return new CommandResult(false, "No se puedo registrar el equipo en el torneo. Inténtalo más tarde.");
            }
            equipoToCategoria.Categoria = categoria;

            if (command.EquipoId != 0)
            {
                var equipo = this.equipoTasks.Get(command.EquipoId);

                // Activar jugador y equipo si se diera el caso
                this.equipoTasks.CreateOrUpdate(equipo, command.Jugador1Id, command.Jugador2Id);
                
                // Verificar si el equipo está ya registrado
                if (categoria.EquiposToCategorias.Any(etc => etc.Equipo.Id == command.EquipoId))
                {
                    return new CommandResult(false, "El equipo seleccionado ya está registrado en este torneo.");
                }
                equipoToCategoria.Equipo = equipo;
            }
            else
            {
                var equipos = this.equipoTasks.GetEquiposPorJugadoresList(command.Jugador1Id, command.Jugador2Id);

                if (equipos.Any())
                {
                    var equipoReactivate = equipos.First();

                    // Reactivar el equipo
                    this.equipoTasks.CreateOrUpdate(equipoReactivate, command.Jugador1Id, command.Jugador2Id);
                    return new CommandResult(true, "Se ha registrado correctamente en el torneo.");
                }

                if (categoria.EquiposToCategorias.Any(etc => etc.Equipo.JugadorA.Id == command.Jugador1Id || etc.Equipo.JugadorB.Id == command.Jugador1Id))
                {
                    return new CommandResult(false, "Ya estás registrado en este torneo.");
                }

                var equipo = new Equipo();
                equipoToCategoria.Equipo = this.equipoTasks.CreateOrUpdate(equipo, command.Jugador1Id, command.Jugador2Id);
            }

            equipoToCategoria.FechaCreacion = DateTime.Now;
            equipoToCategoria.FechaModificacion = DateTime.Now;

            // Actions
            if (equipoToCategoria.IsValid())
            {
                this.equipoToCategoriaRepository.SaveOrUpdate(equipoToCategoria);
                return new CommandResult(true, "Se ha registrado correctamente en el torneo.");
            }
            else
            {
                return new CommandResult(false, "No se puedo registrar el equipo en el torneo. Intentelo más tarde.");
            }
        }
    }

}
