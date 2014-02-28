using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Padel.Web.Mvc.Controllers.ViewModels.Usuarios
{
    public class UsuarioRegistrarModelView
    {
        public Usuario Usuario { get; set; }

        [Required]
        [Display(Name = "Password")]
        public virtual string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password and Confirm Password tienen que ser iguales.")]
        public string PasswordConfirm { get; set; }

        [Required]
        [Display(Name = "Móvil")]
        [RegularExpression("\\d{9}", ErrorMessage = "Introduzca un número de móvil correcto.")]
        [Remote("ValidarTelefonoUnico", "Usuarios")]
        public virtual int TelefonoMovil { get; set; }

        [DataType(DataType.EmailAddress, ErrorMessage = "Introduzca una cuenta de correo correcta.")]
        [Remote("ValidarEmailUnico", "Usuarios")]
        [Required]
        public virtual string Email { get; set; }

    }
}