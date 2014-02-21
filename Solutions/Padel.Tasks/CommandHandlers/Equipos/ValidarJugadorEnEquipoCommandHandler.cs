using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands.Equipos;
using Padel.Tasks.Events.Equipos;
using SharpArch.Domain.Commands;
using SharpArch.Domain.Events;
using SharpArch.Domain.PersistenceSupport;

namespace Padel.Tasks.CommandHandlers.Equipos
{
    public class ValidarJugadorEnEquipoCommandHandler : ICommandHandler<ValidarJugadorEnEquipoCommand, CommandResult>
    {
        private readonly IRepository<Equipo> equipoRepository;

        public ValidarJugadorEnEquipoCommandHandler(IRepository<Equipo> equipoRepository)
        {
            this.equipoRepository = equipoRepository;
        }


        public CommandResult Handle(ValidarJugadorEnEquipoCommand command)
        {
            Equipo equipo = this.equipoRepository.Get(command.IdEquipo);
            if (equipo.JugadorA.Id == command.IdJugador)
            {
                equipo.JugadorAVerificado = true;
            }
            if (equipo.JugadorB.Id == command.IdJugador)
            {
                equipo.JugadorBVerificado = true;
            }
            if (equipo.JugadorAVerificado == true && equipo.JugadorBVerificado == true)
            {
                equipo.Estado = EstadoEquipoEnum.Activado;
            }
            this.equipoRepository.SaveOrUpdate(equipo);
            DomainEvents.Raise<ValidarJugadorEnEquipoEvent>(new ValidarJugadorEnEquipoEvent(command.IdEquipo, command.IdJugador));
            return new CommandResult(true, "Se ha activado correctamente el jugador.");
        }
    }
}
