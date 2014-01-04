using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain
{
    public class Torneo : PadelEntity
    {
        public Torneo()
        {
            Categorias = new List<Categoria>();
        }

        public virtual string Name { get; set; }

        [Required]
        public virtual IList<Categoria> Categorias { get; set; }

    }
}
