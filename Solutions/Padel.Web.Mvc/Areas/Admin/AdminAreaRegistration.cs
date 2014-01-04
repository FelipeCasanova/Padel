using System.Web.Mvc;
using LowercaseRoutesMVC;

namespace Padel.Web.Mvc.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRouteLowercase(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {controller = "HomeAdmin",  action = "Index", id = UrlParameter.Optional },
                new[] { "Padel.Web.Mvc.Areas.Admin.Controllers" }
            );

        }
    }
}
