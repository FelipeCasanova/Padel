using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus;

namespace Padel.Web.Mvc.Areas.Admin.Controllers
{

    [Authorize(Roles = "Administrador")]
    public abstract partial class BaseController : Controller
    {
        public SideBarModelView SideBarModelView { get; set; }

        public BreadcrumbModelView BreadcrumbModelView { get; set; }

        public BaseController()
        {
            // Asignamos el menu sidebar
            SideBarModelView = new SideBarModelView();
            ViewBag.SideBarModelView = SideBarModelView;
            
            // Asignamos el menu breadcrumb
            BreadcrumbModelView = new BreadcrumbModelView();
            ViewBag.BreadcrumbModelView = BreadcrumbModelView;
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Valores por defecto
            ViewBag.PageTitle = GetPageTitle();

            // Valores por defecto
            SideBarModelView.Add(new SideBarItem
            {
                Name = MVC.Admin.Torneos.Name,
                Url = "#",
                Class = "fa fa-sitemap",
                SubMenu = new SideBarModelView() { 
                    new SideBarItem() { 
                        Name = "Gestionar Torneos",
                        Url = Url.Action(MVC.Admin.Torneos.Actions.ActionNames.Index, MVC.Admin.Torneos.Name)
                    }
                }
            });
            SideBarModelView.Add(new SideBarItem
            {
                Name = MVC.Admin.Jugadores.Name,
                Url = Url.Action(MVC.Admin.Jugadores.Actions.ActionNames.Index, MVC.Admin.Jugadores.Name),
                Class = "fa fa-heart"
            });
            SetActiveSideMenuItem(SideBarModelView);

            // Valores por defecto
            BreadcrumbModelView.Add(new BreadcrumbItem { Name = "Home", Url = Url.Action(MVC.Admin.HomeAdmin.Actions.ActionNames.Index, MVC.Admin.HomeAdmin.Name) });
            SetBreadcrumb(BreadcrumbModelView);

            base.OnActionExecuting(filterContext);
        }

        public abstract string GetPageTitle();

        public abstract void SetActiveSideMenuItem(SideBarModelView SideBarModelView);

        public abstract void SetBreadcrumb(BreadcrumbModelView breadcrumbModelView);
        
    }
}
