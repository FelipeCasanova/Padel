using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Web.Mvc.Controllers.Queries.Torneos;
using Padel.Web.Mvc.Filters;
using SharpArch.Domain.Commands;
using SharpArch.NHibernate.Web.Mvc;
using Padel.Domain.Contracts.Tasks;

namespace Padel.Web.Mvc.Controllers
{
    public partial class TorneosController : BaseController
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly ITorneosQuery torneosQuery;
        private readonly IEquipoTasks equipoTasks;

        public TorneosController(ICommandProcessor commandProcessor, ITorneosQuery torneosQuery, IEquipoTasks equipoTasks)
        {
            this.commandProcessor = commandProcessor;
            this.torneosQuery = torneosQuery;
            this.equipoTasks = equipoTasks;
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

        
        [AjaxOnly]
        [HttpPost]
        [Transaction]
        [Authorize(Roles = "Administrador, Jugador")]
        [CustomValidateAntiForgeryTokenAttribute]
        public virtual ActionResult _CrearEquipoParaTorneo(int? idEquipo, int? idJugador, int? idTorneo, int? idCategoria, bool? terminosAceptados)
        {
            if (!terminosAceptados.GetValueOrDefault())
            {
                return Json(new CommandResult(false, "Debe aceptar los términos si quiere registrarse."), JsonRequestBehavior.AllowGet);
            }

            if (ModelState.IsValid)
            {
                var command = new CrearEquipoParaTorneoCommand(
                    idTorneo.GetValueOrDefault(), 
                    idCategoria.GetValueOrDefault(), 
                    ((PadelPrincipal)User).Id, 
                    idEquipo.GetValueOrDefault(), 
                    idJugador.GetValueOrDefault());
                var results = this.commandProcessor.Process<CrearEquipoParaTorneoCommand, CommandResult>(command);
                return Json(results.First(), JsonRequestBehavior.AllowGet);
            }

            return Json(new CommandResult(false, "No se puedo registrar el equipo en el torneo. Intentelo más tarde."), JsonRequestBehavior.AllowGet);
        }

    }
}
