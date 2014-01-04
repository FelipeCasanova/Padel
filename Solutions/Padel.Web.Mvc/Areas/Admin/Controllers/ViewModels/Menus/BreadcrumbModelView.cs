using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus
{
    public class BreadcrumbItem
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }

    public class BreadcrumbModelView : List<BreadcrumbItem>
    {
    }
}