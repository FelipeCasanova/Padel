using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Web.Mvc.Controllers.ViewModels.Menu;

namespace Padel.Web.Mvc.Controllers
{
    public abstract partial class BaseController : Controller
    {
        public MenuModelView MenuModelView { get; set; }

        public BaseController()
        {
            // Asignamos el menu superior
            MenuModelView = new MenuModelView();
            ViewBag.MenuModelView = MenuModelView;

            // Valores por defecto
            MenuModelView.TopMenu = ViewModels.Menu.TopMenu.Home;

            SetTopMenu(MenuModelView);
        }

        public abstract void SetTopMenu(MenuModelView modelView);
    }
}
