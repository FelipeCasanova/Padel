﻿using System;
using System.ComponentModel.DataAnnotations;
using Padel.Domain;
using System.Collections.Generic;

namespace Padel.Domain
{
    public class Usuario : PadelEntity
    {
        public Usuario()
        {
            Roles = new List<Role>();
        }

        [Required]
        [Display(Name = "Nombre")]
        public virtual string Nombre { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public virtual SexoEnum Sexo { get; set; }

        [Required]
        [Display(Name = "Móvil")]
        [RegularExpression("\\d{9}", ErrorMessage="Introduzca un número de móvil correcto.")]
        public virtual int TelefonoMovil { get; set; }

        [Required]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Introduzca una cuenta de correo correcta.")]
        public virtual string Email { get; set; }

        [Display(Name = "Password")]
        public virtual string Password { get; set; }

        [Required]
        [Display(Name = "Lv.")]
        [UIHint("GameLevel")]
        public virtual int Nivel { get; set; }

        [Required]
        [Display(Name = "Exp.")]
        [UIHint("GameExp")]
        public virtual int PuntosExperiencia { get; set; }

        [Required]
        [Display(Name = "Corazones")]
        [UIHint("GameHearts")]
        public virtual int AplicacionExperiencia { get; set; }

        [Required]
        [Display(Name = "Bola/s")]
        [UIHint("GameCurrency")]
        public virtual decimal DineroFicticio { get; set; }

        [Required]
        public virtual IList<Role> Roles { get; set; }

        public virtual string Ip { get; set; }
    }
}