using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus;
using Microsoft.Web.Mvc;
using SharpArch.NHibernate.Web.Mvc;

namespace Padel.Web.Mvc.Areas.Admin.Controllers
{
    public partial class JugadoresController : BaseController
    {
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
        public virtual ActionResult _Listado()
        {
            return Json(new List<object> { new { name = "name1", surname = "surname1" }
                , new { name = "name2", surname = "surname2" }
                , new { name = "name3", surname = "surname3" }
            , new { name = "name2", surname = "surname2" }
            , new { name = "name2", surname = "surname2" }
            , new { name = "name2", surname = "surname2" }
            , new { name = "name2", surname = "surname2" }
            , new { name = "name2", surname = "surname2" }}, JsonRequestBehavior.AllowGet);
        }

    }
}
