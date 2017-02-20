using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Infrastructure.Utilities;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Tasks.Commands.Equipos;
using Padel.Web.Mvc.Controllers.Queries.Equipos;
using Padel.Web.Mvc.Filters;
using SharpArch.Web.Mvc.JsonNet;
using MediatR;
using SharpArch.Web.Mvc;

namespace Padel.Web.Mvc.Controllers
{
    [Authorize(Roles = "Administrador, Jugador")]
    public partial class EquiposController : Controller
    {
        private readonly IMediator mediator;
        private readonly IEquiposQuery equiposQuery;

        public EquiposController(IMediator commandProcessor, IEquiposQuery equiposQuery)
        {
            this.mediator = commandProcessor;
            this.equiposQuery = equiposQuery;
        }

        [HttpPost]
        [Transaction]
        public virtual ActionResult _EquiposPorJugador(string tipo)
        {
            var viewModel = this.equiposQuery.GetEquiposPorJugadorList(((PadelPrincipal)User).Id, tipo);
            return new JsonNetResult(viewModel);
        }

        [HttpPost]
        [Transaction]
        [CustomValidateAntiForgeryTokenAttribute]
        public virtual ActionResult _VerificarJugador(int idEquipo)
        {
            var command = new ValidarJugadorEnEquipoCommand(idEquipo, ((PadelPrincipal)User).Id);
            var result = this.mediator.Send<CommandResult>(command).Result;
            return new JsonNetResult(result);
            
        }

        
            [HttpPost]
        [Transaction]
        [CustomValidateAntiForgeryTokenAttribute]
        public virtual ActionResult _AsignarJugadorSeleccionadoAlEquipo(int idJugador)
        {
            var command = new CrearEquipoCommand(((PadelPrincipal)User).Id, idJugador);
            var result = this.mediator.Send<CommandResult>(command).Result;
            return new JsonNetResult(result);
        }

        [HttpPost]
        [Transaction]
        [CustomValidateAntiForgeryTokenAttribute]
        public virtual ActionResult _EliminarEquipo(int idEquipo)
        {
            var command = new EliminarEquipoCommand(idEquipo);
            var result = this.mediator.Send<CommandResult>(command).Result;
            return new JsonNetResult(result);

        }
    }
}
