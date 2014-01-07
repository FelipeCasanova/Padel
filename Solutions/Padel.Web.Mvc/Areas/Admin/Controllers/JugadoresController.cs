using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus;
using Microsoft.Web.Mvc;
using SharpArch.NHibernate.Web.Mvc;
using Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Usuarios;

namespace Padel.Web.Mvc.Areas.Admin.Controllers
{
    public partial class JugadoresController : BaseController
    {
        private readonly IJugadoresQuery jugadoresQuery;

        public JugadoresController(IJugadoresQuery jugadoresQuery)
        {
            this.jugadoresQuery = jugadoresQuery;
        }

        //
        // GET: /Admin/Jugadores/

        public virtual ActionResult Index()
        {
            return View();
        }

        public override string GetPageTitle()
        {
            return MVC.Admin.Jugadores.Name;
        }

        public override void SetBreadcrumb(ViewModels.Menus.BreadcrumbModelView breadcrumbModelView)
        {
            breadcrumbModelView.Add(new BreadcrumbItem { Name = MVC.Admin.Jugadores.Name, Url = Url.Action(MVC.Admin.HomeAdmin.Actions.ActionNames.Index, MVC.Admin.Jugadores.Name) });
        }

        public override void SetActiveSideMenuItem(SideBarModelView SideBarModelView)
        {
            var menuItem = SideBarModelView.First(m => m.Name == MVC.Admin.Jugadores.Name);
            menuItem.IsSelected = true;
        }

        [HttpPost]
        [Transaction]
        public virtual ActionResult _Listado(int? page, int? size)
        {
            var viewModel = this.jugadoresQuery.GetJugadoresList(page ?? 1, size ?? int.MaxValue);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
