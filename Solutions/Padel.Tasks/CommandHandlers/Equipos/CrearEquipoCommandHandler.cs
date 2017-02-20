using MediatR;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;
using Padel.Domain.Events.Equipos;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Tasks.Commands.Equipos;
using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Padel.Tasks.CommandHandlers.Equipos
{
    public class CrearEquipoCommandHandler : IRequestHandler<CrearEquipoCommand, CommandResult>
    {
        private readonly IRepository<Usuario> usuarioRepository;
        private readonly IEquipoTasks equipoTasks;
        private readonly IMediator mediator;

        public CrearEquipoCommandHandler(IRepository<Usuario> usuarioRepository, IEquipoTasks equipoTasks, IMediator mediator)
        {
            this.usuarioRepository = usuarioRepository;
            this.equipoTasks = equipoTasks;
            this.mediator = mediator;
        }

        public CommandResult Handle(CrearEquipoCommand command)
        {
            PadelPrincipal principal = (PadelPrincipal)Thread.CurrentPrincipal;
            var equipos = this.equipoTasks.GetEquiposPorJugadoresList(command.Jugador1Id, command.Jugador2Id);
            
            if (equipos.Any())
            {
                var equipoReactivate = equipos.First();

                // Reactivar el equipo
                var estabaEliminado = equipoReactivate.Estado == EstadoEquipoEnum.Eliminado;
                this.equipoTasks.CreateOrUpdate(equipoReactivate, command.Jugador1Id, command.Jugador2Id, principal.Id);
                if (estabaEliminado)
                {
                    mediator.Publish(new CrearEquipoEvent(equipoReactivate.Id, command.Jugador1Id, command.Jugador2Id));
                    return new CommandResult(true, "Se ha creado correctamente el equipo.");
                }
                else
                {
                    return new CommandResult(true, "El equipo ya existe.");
                }
            }
            else
            {
                // crear equipo nuevo
                var equipo = new Equipo();
                this.equipoTasks.CreateOrUpdate(equipo, command.Jugador1Id, command.Jugador2Id, principal.Id);
                mediator.Publish(new CrearEquipoEvent(equipo.Id, command.Jugador1Id, command.Jugador2Id));
                return new CommandResult(true, "Se ha creado correctamente el equipo.");
            }
        }
    }
}
