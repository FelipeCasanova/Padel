using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands.Torneos
{
    public class VerificarTorneoCommand : CommandBase
    {
        public VerificarTorneoCommand(int categoriaId)
        {
            this.CategoriaId = categoriaId;
        }

        public int CategoriaId { get; set; }
    }
}
