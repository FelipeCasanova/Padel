// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
// 0114: suppress "Foo.BarController.Baz()' hides inherited member 'Qux.BarController.Baz()'. To make the current member override that implementation, add the override keyword. Otherwise add the new keyword." when an action (with an argument) overrides an action in a parent controller
#pragma warning disable 1591, 3008, 3009, 0108, 0114
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Padel.Web.Mvc.Controllers
{
    public partial class EquiposController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected EquiposController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _EquiposPorJugador()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._EquiposPorJugador);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _VerificarJugador()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._VerificarJugador);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _AsignarJugadorSeleccionadoAlEquipo()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._AsignarJugadorSeleccionadoAlEquipo);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _EliminarEquipo()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._EliminarEquipo);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public EquiposController Actions { get { return MVC.Equipos; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Equipos";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Equipos";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string _EquiposPorJugador = "_EquiposPorJugador";
            public readonly string _VerificarJugador = "_VerificarJugador";
            public readonly string _AsignarJugadorSeleccionadoAlEquipo = "_AsignarJugadorSeleccionadoAlEquipo";
            public readonly string _EliminarEquipo = "_EliminarEquipo";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string _EquiposPorJugador = "_EquiposPorJugador";
            public const string _VerificarJugador = "_VerificarJugador";
            public const string _AsignarJugadorSeleccionadoAlEquipo = "_AsignarJugadorSeleccionadoAlEquipo";
            public const string _EliminarEquipo = "_EliminarEquipo";
        }


        static readonly ActionParamsClass__EquiposPorJugador s_params__EquiposPorJugador = new ActionParamsClass__EquiposPorJugador();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__EquiposPorJugador _EquiposPorJugadorParams { get { return s_params__EquiposPorJugador; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__EquiposPorJugador
        {
            public readonly string tipo = "tipo";
        }
        static readonly ActionParamsClass__VerificarJugador s_params__VerificarJugador = new ActionParamsClass__VerificarJugador();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__VerificarJugador _VerificarJugadorParams { get { return s_params__VerificarJugador; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__VerificarJugador
        {
            public readonly string idEquipo = "idEquipo";
        }
        static readonly ActionParamsClass__AsignarJugadorSeleccionadoAlEquipo s_params__AsignarJugadorSeleccionadoAlEquipo = new ActionParamsClass__AsignarJugadorSeleccionadoAlEquipo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__AsignarJugadorSeleccionadoAlEquipo _AsignarJugadorSeleccionadoAlEquipoParams { get { return s_params__AsignarJugadorSeleccionadoAlEquipo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__AsignarJugadorSeleccionadoAlEquipo
        {
            public readonly string idJugador = "idJugador";
        }
        static readonly ActionParamsClass__EliminarEquipo s_params__EliminarEquipo = new ActionParamsClass__EliminarEquipo();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__EliminarEquipo _EliminarEquipoParams { get { return s_params__EliminarEquipo; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__EliminarEquipo
        {
            public readonly string idEquipo = "idEquipo";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
            }
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_EquiposController : Padel.Web.Mvc.Controllers.EquiposController
    {
        public T4MVC_EquiposController() : base(Dummy.Instance) { }

        [NonAction]
        partial void _EquiposPorJugadorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string tipo);

        [NonAction]
        public override System.Web.Mvc.ActionResult _EquiposPorJugador(string tipo)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._EquiposPorJugador);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "tipo", tipo);
            _EquiposPorJugadorOverride(callInfo, tipo);
            return callInfo;
        }

        [NonAction]
        partial void _VerificarJugadorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int idEquipo);

        [NonAction]
        public override System.Web.Mvc.ActionResult _VerificarJugador(int idEquipo)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._VerificarJugador);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "idEquipo", idEquipo);
            _VerificarJugadorOverride(callInfo, idEquipo);
            return callInfo;
        }

        [NonAction]
        partial void _AsignarJugadorSeleccionadoAlEquipoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int idJugador);

        [NonAction]
        public override System.Web.Mvc.ActionResult _AsignarJugadorSeleccionadoAlEquipo(int idJugador)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._AsignarJugadorSeleccionadoAlEquipo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "idJugador", idJugador);
            _AsignarJugadorSeleccionadoAlEquipoOverride(callInfo, idJugador);
            return callInfo;
        }

        [NonAction]
        partial void _EliminarEquipoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int idEquipo);

        [NonAction]
        public override System.Web.Mvc.ActionResult _EliminarEquipo(int idEquipo)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._EliminarEquipo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "idEquipo", idEquipo);
            _EliminarEquipoOverride(callInfo, idEquipo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
