﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Padel.Domain
{
    public class Equipo : PadelEntity
    {
        public Equipo()
        {
        }

        public virtual string Nombre { get; set; }

        [Required]
        public virtual TipoEquipoEnum TipoEquipo { get; set; }

        [Required]
        public virtual Usuario JugadorA { get; set; }

        public virtual bool JugadorAVerificado { get; set; }

        [Required]
        public virtual Usuario JugadorB { get; set; }

        public virtual bool JugadorBVerificado { get; set; }

    }
}