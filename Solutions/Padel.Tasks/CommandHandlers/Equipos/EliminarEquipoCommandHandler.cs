using MediatR;
using Padel.Domain;
using Padel.Domain.Events.Equipos;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Tasks.Commands.Equipos;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Padel.Tasks.CommandHandlers.Equipos
{
    public class EliminarEquipoCommandHandler : IRequestHandler<EliminarEquipoCommand, CommandResult>
    {
        private readonly INHibernateRepository<Equipo> equipoRepository;
        private readonly INHibernateRepository<Usuario> usuarioRepository;
        private readonly IMediator mediator;

        public EliminarEquipoCommandHandler(INHibernateRepository<Usuario> usuarioRepository, INHibernateRepository<Equipo> equipoRepository, IMediator mediator)
        {
            this.usuarioRepository = usuarioRepository;
            this.equipoRepository = equipoRepository;
            this.mediator = mediator;
        }

        public CommandResult Handle(EliminarEquipoCommand command)
        {
            Equipo equipo = this.equipoRepository.Get(command.EquipoId);
            if (equipo.EquiposToCategorias.Any(ec => ec.Categoria.Estado == EstadoCategoriaEnum.Pendiente || ec.Categoria.Estado == EstadoCategoriaEnum.Progreso))
            {
                return new CommandResult(false, "No se puede eliminar el equipo porque esta apuntado a un torneo.");
            }

            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            if (equipo.JugadorA.Id == principal.Id || equipo.JugadorB.Id == principal.Id || principal.IsInRole("Administrador"))
            {
                equipo.JugadorAVerificado = false;
                equipo.JugadorBVerificado = false;
                equipo.Estado = EstadoEquipoEnum.Eliminado;
                this.equipoRepository.Update(equipo);
                mediator.Publish(new EliminarEquipoEvent(equipo.Id, equipo.JugadorA.Id, equipo.JugadorB.Id));
                return new CommandResult(true, "Se ha eliminado correctamente el equipo.");
            }
            else
            {
                return new CommandResult(false, "No se puede eliminar el equipo. Intentelo más tarde.");
            }
        }
    }
}
