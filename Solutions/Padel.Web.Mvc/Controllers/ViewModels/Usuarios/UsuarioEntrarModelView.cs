using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Padel.Web.Mvc.Controllers.ViewModels.Usuarios
{
    public class UsuarioEntrarModelView
    {
        [Required]
        [Display(Name = "Email o número telefónico")]
        public virtual string EmailOTelefonoMovil { get; set; }

        [Required]
        [Display(Name = "Password")]
        public virtual string PasswordEntrar { get; set; }

        public static UsuarioEntrarModelView CrearUsuarioModelView(Usuario usuario)
        {
            UsuarioEntrarModelView model = new UsuarioEntrarModelView();
            return model;
        }
    }
}