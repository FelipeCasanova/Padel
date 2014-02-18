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
    public class TorneoViewModel : IPrecio
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EstadoCategoriaEnum EstadoCategoria { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TipoTorneoEnum Tipo { get; set; }

        public int CategoriaId { get; set; }

        public string CategoriaNombre { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual TipoEquipoEnum TipoEquipo { get; set; }

        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        public int NivelMin { get; set; }

        public int NivelMax { get; set; }

        public int NumeroEquipos { get; set; }

    }
}