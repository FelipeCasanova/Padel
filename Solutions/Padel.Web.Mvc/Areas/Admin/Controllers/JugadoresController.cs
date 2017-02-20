using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Usuarios;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Jugadores;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus;
using Padel.Web.Mvc.Filters;
using SharpArch.Web.Mvc.JsonNet;
using Padel.Tasks.Commands.Admin.Jugadores;
using Padel.Tasks.CommandResults;
using MediatR;
using SharpArch.Web.Mvc;

namespace Padel.Web.Mvc.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrador")]
    public partial class JugadoresController : BaseController
    {
        private readonly IMediator mediator;

        private readonly IJugadoresQuery jugadoresQuery;

        public JugadoresController(IMediator mediator, IJugadoresQuery jugadoresQuery)
        {
            this.mediator = mediator;
            this.jugadoresQuery = jugadoresQuery;
        }

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
        public virtual ActionResult _Listado(int? page, int? size)
        {
            var viewModel = this.jugadoresQuery.GetJugadoresList(page ?? 1, size ?? int.MaxValue);
            return new JsonNetResult(viewModel);
        }


        /// <summary>
        /// Modificar jugadores
        /// </summary>
        /// <param name="jugadoresModelView">Lista de entidades JugadorViewModel.</param>
        /// <returns>Devuelve el resultado de la operacion.</returns>
        [AjaxOnly]
        [HttpPost]
        [Transaction]
        [AllowAnonymous]
        [CustomValidateAntiForgeryTokenAttribute]
        public virtual ActionResult _Modificar(List<JugadorViewModel> jugadoresModelView)
        {
            List<object> results = new List<object>();

            foreach (var jugador in jugadoresModelView)
            {
                var command = new ModificarJugadorCommand(jugador.Id, jugador.Nombre, jugador.Sexo, jugador.TelefonoMovil, jugador.Email);
                var result = this.mediator.Send<CommandResult>(command).Result;
                if (result != null) 
                {
                    results.Add(result);
                }
            }
            
            return new JsonNetResult(results);
        }
    }
}
