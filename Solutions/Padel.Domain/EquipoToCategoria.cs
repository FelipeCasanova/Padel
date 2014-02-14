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
        public virtual EstadoEquipoCategoriaEnum Estado { get; set; }

        [Range(0, 50)]
        public virtual decimal DineroRealJugadorA { get; set; }

        [Range(0, 50)]
        public virtual decimal DineroFicticioJugadorA { get; set; }

        [Range(0, 50)]
        public virtual decimal DineroRealJugadorB { get; set; }

        [Range(0, 50)]
        public virtual decimal DineroFicticioJugadorB { get; set; }

        [Required]
        public virtual Equipo Equipo { get; set; }

        [Required]
        public virtual Categoria Categoria { get; set; }
    }
}
