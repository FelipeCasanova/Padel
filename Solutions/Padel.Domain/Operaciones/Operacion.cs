using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpArch.Domain.DomainModel;

namespace Padel.Domain.Operaciones
{
    
    public class Operacion : PadelEntity
    {
        public virtual string Mensaje { get; set; }

        public virtual string Accion { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
