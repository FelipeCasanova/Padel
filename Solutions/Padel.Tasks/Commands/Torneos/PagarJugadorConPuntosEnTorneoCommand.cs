using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.Commands;

namespace Padel.Tasks.Commands.Torneos
{
    public class PagarJugadorConPuntosEnTorneoCommand : CommandBase
    {

        public PagarJugadorConPuntosEnTorneoCommand(int idEquipoCategoria, int tipo)
        {
            this.EquipoCategoriaId = idEquipoCategoria;
            this.Tipo = tipo;
        }

        public int EquipoCategoriaId { get; set; }

        public int Tipo { get; set; }

    }
}
