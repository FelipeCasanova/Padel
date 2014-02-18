using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padel.Domain.Operaciones
{
    public class UsuarioOperacion : Operacion
    {
        public enum AccionEnum : byte { Registrar = 0, Entrar = 1 }

        public virtual Usuario Usuario { get; set; }
    }
}
