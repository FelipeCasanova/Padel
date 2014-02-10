using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Web.Mvc.Controllers.ViewModels.Menu;

namespace Padel.Web.Mvc.Controllers
{
    public abstract partial class BaseJugadorController : BaseController
    {
        public SideMenuJugadorModelView SideMenuJugadorModelView { get; set; }

        public BaseJugadorController() : base()
        {
            // Asignamos el menu superior
            SideMenuJugadorModelView = new SideMenuJugadorModelView();
            ViewBag.SideMenuJugadorModelView = SideMenuJugadorModelView;

            // Valores por defecto
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Dashboard;

            SetSideMenuJugador(SideMenuJugadorModelView);
        }

        public abstract void SetSideMenuJugador(SideMenuJugadorModelView modelView);
        
    }
}
