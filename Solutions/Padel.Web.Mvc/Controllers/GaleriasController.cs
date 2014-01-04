using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Padel.Web.Mvc.Controllers
{
    public partial class GaleriasController : BaseController
    {

        [NonAction]
        public override void SetTopMenu(ViewModels.Menu.MenuModelView modelView)
        {
            modelView.TopMenu = ViewModels.Menu.TopMenu.Galeria;
        }

        public virtual ActionResult Index()
        {
            return View();
        }


    }
}
