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
namespace Padel.Web.Mvc.Areas.Admin.Controllers
{
    public partial class TorneosController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected TorneosController(Dummy d) { }

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
        public virtual System.Web.Mvc.ActionResult _Listado()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._Listado);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _Verificar()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._Verificar);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult _ListadoPendientes()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._ListadoPendientes);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public TorneosController Actions { get { return MVC.Admin.Torneos; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "Admin";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Torneos";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Torneos";
        [GeneratedCode("T4MVC", "2.0")]
        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string _Listado = "_Listado";
            public readonly string Verificar = "Verificar";
            public readonly string _Verificar = "_Verificar";
            public readonly string _ListadoPendientes = "_ListadoPendientes";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string _Listado = "_Listado";
            public const string Verificar = "Verificar";
            public const string _Verificar = "_Verificar";
            public const string _ListadoPendientes = "_ListadoPendientes";
        }


        static readonly ActionParamsClass__Listado s_params__Listado = new ActionParamsClass__Listado();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__Listado _ListadoParams { get { return s_params__Listado; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__Listado
        {
            public readonly string page = "page";
            public readonly string size = "size";
        }
        static readonly ActionParamsClass__Verificar s_params__Verificar = new ActionParamsClass__Verificar();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__Verificar _VerificarParams { get { return s_params__Verificar; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__Verificar
        {
            public readonly string idCategoria = "idCategoria";
        }
        static readonly ActionParamsClass__ListadoPendientes s_params__ListadoPendientes = new ActionParamsClass__ListadoPendientes();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass__ListadoPendientes _ListadoPendientesParams { get { return s_params__ListadoPendientes; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass__ListadoPendientes
        {
            public readonly string page = "page";
            public readonly string size = "size";
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
                public readonly string Index = "Index";
                public readonly string Verificar = "Verificar";
            }
            public readonly string Index = "~/Areas/Admin/Views/Torneos/Index.cshtml";
            public readonly string Verificar = "~/Areas/Admin/Views/Torneos/Verificar.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_TorneosController : Padel.Web.Mvc.Areas.Admin.Controllers.TorneosController
    {
        public T4MVC_TorneosController() : base(Dummy.Instance) { }

        [NonAction]
        partial void IndexOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Index()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Index);
            IndexOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void _ListadoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? page, int? size);

        [NonAction]
        public override System.Web.Mvc.ActionResult _Listado(int? page, int? size)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._Listado);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "size", size);
            _ListadoOverride(callInfo, page, size);
            return callInfo;
        }

        [NonAction]
        partial void VerificarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Verificar()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Verificar);
            VerificarOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void _VerificarOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int idCategoria);

        [NonAction]
        public override System.Web.Mvc.ActionResult _Verificar(int idCategoria)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._Verificar);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "idCategoria", idCategoria);
            _VerificarOverride(callInfo, idCategoria);
            return callInfo;
        }

        [NonAction]
        partial void _ListadoPendientesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? page, int? size);

        [NonAction]
        public override System.Web.Mvc.ActionResult _ListadoPendientes(int? page, int? size)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames._ListadoPendientes);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "page", page);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "size", size);
            _ListadoPendientesOverride(callInfo, page, size);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108, 0114
