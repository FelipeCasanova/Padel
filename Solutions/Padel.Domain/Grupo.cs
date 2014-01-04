using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain
{
    public class Grupo : PadelEntity
    {
        public Grupo()
        {
            Jornadas = new List<Jornada>();
        }

        [Required]
        public virtual Categoria Categoria { get; set; }

        [Required]
        public virtual IList<Jornada> Jornadas { get; set; }
    }
}
