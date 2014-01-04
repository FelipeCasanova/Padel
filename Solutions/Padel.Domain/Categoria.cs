using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain
{
    public class Categoria : PadelEntity
    {
        public Categoria()
        {
            Grupo = new List<Grupo>();
        }

        [Required]
        public virtual DateTime FechaInicio { get; set; }

        [Required]
        public virtual DateTime FechaFin { get; set; }

        [Required]
        public virtual Torneo Torneo { get; set; }

        [Required]
        public virtual IList<Grupo> Grupo { get; set; }

        public virtual Equipo Ganador { get; set; }
    }
}
