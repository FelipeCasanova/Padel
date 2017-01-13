using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Jugadores
{
    public class JugadorViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SexoEnum Sexo { get; set; }

        public int TelefonoMovil { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}