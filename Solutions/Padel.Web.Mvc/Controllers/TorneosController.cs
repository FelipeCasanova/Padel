using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Web.Mvc.Controllers.Queries.Torneos;

namespace Padel.Web.Mvc.Controllers
{
    public partial class TorneosController : BaseController
    {
        private readonly ITorneosQuery torneosQuery;

        public TorneosController(ITorneosQuery torneosQuery)
        {
            this.torneosQuery = torneosQuery;
        }

        [NonAction]
        public override void SetTopMenu(ViewModels.Menu.MenuModelView modelView)
        {
            modelView.TopMenu = ViewModels.Menu.TopMenu.Torneos;
        }

        public virtual ActionResult Index()
        {
            var viewModel = this.torneosQuery.GetTorneosPendientesList();
            return View(viewModel);
        }


    }
}
