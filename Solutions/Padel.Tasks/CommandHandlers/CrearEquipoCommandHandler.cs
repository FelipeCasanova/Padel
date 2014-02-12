using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;
using Padel.Tasks.Commands;
using Padel.Tasks.CommandResults;
using SharpArch.Domain.PersistenceSupport;
using Padel.Domain;
using System.Threading;
using Padel.Infrastructure.Utilities;
using Padel.Domain.Contracts.Tasks;

namespace Padel.Tasks.CommandHandlers
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
            var equipos = this.equipoTasks.GetEquiposPorJugadoresList(command.Jugador1Id, command.Jugador2Id);

            if (equipos.Any())
            {
                var equipoReactivate = equipos.First();

                // Reactivar el equipo
                this.equipoTasks.CreateOrUpdate(equipoReactivate, command.Jugador1Id, command.Jugador2Id);
                return new CommandResult(true, "Se ha creado correctamente el equipo.");
            }
            else
            {
                // crear equipo nuevo
                var equipo = new Equipo();
                this.equipoTasks.CreateOrUpdate(equipo, command.Jugador1Id, command.Jugador2Id);
                return new CommandResult(true, "Se ha creado correctamente el equipo.");
            }
        }
    }
}
