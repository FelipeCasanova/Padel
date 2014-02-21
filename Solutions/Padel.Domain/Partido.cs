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

        [Required]
        public virtual Equipo EquipoB { get; set; }

        public virtual Resultado Resultado { get; set; }
    }
}
