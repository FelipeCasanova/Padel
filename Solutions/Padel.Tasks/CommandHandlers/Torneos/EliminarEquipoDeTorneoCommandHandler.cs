using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Padel.Domain;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands.Torneos;
using SharpArch.Domain.PersistenceSupport;
using MediatR;
using System.ComponentModel.DataAnnotations;
using SharpArch.NHibernate.Contracts.Repositories;

namespace Padel.Tasks.CommandHandlers.Equipos
{
    public class EliminarEquipoDeTorneoCommandHandler : IRequestHandler<EliminarEquipoDeTorneoCommand, CommandResult>
    {
        private readonly INHibernateRepository<EquipoToCategoria> equipoToCategoriaRepository;
        private readonly INHibernateRepository<Usuario> usuarioRepository;

        public EliminarEquipoDeTorneoCommandHandler(INHibernateRepository<EquipoToCategoria> equipoToCategoriaRepository, INHibernateRepository<Usuario> usuarioRepository)
        {
            this.equipoToCategoriaRepository = equipoToCategoriaRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public CommandResult Handle(EliminarEquipoDeTorneoCommand command)
        {
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            EquipoToCategoria equiposToCategoria = this.equipoToCategoriaRepository.Get(command.EquipoCategoriaId);
            
            if (equiposToCategoria == null)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se ha podido eliminar el equipo del torneo. Intentelo más tarde.");
            }

            if (equiposToCategoria.Categoria.Estado == EstadoCategoriaEnum.Progreso)
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se puede eliminar el equipo del torneo porque esta en progreso.");
            }

            var validatorCtx = new ValidationContext(equiposToCategoria);
            if (equiposToCategoria.IsValid(validatorCtx) && 
                (equiposToCategoria.Equipo.JugadorA.Id == principal.Id || equiposToCategoria.Equipo.JugadorB.Id == principal.Id 
                || principal.IsInRole("Administrador")))
            {
                decimal devolver = 0;
                // Jugador A
                Usuario usuarioA = this.usuarioRepository.Get(equiposToCategoria.Equipo.JugadorA.Id);
                usuarioA.DineroFicticio += equiposToCategoria.DineroFicticioJugadorA + equiposToCategoria.DineroRealJugadorA;
                if (usuarioA.Id == principal.Id)
                {
                    devolver = equiposToCategoria.DineroFicticioJugadorA + equiposToCategoria.DineroRealJugadorA;
                }

                // Jugador B
                Usuario usuarioB = this.usuarioRepository.Get(equiposToCategoria.Equipo.JugadorB.Id);
                usuarioB.DineroFicticio += equiposToCategoria.DineroFicticioJugadorB + equiposToCategoria.DineroRealJugadorB;
                if (usuarioB.Id == principal.Id)
                {
                    devolver = equiposToCategoria.DineroFicticioJugadorB + equiposToCategoria.DineroRealJugadorB;
                }

                equiposToCategoria.Estado = EstadoEquipoCategoriaEnum.Eliminado;
                this.equipoToCategoriaRepository.Update(equiposToCategoria);
                
                return new CommandResult(true, "Se ha eliminado correctamente el equipo del torneo.", new { Precio = devolver });    
            }
            else
            {
                //this.equipoToCategoriaRepository.DbContext.RollbackTransaction();
                return new CommandResult(false, "No se ha podido eliminar el equipo del torneo. Intentelo más tarde.");
            }
            
        }
    }
}
