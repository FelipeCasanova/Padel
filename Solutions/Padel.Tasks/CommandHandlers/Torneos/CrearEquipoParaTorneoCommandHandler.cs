using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Web;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Tasks.Commands.Torneos;
using SharpArch.Domain.PersistenceSupport;
using System.Threading;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Padel.Tasks.CommandHandlers.Torneos
{
    public class CrearEquipoParaTorneoCommandHandler : IRequestHandler<CrearEquipoParaTorneoCommand, CommandResult>
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

        private CommandResult PrecondicionJugadorYaRegistrado(int jugadorId, Categoria categoria, bool success, string mensaje)
        {
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            var equiposJugador = categoria.EquiposToCategorias.Where(etc => (etc.Equipo.JugadorA.Id == jugadorId || etc.Equipo.JugadorB.Id == jugadorId)
                && etc.Estado != EstadoEquipoCategoriaEnum.Eliminado);
            if (equiposJugador.Any())
            {
                if (success)
                {
                    this.equipoTasks.CreateOrUpdate(equiposJugador.First().Equipo, principal.Id);
                }
                else
                {
                    //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                }
                return new CommandResult(success, mensaje);
            }
            return null;
        }

        private CommandResult PrecondicionJugadorEnRangoDeExperiencia(int jugadorId, Categoria categoria)
        {
            var jugador = this.usuarioRepository.Get(jugadorId);
            if (jugador.PuntosExperiencia < categoria.NivelMinExp || jugador.PuntosExperiencia > categoria.NivelMaxExp)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "Lo sentimos mucho pero este torneo solo admite jugadores entre " + categoria.NivelMinExp
                    + " Exp y " + categoria.NivelMaxExp + "Exp.");
            }
            return null;
        }

        private CommandResult PrecondicionEquipoEnCategoria(Equipo equipo, Categoria categoria)
        {
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            if (categoria.EquiposToCategorias.Any(etc => etc.Equipo.Id == equipo.Id
                    && etc.Estado != EstadoEquipoCategoriaEnum.Eliminado))
            {
                // Activar al usuario en el equipo si se diera el caso
                this.equipoTasks.CreateOrUpdate(equipo, principal.Id);
                return new CommandResult(true, "El equipo seleccionado ya está registrado en este torneo.");
            }
            return null;
        }

        private CommandResult PrecondicionEquipoTipoEnCategoriaTipo(Equipo equipo, Categoria categoria)
        {
            if (categoria.TipoEquipo != equipo.TipoEquipo)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "El equipo seleccionado debe ser de tipo '" + categoria.TipoEquipo.ToString() + "'.");
            }
            return null;
        }

        public CommandResult Handle(CrearEquipoParaTorneoCommand command)
        {
            CommandResult result = null;
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;

            EquipoToCategoria equipoToCategoria = new EquipoToCategoria();

            // Preconditions
            if (command.Jugador1Id == command.Jugador2Id)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se ha podido registrar el equipo. Inténtalo más tarde.");
            }

            // La categoria existe
            if (command.CategoriaId == 0)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se puedo registrar el equipo en la categoría. Inténtalo más tarde.");
            }

            // El torneo existe
            if (command.TorneoId == 0)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se puedo registrar el equipo en el torneo. Inténtalo más tarde.");
            }

            // Se ha elegido un compañero o un equipo
            if (command.Jugador2Id == 0 && command.EquipoId == 0)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "Debes elegir o crear un equipo.");
            }

            // Comprobar que la categoria pertenece al torneo
            var categoria = this.categoriaRepository.Get(command.CategoriaId);
            if (categoria.Torneo.Id != command.TorneoId)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se puedo registrar el equipo en el torneo. Inténtalo más tarde.");
            }

            // Comprobar si el usuario ya está en el torneo
            if ((result = PrecondicionJugadorYaRegistrado(command.Jugador1Id, categoria, true, "Ya estás registrado en este torneo.")) != null) { return result; }

            // Comprobar que el usuario entra dentro del rango de la experiencia
            if ((result = PrecondicionJugadorEnRangoDeExperiencia(command.Jugador1Id, categoria)) != null) { return result; }

            equipoToCategoria.Categoria = categoria;

            if (command.EquipoId != 0)
            {
                var equipo = this.equipoTasks.Get(command.EquipoId);

                // Verificar si el equipo está ya registrado
                if ((result = PrecondicionEquipoEnCategoria(equipo, categoria)) != null) { return result; }
                
                var jugador2Id = equipo.JugadorB.Id;
                if (command.Jugador1Id == jugador2Id)
                {// El jugador 2 es el otro
                    jugador2Id = equipo.JugadorA.Id;
                }

                // Comprobar si el compañero ya está en el torneo
                if ((result = PrecondicionJugadorYaRegistrado(jugador2Id, categoria, false, "Su compañero ya estás registrado en este torneo.")) != null) { return result; }

                // Comprobar que el jugador2 entra dentro del rango de la experiencia
                if ((result = PrecondicionJugadorEnRangoDeExperiencia(jugador2Id, categoria)) != null) { return result; }

                // Verificar si el equipo tiene el mismo tipo que la categoría
                if ((result = PrecondicionEquipoTipoEnCategoriaTipo(equipo, categoria)) != null) { return result; }
                
                // Activar al usuario en el equipo si se diera el caso
                this.equipoTasks.CreateOrUpdate(equipo, principal.Id);
                equipoToCategoria.Equipo = equipo;
            }
            else
            {
                // Comprobar si el compañero ya está en el torneo
                if ((result = PrecondicionJugadorYaRegistrado(command.Jugador2Id, categoria, false, "Su compañero ya estás registrado en este torneo.")) != null) { return result; }

                // Comprobar que el jugador2 entra dentro del rango de la experiencia
                if ((result = PrecondicionJugadorEnRangoDeExperiencia(command.Jugador2Id, categoria)) != null) { return result; } 

                var equipos = this.equipoTasks.GetEquiposPorJugadoresList(command.Jugador1Id, command.Jugador2Id);
                if (equipos.Any())
                {
                    var equipo = equipos.First();

                    // Verificar si el equipo está ya registrado
                    if ((result = PrecondicionEquipoEnCategoria(equipo, categoria)) != null) { return result; }

                    // Verificar si el equipo tiene el mismo tipo que la categoría
                    if ((result = PrecondicionEquipoTipoEnCategoriaTipo(equipo, categoria)) != null) { return result; }

                    // Activar al usuario en el equipo y añadirlo a la categoria
                    this.equipoTasks.CreateOrUpdate(equipo, principal.Id);
                    equipoToCategoria.Equipo = equipo;
                }
                else
                {
                    // Crear equipo y añadirlo a la categoria
                    var equipo = this.equipoTasks.CreateOrUpdate(new Equipo(), command.Jugador1Id, command.Jugador2Id, principal.Id);

                    // Verificar si el equipo tiene el mismo tipo que la categoría
                    if ((result = PrecondicionEquipoTipoEnCategoriaTipo(equipo, categoria)) != null) { return result; }

                    equipoToCategoria.Equipo = equipo;
                }
            }

            equipoToCategoria.Estado = EstadoEquipoCategoriaEnum.Pendiente;
            equipoToCategoria.FechaCreacion = DateTime.Now;
            equipoToCategoria.FechaModificacion = equipoToCategoria.FechaCreacion;

            // Actions
            var validatorCtx = new ValidationContext(equipoToCategoria);
            if (equipoToCategoria.IsValid(validatorCtx))
            {
                this.equipoToCategoriaRepository.SaveOrUpdate(equipoToCategoria);
                return new CommandResult(true, "Se ha registrado correctamente en el torneo.");
            }
            else
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se puedo registrar el equipo en el torneo. Intentelo más tarde.");
            }
        }
    }

}
