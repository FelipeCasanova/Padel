using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padel.Web.Mvc.Controllers.ViewModels.Menu
{
    public enum TopMenu : byte { Home = 0, Torneos = 1, Normas_Torneos = 2, Premios = 3, Resultados = 4, Galeria = 5, Contacto = 6 }
    
    public class MenuModelView
    {
        public TopMenu TopMenu { get; set; }
    }
}