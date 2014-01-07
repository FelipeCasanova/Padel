using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;

namespace Padel.Web.Mvc.Controllers.ViewModels
{
    public class JugadorViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public SexoEnum Sexo { get; set; }

        public int TelefonoMovil { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}