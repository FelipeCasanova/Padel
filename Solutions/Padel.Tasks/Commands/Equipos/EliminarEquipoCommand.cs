using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands.Equipos
{
    public class EliminarEquipoCommand : CommandBase
    {
        public EliminarEquipoCommand(int idEquipo)
        {
            this.EquipoId = idEquipo;
        }

        public int EquipoId { get; set; }
    }
}
