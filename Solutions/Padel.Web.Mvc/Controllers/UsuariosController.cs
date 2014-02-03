using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Web;
using Microsoft.Web.Mvc;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;
using Padel.Infrastructure.Utilities;
using Padel.Tasks;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Web.Mvc.Controllers.Queries.Equipos;
using Padel.Web.Mvc.Controllers.ViewModels;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Web.Mvc;

namespace Padel.Web.Mvc.Controllers
{
    [Authorize(Roles="Administrador, Jugador")]
    public partial class UsuariosController : BaseController
    {
        private readonly ICommandProcessor commandProcessor;

        private readonly IUsuarioTasks usuarioTasks;

        private readonly IEquiposQuery equiposQuery;

        private readonly IJugadoresQuery jugadoresQuery;

        public UsuariosController(ICommandProcessor commandProcessor, IUsuarioTasks usuarioTasks, IEquiposQuery equiposQuery, IJugadoresQuery jugadoresQuery)
        {
            this.commandProcessor = commandProcessor;
            this.usuarioTasks = usuarioTasks;
            this.equiposQuery = equiposQuery;
            this.jugadoresQuery = jugadoresQuery;
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [NonAction]
        public override void SetTopMenu(ViewModels.Menu.MenuModelView modelView)
        {
            modelView.TopMenu = ViewModels.Menu.TopMenu.Home;
        }

        /// <summary>
        /// Registrar al usuario
        /// </summary>
        /// <param name="usuario">Entidad usuario.</param>
        /// <returns>Devuelve el model view para el usuario.</returns>
        [AjaxOnly]
        [HttpPost]
        [Transaction]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult _Registrar(UsuarioRegistrarModelView usuarioModelView)
        {
            ViewBag.ErrorResult = PartialView(MVC.Shared.Views.Usuarios._Registrar);

            // No queremos validar aquí los roles
            ModelState.Remove("usuario.Roles");

            if (ModelState.IsValid)
            {
                var command = new RegistrarUsuarioCommand(usuarioModelView.Usuario.Nombre, usuarioModelView.Usuario.Sexo,
                    usuarioModelView.TelefonoMovil, usuarioModelView.Email, usuarioModelView.Password);
                var results = this.commandProcessor.Process<RegistrarUsuarioCommand, CommandResult>(command);

                if (results.First().Success)
                {
                    var usuarioDB = usuarioTasks.GetByMovil(usuarioModelView.TelefonoMovil);
                    var token = FederatedAuthentication.SessionAuthenticationModule.CreateSessionSecurityToken(ClaimsPrincipalUtility.CreatePrincipal(usuarioDB),
                        "PadelContext", DateTime.Now, DateTime.Now.AddHours(1), true);
                    FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(token);
                    return JavaScript("window.location = '" + Url.Action(MVC.Home.ActionNames.Index, MVC.Home.Name) +"';");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El usuario no se pudo crear. Inténtelo más tarde.");
                }
            }

            return PartialView(MVC.Shared.Views.Usuarios._Registrar, usuarioModelView);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Entrar(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AjaxOnly]
        [HttpPost]
        [Transaction]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult _Entrar(UsuarioEntrarModelView usuarioModelView, string returnUrl)
        {
            ViewBag.ErrorResult = PartialView(MVC.Shared.Views.Usuarios._Entrar);

            if (ModelState.IsValid)
            {
                var command = new EntrarUsuarioCommand(usuarioModelView.EmailOTelefonoMovil, usuarioModelView.PasswordEntrar);
                var results = this.commandProcessor.Process<EntrarUsuarioCommand, CommandResult>(command);

                if (results.First().Success)
                {
                    var usuarioDB = usuarioTasks.GetByMovil(command.TelefonoMovil);

                    var token = FederatedAuthentication.SessionAuthenticationModule.CreateSessionSecurityToken(ClaimsPrincipalUtility.CreatePrincipal(usuarioDB),
                        "PadelContext", DateTime.Now, DateTime.Now.AddHours(1), true);
                    FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(token);

                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        if (User.IsInRole("Administrador"))
                        {
                            if (Request.UrlReferrer != null)
                            {
                                return JavaScript("window.location = '" + Request.UrlReferrer.AbsoluteUri + "';");
                            }
                            return JavaScript("window.location = '" + Url.Action(MVC.Admin.HomeAdmin.ActionNames.Index, MVC.Admin.HomeAdmin.Name, new { area = "Admin" }) + "';");
                        }
                        else
                        {
                            if (Request.UrlReferrer != null)
                            {
                                return JavaScript("window.location = '" + Request.UrlReferrer.AbsoluteUri + "';");
                            }
                            return JavaScript("window.location = '" + Url.Action(MVC.Home.ActionNames.Index, MVC.Home.Name) + "';");
                        }
                    }
                    else
                    {
                        return JavaScript("window.location = '" + returnUrl + "';");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El usuario no es válido. Compruebe los datos.");
                }
            }

            return PartialView(MVC.Shared.Views.Usuarios._Entrar);
        }

        [AllowAnonymous]
        public virtual JsonResult ValidarTelefonoUnico(int? telefonoMovil)
        {
            if (telefonoMovil != null)
            {
                var numeroUsuarios = usuarioTasks.GetNumeroUsuariosByMovil(telefonoMovil.GetValueOrDefault());
                if (numeroUsuarios > 0)
                {
                    return Json("El número de teléfono ya existe. Pruebe con otro número diferente.", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public virtual JsonResult ValidarEmailUnico(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var numeroUsuarios = usuarioTasks.GetNumeroUsuariosByEmail(email);
                if (numeroUsuarios > 0)
                {
                    return Json("El email ya existe. Pruebe con otro email diferente.", JsonRequestBehavior.AllowGet);
                }
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public virtual ActionResult Logout()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
        }

        [HttpPost]
        [Transaction]
        public virtual ActionResult _EquiposPorJugador(int idJugador)
        {
            var viewModel = this.equiposQuery.GetEquiposPorJugadorList(idJugador);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Transaction]
        public virtual ActionResult _JugadorPorNombre(string nombreJugador)
        {
            var viewModel = this.jugadoresQuery.GetJugadorPorNombreList(nombreJugador).Take(10);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

    }
}
