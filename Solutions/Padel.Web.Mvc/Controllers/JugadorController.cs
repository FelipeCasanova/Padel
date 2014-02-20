using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Infrastructure.Utilities;
using System.Threading;
using Padel.Web.Mvc.Controllers.Queries.Operaciones;
using Padel.Domain.Contracts.Tasks;
using SharpArch.Domain.Commands;

namespace Padel.Web.Mvc.Controllers
{
    [Authorize(Roles = "Administrador, Jugador")]
    public partial class JugadorController : BaseJugadorController
    {
        private readonly ICommandProcessor commandProcessor;

        private readonly IUsuarioTasks usuarioTasks;

        private readonly IOperacionesQuery operacionesQuery;

        public JugadorController(ICommandProcessor commandProcessor, IUsuarioTasks usuarioTasks, IOperacionesQuery operacionesQuery)
        {
            this.commandProcessor = commandProcessor;
            this.usuarioTasks = usuarioTasks;
            this.operacionesQuery = operacionesQuery;
        }

        [NonAction]
        public override void SetSideMenuJugador(ViewModels.Menu.SideMenuJugadorModelView modelView)
        {
        }

        [NonAction]
        public override void SetTopMenu(ViewModels.Menu.MenuModelView modelView)
        {
        }

        //
        // GET: /Jugador/

        public virtual ActionResult Index()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Dashboard;
            return View();
        }

        public virtual PartialViewResult GetOperacionesPorUsuario()
        {
            var usuario = (PadelPrincipal)Thread.CurrentPrincipal;
            return PartialView(MVC.Shared.Views.Jugador._OperacionModelView, this.operacionesQuery.GetOperacionesPorUsuario(usuario.Id));
        }

        public virtual ActionResult Datos()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Datos;
            return View();
        }

        public virtual ActionResult Equipos()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Equipos;
            return View();
        }

        public virtual ActionResult Partidos()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Partidos;
            return View();
        }

        public virtual ActionResult Torneos()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Torneos;
            return View();
        }

        public virtual ActionResult Graficas()
        {
            SideMenuJugadorModelView.SideMenuJugador = ViewModels.Menu.SideMenuJugadorEnum.Graficas;
            return View();
        }


        
    }
}
