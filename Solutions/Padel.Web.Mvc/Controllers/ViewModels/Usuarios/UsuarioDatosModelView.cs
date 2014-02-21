using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Padel.Web.Mvc.Controllers.ViewModels.Usuarios
{
    public class UsuarioDatosModelView
    {
        [Required]
        public Usuario Usuario { get; set; }

    }
}