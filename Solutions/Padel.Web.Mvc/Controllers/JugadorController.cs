using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padel.Web.Mvc.Controllers
{
    [Authorize(Roles = "Administrador, Jugador")]
    public partial class JugadorController : BaseJugadorController
    {
        [NonAction]
        public override void SetSideMenuJugador(ViewModels.Menu.SideMenuJugadorModelView modelView)
        {
        }

        [NonAction]
        public override void SetTopMenu(ViewModels.Menu.MenuModelView modelView)
        {
        }

        //
        // GET: /Jugador/

        public virtual ActionResult Index()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Dashboard;
            return View();
        }

        public virtual ActionResult Datos()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Datos;
            return View();
        }

        public virtual ActionResult Equipos()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Equipos;
            return View();
        }

        public virtual ActionResult Partidos()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Partidos;
            return View();
        }

        public virtual ActionResult Torneos()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Torneos;
            return View();
        }

        public virtual ActionResult Graficas()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Graficas;
            return View();
        }


        
    }
}
