using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Torneos
{
    public class TorneoViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TipoTorneoEnum Tipo { get; set; }

        public string Categoria { get; set; }

        public int CategoriaId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EstadoCategoriaEnum Estado { get; set; }

        public DateTime FechaModificacion { get; set; }
    }
}