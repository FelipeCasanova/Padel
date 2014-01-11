using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus;
using SharpArch.NHibernate.Web.Mvc;
using Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Torneos;

namespace Padel.Web.Mvc.Areas.Admin.Controllers
{
    public partial class TorneosController : BaseController
    {
        private readonly ITorneosQuery torneosQuery;

        public TorneosController(ITorneosQuery torneosQuery)
        {
            this.torneosQuery = torneosQuery;
        }

        //
        // GET: /Admin/Torneos/

        public virtual ActionResult Index()
        {
            return View();
        }

        public override string GetPageTitle()
        {
            return MVC.Admin.Torneos.Name;
        }

        public override void SetBreadcrumb(ViewModels.Menus.BreadcrumbModelView breadcrumbModelView)
        {
            breadcrumbModelView.Add(new BreadcrumbItem { Name = MVC.Admin.Torneos.Name, Url = Url.Action(MVC.Admin.Torneos.Actions.ActionNames.Index, MVC.Admin.Torneos.Name) });
        }

        public override void SetActiveSideMenuItem(SideBarModelView SideBarModelView)
        {
            var menuItem = SideBarModelView.First(m => m.Name == MVC.Admin.Torneos.Name);
            menuItem.IsSelected = true;
        }

        [HttpPost]
        [Transaction]
        public virtual ActionResult _Listado(int? page, int? size)
        {
            var viewModel = this.torneosQuery.GetTorneosList(page ?? 1, size ?? int.MaxValue);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
