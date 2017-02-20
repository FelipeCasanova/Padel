using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using MediatR;
using Padel.Tasks.CommandResults;

namespace Padel.Tasks.Commands.Torneos
{
    public class VerificarTorneoCommand : IRequest<CommandResult>
    {
        public VerificarTorneoCommand(int categoriaId)
        {
            this.CategoriaId = categoriaId;
        }

        public int CategoriaId { get; set; }
    }
}
