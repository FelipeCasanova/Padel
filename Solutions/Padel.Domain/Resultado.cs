using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain
{
    public class Resultado : PadelEntity
    {
        public virtual int? Set1A { get; set; }
        public virtual int? Set2A { get; set; }
        public virtual int? Set3A { get; set; }

        public virtual int? Set1B { get; set; }
        public virtual int? Set2B { get; set; }
        public virtual int? Set3B { get; set; }

        public virtual Equipo Ganador { get; set; }
    }
}
