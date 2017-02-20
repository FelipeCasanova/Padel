using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediatR;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.Commands.Equipos
{
    public class CrearEquipoCommand : IRequest<CommandResult>
    {
        public CrearEquipoCommand(int idJugadorA, int idJugadorB)
        {
            this.Jugador1Id = idJugadorA;
            this.Jugador2Id = idJugadorB;
        }

        public int Jugador1Id { get; set; }

        public int Jugador2Id { get; set; }
    }

    
}
