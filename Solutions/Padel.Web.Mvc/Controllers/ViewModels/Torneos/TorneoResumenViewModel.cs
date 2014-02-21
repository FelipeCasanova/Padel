using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padel.Web.Mvc.Controllers.ViewModels.Torneos
{
    public class TorneoResumenViewModel
    {
        public int TorneosPendientes { get; set; }

        public int TorneosProgreso { get; set; }

        public int TorneosGanados { get; set; }

        public int TorneosPerdidos { get; set; }
    }
}