using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padel.Web.Mvc.Controllers.ViewModels.Equipos
{
    public class EquipoViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int JugadorAId { get; set; }

        public string JugadorANombre { get; set; }

        public int JugadorBId { get; set; }

        public string JugadorBNombre { get; set; }
    }
}