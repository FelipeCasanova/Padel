using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands
{
    public class EliminarEquipoCommand : CommandBase
    {
        public EliminarEquipoCommand(int idEquipo, int idJugador)
        {
            this.EquipoId = idEquipo;
            this.JugadorId = idJugador;
        }

        public int EquipoId { get; set; }

        public int JugadorId { get; set; }
    }
}
