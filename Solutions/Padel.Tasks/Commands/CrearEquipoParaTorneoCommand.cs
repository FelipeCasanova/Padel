using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Padel.Domain;

namespace Padel.Tasks.Commands
{
    public class CrearEquipoParaTorneoCommand : PadelCommandBase
    {
        public CrearEquipoParaTorneoCommand(int torneoId, int categoriaId, int usuarioId, int equipoId, int parejaId, IList<Equipo> equipos)
        {
            this.TorneoId = torneoId;
            this.CategoriaId = categoriaId;
            this.UsuarioId = usuarioId;
            this.EquipoId = equipoId;
            this.ParejaId = parejaId;
            this.equipos = equipos;
        }

        public int TorneoId { get; set; }

        public int CategoriaId { get; set; }

        public int UsuarioId { get; set; }

        public int EquipoId { get; set; }

        public int ParejaId { get; set; }

        public IList<Equipo> equipos { get; set; }
    }
}
