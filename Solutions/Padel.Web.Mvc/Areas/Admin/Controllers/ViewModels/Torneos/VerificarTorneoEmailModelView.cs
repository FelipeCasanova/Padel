using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Torneos
{
    public class VerificarTorneoEmailModelView
    {
        public string NombreTorneo { get; set; }

        public string NombreCategoria { get; set; }

        public string NombreEquipo { get; set; }
    }
}