using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain
{
    public class EquipoToCategoria : PadelEntity
    {
        [Required]
        public virtual Equipo Equipo { get; set; }

        [Required]
        public virtual Categoria Categoria { get; set; }

    }
}
