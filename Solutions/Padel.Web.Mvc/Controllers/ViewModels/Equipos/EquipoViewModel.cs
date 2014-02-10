using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Padel.Web.Mvc.Controllers.ViewModels.Equipos
{
    public class EquipoViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int JugadorAId { get; set; }

        public string JugadorANombre { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SexoEnum JugadorASexo { get; set; }

        public bool JugadorAVerificado { get; set; }

        public int JugadorBId { get; set; }

        public string JugadorBNombre { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SexoEnum JugadorBSexo { get; set; }

        public bool JugadorBVerificado { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EstadoEquipoEnum Estado { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TipoEquipoEnum TipoEquipo { get; set; }
    }
}