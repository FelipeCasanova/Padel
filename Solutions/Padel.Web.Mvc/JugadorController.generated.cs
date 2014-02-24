// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Padel.Web.Mvc.Controllers
{
    public partial class JugadorController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected JugadorController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }


        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public JugadorController Actions { get { return MVC.Jugador; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Jugador";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Jugador";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string GetOperacionesPorUsuario = "GetOperacionesPorUsuario";
            public readonly string GetNotificacionesPorUsuario = "GetNotificacionesPorUsuario";
            public readonly string Datos = "Datos";
            public readonly string Equipos = "Equipos";
            public readonly string Partidos = "Partidos";
            public readonly string Torneos = "Torneos";
            public readonly string Graficas = "Graficas";
            public readonly string _TorneosResumenPorJugador = "_TorneosResumenPorJugador";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string GetOperacionesPorUsuario = "GetOperacionesPorUsuario";
            public const string GetNotificacionesPorUsuario = "GetNotificacionesPorUsuario";
            public const string Datos = "Datos";
            public const string Equipos = "Equipos";
            public const string Partidos = "Partidos";
            public const string Torneos = "Torneos";
            public const string Graficas = "Graficas";
            public const string _TorneosResumenPorJugador = "_TorneosResumenPorJugador";
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
                public readonly string Datos = "Datos";
                public readonly string Equipos = "Equipos";
                public readonly string Graficas = "Graficas";
                public readonly string Index = "Index";
                public readonly string Partidos = "Partidos";
                public readonly string Torneos = "Torneos";
            }
            public readonly string Datos = "~/Views/Jugador/Datos.cshtml";
            public readonly string Equipos = "~/Views/Jugador/Equipos.cshtml";
            public readonly string Graficas = "~/Views/Jugador/Graficas.cshtml";
            public readonly string Index = "~/Views/Jugador/Index.cshtml";
            public readonly string Partidos = "~/Views/Jugador/Partidos.cshtml";
            public readonly string Torneos = "~/Views/Jugador/Torneos.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_JugadorController : Padel.Web.Mvc.Controllers.JugadorController
    {
        public T4MVC_JugadorController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        partial void GetOperacionesPorUsuarioOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult GetOperacionesPorUsuario()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetOperacionesPorUsuario);
            GetOperacionesPorUsuarioOverride(callInfo);
            return callInfo;
        }

        partial void GetNotificacionesPorUsuarioOverride(T4MVC_System_Web_Mvc_PartialViewResult callInfo);

        public override System.Web.Mvc.PartialViewResult GetNotificacionesPorUsuario()
        {
            var callInfo = new T4MVC_System_Web_Mvc_PartialViewResult(Area, Name, ActionNames.GetNotificacionesPorUsuario);
            GetNotificacionesPorUsuarioOverride(callInfo);
            return callInfo;
        }

        partial void DatosOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Datos()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Datos);
            DatosOverride(callInfo);
            return callInfo;
        }

        partial void EquiposOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Equipos()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Equipos);
            EquiposOverride(callInfo);
            return callInfo;
        }

        partial void PartidosOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Partidos()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Partidos);
            PartidosOverride(callInfo);
            return callInfo;
        }

        partial void TorneosOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Torneos()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Torneos);
            TorneosOverride(callInfo);
            return callInfo;
        }

        partial void GraficasOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Graficas()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Graficas);
            GraficasOverride(callInfo);
            return callInfo;
        }

        partial void _TorneosResumenPorJugadorOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult _TorneosResumenPorJugador()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._TorneosResumenPorJugador);
            _TorneosResumenPorJugadorOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
