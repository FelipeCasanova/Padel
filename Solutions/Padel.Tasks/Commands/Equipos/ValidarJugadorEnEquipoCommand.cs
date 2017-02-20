using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.Commands.Equipos
{
    public class ValidarJugadorEnEquipoCommand : IRequest<CommandResult>
    {
        public ValidarJugadorEnEquipoCommand(int idEquipo, int idJugador)
        {
            this.IdEquipo = idEquipo;
            this.IdJugador = idJugador;
        }

        public int IdEquipo { get; set; }
    
        public int IdJugador { get; set; }}
}
