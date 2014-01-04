using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus;

namespace Padel.Web.Mvc.Areas.Admin.Controllers
{
    public partial class HomeAdminController : BaseController
    {
        //
        // GET: /Admin/HomeAdmin/

        public virtual ActionResult Index()
        {
            return View();
        }

        public override string GetPageTitle()
        {
            return "Home";
        }

        public override void SetBreadcrumb(ViewModels.Menus.BreadcrumbModelView breadcrumbModelView)
        {
        }

        public override void SetActiveSideMenuItem(SideBarModelView SideBarModelView)
        {
        }
    }
}
