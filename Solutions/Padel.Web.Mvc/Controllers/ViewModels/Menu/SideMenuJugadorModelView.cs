using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padel.Web.Mvc.Controllers.ViewModels.Menu
{
    public enum SideMenuJugadorEnum : byte { Dashboard = 0, Datos = 1,  Equipos = 2, Partidos = 3, Torneos = 4, Graficas = 5 }
    
    public class SideMenuJugadorModelView
    {
        public SideMenuJugadorEnum SideMenuJugador { get; set; }
    }
}