using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands.Torneos
{
    public class EliminarEquipoDeTorneoCommand : CommandBase
    {
        public EliminarEquipoDeTorneoCommand(int idEquipo, int idCategoria)
        {
            this.EquipoId = idEquipo;
            this.CategoriaId = idCategoria;
        }

        public int EquipoId { get; set; }

        public int CategoriaId { get; set; }
    }
}
