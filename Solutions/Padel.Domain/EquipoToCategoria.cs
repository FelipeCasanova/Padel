using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Domain
{
    public class EquipoToCategoria : PadelEntity
    {
        public virtual Equipo Equipo { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
