using MediatR;
using Padel.Tasks.CommandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Tasks.Commands.Torneos
{
    public class EliminarEquipoDeTorneoCommand : IRequest<CommandResult>
    {
        public EliminarEquipoDeTorneoCommand(int idEquipoCategoria)
        {
            this.EquipoCategoriaId = idEquipoCategoria;
        }

        public int EquipoCategoriaId { get; set; }
    }
}
