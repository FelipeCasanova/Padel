using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SharpArch.NHibernate.Web.Mvc;
using Padel.Web.Mvc.Controllers.Queries.Equipos;
using SharpArch.Domain.Commands;
using SharpArch.Web.Mvc.JsonNet;
using Padel.Web.Mvc.Filters;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.Commands;
using Padel.Tasks.CommandResults;

namespace Padel.Web.Mvc.Controllers
{
    [Authorize(Roles = "Administrador, Jugador")]
    public partial class EquiposController : Controller
    {
        private readonly ICommandProcessor commandProcessor;
        private readonly IEquiposQuery equiposQuery;

        public EquiposController(ICommandProcessor commandProcessor, IEquiposQuery equiposQuery)
        {
            this.commandProcessor = commandProcessor;
            this.equiposQuery = equiposQuery;
        }

        [HttpPost]
        [Transaction]
        public virtual ActionResult _EquiposPorJugador(int idJugador, string tipo)
        {
            var viewModel = this.equiposQuery.GetEquiposPorJugadorList(idJugador, tipo);
            return new JsonNetResult(viewModel);
        }

        [HttpPost]
        [Transaction]
        [CustomValidateAntiForgeryTokenAttribute]
        public virtual ActionResult _VerificarJugador(int idEquipo)
        {
            var command = new ValidarJugadorEnEquipoCommand(idEquipo, ((PadelPrincipal)User).Id);
            var results = this.commandProcessor.Process<ValidarJugadorEnEquipoCommand, CommandResult>(command);
            return new JsonNetResult(results.First());
            
        }

        [HttpPost]
        [Transaction]
        [CustomValidateAntiForgeryTokenAttribute]
        public virtual ActionResult _EliminarEquipo(int idEquipo)
        {
            var command = new EliminarEquipoCommand(idEquipo, ((PadelPrincipal)User).Id);
            var results = this.commandProcessor.Process<EliminarEquipoCommand, CommandResult>(command);
            return new JsonNetResult(results.First());

        }
    }
}
