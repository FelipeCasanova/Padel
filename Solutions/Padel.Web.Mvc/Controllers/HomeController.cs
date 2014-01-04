namespace Padel.Web.Mvc.Controllers
{
    using System.Web.Mvc;

    public partial class HomeController : BaseController
    {
        [NonAction]
        public override void SetTopMenu(ViewModels.Menu.MenuModelView modelView)
        {
            modelView.TopMenu = ViewModels.Menu.TopMenu.Home;
        }

        public virtual ActionResult Index()
        {
            return View();
        }
   
    }
}
