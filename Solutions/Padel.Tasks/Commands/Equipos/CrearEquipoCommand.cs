using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands.Equipos
{
    public class CrearEquipoCommand : CommandBase
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
