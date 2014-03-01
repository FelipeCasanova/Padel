using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.IdentityModel.Web;
using Microsoft.Web.Mvc;
using MvcReCaptcha;
using Padel.Domain;
using Padel.Domain.Contracts.Tasks;
using Padel.Infrastructure.Utilities;
using Padel.Infrastructure.Utilities.Emails;
using Padel.Tasks;
using Padel.Tasks.CommandResults;
using Padel.Tasks.Commands;
using Padel.Tasks.Commands.Usuarios;
using Padel.Web.Mvc.Controllers.Queries.Usuarios;
using Padel.Web.Mvc.Controllers.ViewModels.Usuarios;
using SharpArch.Domain.Commands;
using SharpArch.Domain.PersistenceSupport;
using SharpArch.NHibernate.Web.Mvc;

namespace Padel.Web.Mvc.Controllers
{
    [Authorize(Roles = "Administrador, Jugador")]
    public partial class UsuariosController : BaseController
    {
        private readonly ICommandProcessor commandProcessor;

        private readonly IUsuarioTasks usuarioTasks;

        private readonly IEmailTasks emailTasks;

        private readonly IJugadoresQuery jugadoresQuery;

        public UsuariosController(ICommandProcessor commandProcessor, IUsuarioTasks usuarioTasks, IEmailTasks emailTasks, IJugadoresQuery jugadoresQuery)
        {
            this.commandProcessor = commandProcessor;
            this.usuarioTasks = usuarioTasks;
            this.emailTasks = emailTasks;
            this.jugadoresQuery = jugadoresQuery;
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
        public virtual ActionResult _Modificar(UsuarioDatosModelView usuarioModelView)
        {
            ViewBag.ErrorResult = PartialView(MVC.Shared.Views.Usuarios._Modificar, usuarioModelView);

            usuarioModelView.Usuario = this.usuarioTasks.Get(((PadelPrincipal)User).Id);
            this.TryUpdateModel(usuarioModelView);
            if (ModelState.IsValid)
            {
                this.usuarioTasks.CreateOrUpdate(usuarioModelView.Usuario);
                var command2 = new RefrescarUsuarioCommand();
                var result2 = this.commandProcessor.Process<RefrescarUsuarioCommand, CommandResult>(command2).First();
            }

            return PartialView(MVC.Shared.Views.Usuarios._Modificar, usuarioModelView);
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
        [CaptchaValidator()]
        public virtual ActionResult _Registrar(UsuarioRegistrarModelView usuarioModelView, bool captchaValid)
        {
            ViewBag.ErrorResult = PartialView(MVC.Shared.Views.Usuarios._Registrar, usuarioModelView);

            // No queremos validar aquí los roles
            ModelState.Remove("usuario.Roles");

            if (ModelState.IsValid && captchaValid)
            {
                var command = new RegistrarUsuarioCommand(usuarioModelView.Usuario.Nombre, usuarioModelView.Usuario.Sexo,
                    usuarioModelView.TelefonoMovil, usuarioModelView.Email, usuarioModelView.Password, GetVisitorIpAddress());
                var result = this.commandProcessor.Process<RegistrarUsuarioCommand, CommandResult>(command).First();

                if (result.Success)
                {
                    // Enviar email
                    // Generamos el html del email
                    RegistroEmailModelView modelview = new RegistroEmailModelView()
                    {
                        Nombre = usuarioModelView.Usuario.Nombre,
                        Email = usuarioModelView.Usuario.Email,
                        Password = usuarioModelView.Password
                    };
                    var viewString = View(MVC.Emails.Views.Registro, modelview).Capture(ControllerContext);
                    emailTasks.EnviarNotificacion("Padel - Registro", viewString, usuarioModelView.Email);

                    // Refrescar usuario
                    var command2 = new RefrescarUsuarioCommand(usuarioModelView.TelefonoMovil);
                    var result2 = this.commandProcessor.Process<RefrescarUsuarioCommand, CommandResult>(command2).First();

                    // Dar cinco puntos y refrescar usuario
                    if (bool.Parse(ConfigurationManager.AppSettings["Dar5puntosAlRegistrar"]) == true)
                    {
                        var command3 = new IngresarPuntosAUsuarioCommand(5, ((PadelPrincipal)User).Id);
                        var result3 = this.commandProcessor.Process<IngresarPuntosAUsuarioCommand, CommandResult>(command3).First();
                        if (result3.Success)
                        {
                            this.commandProcessor.Process<RefrescarUsuarioCommand, CommandResult>(new RefrescarUsuarioCommand()).First();
                        }
                    }

                    return JavaScript("window.location = '" + Url.Action(MVC.Home.ActionNames.Index, MVC.Home.Name) + "';");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El usuario no se pudo crear. Inténtelo más tarde.");
                }
            }
            else if (!captchaValid)
            {
                ModelState.AddModelError(string.Empty, "El usuario no se pudo crear. Rellene el campo captcha.");
            }

            return PartialView(MVC.Shared.Views.Usuarios._Registrar, usuarioModelView);
        }

        [NonAction]
        public string GetVisitorIpAddress()
        {
            string stringIpAddress;
            stringIpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (stringIpAddress == null) //may be the HTTP_X_FORWARDED_FOR is null
                stringIpAddress = Request.ServerVariables["REMOTE_ADDR"]; //we can use REMOTE_ADDR
            else if (stringIpAddress == null)
                stringIpAddress = GetLanIPAddress();

            return stringIpAddress;
        }

        [NonAction]
        //Get Lan Connected IP address method
        public string GetLanIPAddress()
        {
            //Get the Host Name
            string stringHostName = Dns.GetHostName();
            //Get The Ip Host Entry
            IPHostEntry ipHostEntries = Dns.GetHostEntry(stringHostName);
            //Get The Ip Address From The Ip Host Entry Address List
            System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;
            return arrIpAddress[arrIpAddress.Length - 1].ToString();
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
            ViewBag.ErrorResult = PartialView(MVC.Shared.Views.Usuarios._Entrar, usuarioModelView);

            if (ModelState.IsValid)
            {
                var command = new EntrarUsuarioCommand(usuarioModelView.EmailOTelefonoMovil, usuarioModelView.PasswordEntrar);
                var results = this.commandProcessor.Process<EntrarUsuarioCommand, CommandResult>(command);

                if (results.First().Success)
                {
                    var command2 = new RefrescarUsuarioCommand(command.TelefonoMovil);
                    var result2 = this.commandProcessor.Process<RefrescarUsuarioCommand, CommandResult>(command2).First();

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
        public virtual ActionResult Logout()
        {
            FederatedAuthentication.SessionAuthenticationModule.SignOut();
            return Redirect(Request.UrlReferrer.AbsoluteUri);
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
