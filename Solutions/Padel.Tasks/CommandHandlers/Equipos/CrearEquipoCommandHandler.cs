using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Tasks.Commands.Equipos;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using Padel.Tasks.Events.Equipos;
using SharpArch.Domain.Events;

namespace Padel.Tasks.CommandHandlers.Equipos
{
    public class CrearEquipoCommandHandler : ICommandHandler<CrearEquipoCommand, CommandResult>
    {
        private readonly IRepository<Usuario> usuarioRepository;
        private readonly IEquipoTasks equipoTasks;

        public CrearEquipoCommandHandler(IRepository<Usuario> usuarioRepository, IEquipoTasks equipoTasks)
        {
            this.usuarioRepository = usuarioRepository;
            this.equipoTasks = equipoTasks;
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
                    DomainEvents.Raise<CrearEquipoEvent>(new CrearEquipoEvent(equipoReactivate.Id, command.Jugador1Id, command.Jugador2Id));
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
                DomainEvents.Raise<CrearEquipoEvent>(new CrearEquipoEvent(equipo.Id, command.Jugador1Id, command.Jugador2Id));
                return new CommandResult(true, "Se ha creado correctamente el equipo.");
            }
        }
    }
}
