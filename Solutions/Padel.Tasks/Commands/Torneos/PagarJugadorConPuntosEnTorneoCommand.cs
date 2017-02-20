using MediatR;
using Padel.Tasks.CommandResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Tasks.Commands.Torneos
{
    public class PagarJugadorConPuntosEnTorneoCommand : IRequest<CommandResult>
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
