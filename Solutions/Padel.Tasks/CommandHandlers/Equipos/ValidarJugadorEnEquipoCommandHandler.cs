using MediatR;
using Padel.Domain;
using Padel.Domain.Events.Equipos;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands.Equipos;
using SharpArch.Domain.PersistenceSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Tasks.CommandHandlers.Equipos
{
    public class ValidarJugadorEnEquipoCommandHandler : IRequestHandler<ValidarJugadorEnEquipoCommand, CommandResult>
    {
        private readonly IRepository<Equipo> equipoRepository;
        private readonly IMediator mediator;

        public ValidarJugadorEnEquipoCommandHandler(IRepository<Equipo> equipoRepository, IMediator mediator)
        {
            this.equipoRepository = equipoRepository;
            this.mediator = mediator;
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
            mediator.Publish(new ValidarJugadorEnEquipoEvent(command.IdEquipo, command.IdJugador));
            
            return new CommandResult(true, "Se ha activado correctamente el jugador.");
        }
    }
}
