using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Padel.Domain;

namespace Padel.Web.Mvc.Controllers.ViewModels.Torneos
{
    public class TorneoViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public TipoTorneoEnum Tipo { get; set; }

        public string TipoStr { get { return Tipo.ToString(); } }

        public string Categoria { get; set; }
    }
}