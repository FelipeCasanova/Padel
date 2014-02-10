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
        private readonly IUsuarioTasks usuarioTasks;
        private readonly IRepository<Equipo> equipoRepository;
        private readonly IRepository<Categoria> categoriaRepository;
        private readonly IRepository<EquipoToCategoria> equipoToCategoriaRepository;

        public CrearEquipoParaTorneoCommandHandler(IUsuarioTasks usuarioTasks, IRepository<Categoria> categoriaRepository, IRepository<Equipo> equipoRepository,
            IRepository<EquipoToCategoria> equipoToCategoriaRepository)
        {
            this.usuarioTasks = usuarioTasks;
            this.categoriaRepository = categoriaRepository;
            this.equipoRepository = equipoRepository;
            this.equipoToCategoriaRepository = equipoToCategoriaRepository;
        }

        public CommandResult Handle(CrearEquipoParaTorneoCommand command)
        {
            EquipoToCategoria equipoToCategoria = new EquipoToCategoria();

            // Preconditions
            if (command.UsuarioId == command.ParejaId)
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

            if (command.ParejaId == 0 && command.EquipoId == 0)
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
                var equipo = this.equipoRepository.Get(command.EquipoId);

                // Activar jugador y equipo si se diera el caso
                if (equipo.JugadorA.Id == command.UsuarioId && !equipo.JugadorAVerificado)
                {
                    equipo.JugadorAVerificado = true;
                    this.equipoRepository.SaveOrUpdate(equipo);
                }

                if (equipo.JugadorB.Id == command.UsuarioId && !equipo.JugadorBVerificado)
                {
                    equipo.JugadorBVerificado = true;
                    this.equipoRepository.SaveOrUpdate(equipo);
                }

                if (equipo.JugadorAVerificado && equipo.JugadorBVerificado)
                {
                    equipo.Estado = EstadoEquipoEnum.Activado;
                    this.equipoRepository.SaveOrUpdate(equipo);
                }

                // Verificar si el equipo está ya registrado
                if (categoria.EquiposToCategorias.Any(etc => etc.Equipo.Id == command.EquipoId))
                {
                    return new CommandResult(false, "El equipo seleccionado ya está registrado en este torneo.");
                }
                equipoToCategoria.Equipo = equipo;
            }
            else
            {
                if (command.equipos.Any(e => (e.JugadorA.Id == command.UsuarioId || e.JugadorB.Id == command.UsuarioId)
                    && (e.JugadorA.Id == command.ParejaId || e.JugadorB.Id == command.ParejaId) && e.Estado == EstadoEquipoEnum.Activado))
                {
                    return new CommandResult(false, "El equipo que quieres crear ya esta creado. Selecciona uno de la lista de equipos.");
                }

                if (categoria.EquiposToCategorias.Any(etc => etc.Equipo.JugadorA.Id == command.UsuarioId || etc.Equipo.JugadorB.Id == command.UsuarioId))
                {
                    return new CommandResult(false, "Ya estás registrado en este torneo.");
                }

                var equipo = new Equipo();
                equipo.FechaCreacion = DateTime.Now;
                equipo.FechaModificacion = DateTime.Now;
                equipo.Estado = EstadoEquipoEnum.Desactivado;
                equipo.Nombre = null;
                equipo.JugadorA = this.usuarioTasks.Get(command.UsuarioId);
                equipo.JugadorB = this.usuarioTasks.Get(command.ParejaId);

                if (equipo.JugadorA.Sexo == SexoEnum.Hombre && equipo.JugadorB.Sexo == SexoEnum.Hombre)
                {
                    equipo.TipoEquipo = TipoEquipoEnum.Hombre;
                }
                else if (equipo.JugadorA.Sexo == SexoEnum.Mujer && equipo.JugadorB.Sexo == SexoEnum.Mujer)
                {
                    equipo.TipoEquipo = TipoEquipoEnum.Mujer;
                }
                else
                {
                    equipo.TipoEquipo = TipoEquipoEnum.Mixto;
                }

                equipo.JugadorAVerificado = true;
                equipo.JugadorBVerificado = false;
                this.equipoRepository.SaveOrUpdate(equipo);

                equipoToCategoria.Equipo = equipo;
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
