using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain
{
    public class Partido : PadelEntity
    {
        [Required]
        public virtual Jornada Jornada { get; set; }

        [Required]
        public virtual Equipo EquipoA { get; set; }

        public virtual int? Set1A { get; set; }
        public virtual int? Set2A { get; set; }
        public virtual int? Set3A { get; set; }

        [Required]
        public virtual Equipo EquipoB { get; set; }

        public virtual int? Set1B { get; set; }
        public virtual int? Set2B { get; set; }
        public virtual int? Set3B { get; set; }

        public virtual Equipo Ganador { get; set; }
    }
}
