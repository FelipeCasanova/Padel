using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Domain.Operaciones
{
    public class EquipoOperacion : Operacion
    {
        public enum AccionEnum : byte { CrearEquipo = 0, EliminarEquipo = 1 }
    }
}
