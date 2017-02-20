using MediatR;
using Padel.Tasks.CommandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Tasks.Commands.Equipos
{
    public class EliminarEquipoCommand : IRequest<CommandResult>
    {
        public EliminarEquipoCommand(int idEquipo)
        {
            this.EquipoId = idEquipo;
        }

        public int EquipoId { get; set; }
    }
}
