using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;
using System.ComponentModel.DataAnnotations;

namespace Padel.Web.Mvc.Controllers.ViewModels.Torneos
{
    public class TorneoViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public EstadoCategoriaEnum EstadoCategoria { get; set; }

        public TipoTorneoEnum Tipo { get; set; }

        public string TipoStr { get { return Tipo.ToString(); } }

        public int CategoriaId { get; set; }

        public string CategoriaNombre { get; set; }

        public virtual TipoEquipoEnum TipoEquipo { get; set; }

        public string TipoEquipoStr { get { return TipoEquipo.ToString(); } }

        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }

        public int NivelMin { get; set; }

        public int NivelMax { get; set; }

        public int NumeroEquipos { get; set; }

    }
}