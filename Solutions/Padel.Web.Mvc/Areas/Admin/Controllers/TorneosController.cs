using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Padel.Domain.Contracts.Tasks;
using Padel.Infrastructure.Utilities.Emails;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands.Torneos;
using Padel.Web.Mvc.Areas.Admin.Controllers.Queries.Torneos;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Menus;
using Padel.Web.Mvc.Areas.Admin.Controllers.ViewModels.Torneos;
using SharpArch.Domain.Commands;
using SharpArch.NHibernate.Web.Mvc;
using Padel.Domain;
using System.Text;
using Padel.Web.Mvc.Filters;
using Microsoft.Web.Mvc;
using SharpArch.Web.Mvc.JsonNet;

namespace Padel.Web.Mvc.Areas.Admin.Controllers
{
    public partial class TorneosController : BaseController
    {
        private readonly ICommandProcessor commandProcessor;

        private readonly IEmailTasks emailTasks;

        private readonly ITorneosQuery torneosQuery;


        public TorneosController(ICommandProcessor commandProcessor, IEmailTasks emailTasks, ITorneosQuery torneosQuery)
        {
            this.commandProcessor = commandProcessor;
            this.emailTasks = emailTasks;
            this.torneosQuery = torneosQuery;
        }

        public override string GetPageTitle()
        {
            return MVC.Admin.Torneos.Name;
        }

        public override void SetBreadcrumb(ViewModels.Menus.BreadcrumbModelView breadcrumbModelView)
        {
            breadcrumbModelView.Add(new BreadcrumbItem { Name = MVC.Admin.Torneos.Name, Url = Url.Action(MVC.Admin.Torneos.Actions.ActionNames.Index, MVC.Admin.Torneos.Name) });
        }

        public override void SetActiveSideMenuItem(SideBarModelView SideBarModelView)
        {
            var menuItem = SideBarModelView.First(m => m.Name == MVC.Admin.Torneos.Name);
            menuItem.IsSelected = true;
        }

        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Transaction]
        public virtual ActionResult _Listado(int? page, int? size)
        {
            var viewModel = this.torneosQuery.GetTorneosList(page ?? 1, size ?? int.MaxValue);
            return new JsonNetResult(viewModel);
        }

        [HttpGet]
        public virtual ActionResult Verificar()
        {
            AddBreadScrum(new BreadcrumbItem { Name = MVC.Admin.Torneos.ActionNames.Verificar, Url = Url.Action(MVC.Admin.Torneos.Actions.ActionNames.Verificar, MVC.Admin.Torneos.Name) });
            return View();
        }

        [AjaxOnly]
        [HttpPost]
        [Transaction]
        [CustomValidateAntiForgeryTokenAttribute]
        public virtual ActionResult _Verificar(int idCategoria)
        {
            var command = new VerificarTorneoCommand(idCategoria);
            var result = this.commandProcessor.Process<VerificarTorneoCommand, CommandResult>(command).First();

            if (!result.Success)
            {
                List<EquipoToCategoria> equiposToCategorias = (List<EquipoToCategoria>)result.Result;
                foreach (var equipoToCategoria in equiposToCategorias)
                {
                    // Enviar email
                    // Generamos el html del email
                    VerificarTorneoEmailModelView modelview = new VerificarTorneoEmailModelView()
                    {
                        NombreTorneo = equipoToCategoria.Categoria.Torneo.Nombre,
                        NombreCategoria = equipoToCategoria.Categoria.Nombre,
                        NombreEquipo = equipoToCategoria.Equipo.ToString()
                    };
                    var emails = new StringBuilder();
                    emails.Append(equipoToCategoria.Equipo.JugadorA.Email).Append(";").Append(equipoToCategoria.Equipo.JugadorB.Email);
                    var viewString = View(MVC.Admin.Emails.Views.VerificarTorneo, modelview).Capture(ControllerContext);
                    emailTasks.EnviarNotificacion("Padel - Verificar Torneo", viewString, emails.ToString());    
                }
                
            }

            result.Result = null;
            return new JsonNetResult(result);
        }
        

        [HttpPost]
        [Transaction]
        public virtual ActionResult _ListadoPendientes(int? page, int? size)
        {
            var viewModel = this.torneosQuery.GetTorneosList(page ?? 1, size ?? int.MaxValue, EstadoCategoriaEnum.Pendiente, EstadoCategoriaEnum.Verificado);
            return new JsonNetResult(viewModel);
        }
    }
}
