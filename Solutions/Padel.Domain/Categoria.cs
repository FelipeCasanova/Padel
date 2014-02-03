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
            Grupos = new List<Grupo>();
            EquiposToCategorias = new List<EquipoToCategoria>();
        }

        [Required]
        public virtual string Nombre { get; set; }

        [Required]
        public virtual int NivelMin { get; set; }

        [Required]
        public virtual int NivelMax { get; set; }

        [Required]
        public virtual EstadoCategoriaEnum Estado { get; set; }

        [Required]
        public virtual TipoEquipoEnum TipoEquipo { get; set; }

        [Required]
        [Range(5, 100)]
        public virtual Decimal Precio { get; set; }

        [Required]
        public virtual DateTime FechaInicio { get; set; }

        [Required]
        public virtual DateTime FechaFin { get; set; }

        [Required]
        public virtual Torneo Torneo { get; set; }

        [Required]
        public virtual IList<Grupo> Grupos { get; set; }

        [Required]
        public virtual IList<EquipoToCategoria> EquiposToCategorias { get; set; }
        
        public virtual Equipo Ganador { get; set; }
    }
}
