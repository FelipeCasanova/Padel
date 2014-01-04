using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain
{
    public class Jornada : PadelEntity
    {
        public Jornada()
        {
            Partidos = new List<Partido>();
        }

        [Required]
        public virtual Grupo Grupo { get; set; }

        [Required]
        public virtual IList<Partido> Partidos { get; set; }
    }
}
