using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Padel.Web.Mvc.Controllers.ViewModels.Torneos
{
    public class EquipoTorneoViewModel : TorneoViewModel
    {
        public int EquipoId { get; set; }

        public string EquipoNombre { get; set; }

        public string JugadorANombre { get; set; }

        public string JugadorBNombre { get; set; }
    }
}