using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain.Operaciones
{
    public class EquipoOperacion : Operacion
    {
        public enum AccionEnum : byte { CrearEquipo = 0, EliminarEquipo = 1, ValidarJugadorEnEquipo = 2 }

        [Required]
        public virtual Equipo Equipo { get; set; }
    }
}
