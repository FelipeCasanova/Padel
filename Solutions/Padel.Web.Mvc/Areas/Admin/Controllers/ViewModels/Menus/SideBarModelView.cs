using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus
{
    
    public class SideBarItem
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string Class { get; set; }

        public SideBarModelView SubMenu { get; set; }

        public bool IsSelected { get; set; }
    }

    public class SideBarModelView : List<SideBarItem>
    {
    }
}