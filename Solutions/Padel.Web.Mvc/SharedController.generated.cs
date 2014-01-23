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
namespace T4MVC
{
    public class SharedController
    {

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
                public readonly string _Carousel = "_Carousel";
                public readonly string _Footer = "_Footer";
                public readonly string _Layout = "_Layout";
                public readonly string _LayoutLogin = "_LayoutLogin";
                public readonly string _NavBar = "_NavBar";
            }
            public readonly string _Carousel = "~/Views/Shared/_Carousel.cshtml";
            public readonly string _Footer = "~/Views/Shared/_Footer.cshtml";
            public readonly string _Layout = "~/Views/Shared/_Layout.cshtml";
            public readonly string _LayoutLogin = "~/Views/Shared/_LayoutLogin.cshtml";
            public readonly string _NavBar = "~/Views/Shared/_NavBar.cshtml";
            static readonly _DisplayTemplatesClass s_DisplayTemplates = new _DisplayTemplatesClass();
            public _DisplayTemplatesClass DisplayTemplates { get { return s_DisplayTemplates; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _DisplayTemplatesClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                }
            }
            static readonly _ErrorsClass s_Errors = new _ErrorsClass();
            public _ErrorsClass Errors { get { return s_Errors; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _ErrorsClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string _Error = "_Error";
                    public readonly string _ModalError = "_ModalError";
                }
                public readonly string _Error = "~/Views/Shared/Errors/_Error.cshtml";
                public readonly string _ModalError = "~/Views/Shared/Errors/_ModalError.cshtml";
            }
            static readonly _TorneoClass s_Torneo = new _TorneoClass();
            public _TorneoClass Torneo { get { return s_Torneo; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _TorneoClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string _Currency = "_Currency";
                    public readonly string _Preview = "_Preview";
                }
                public readonly string _Currency = "~/Views/Shared/Torneo/_Currency.cshtml";
                public readonly string _Preview = "~/Views/Shared/Torneo/_Preview.cshtml";
            }
            static readonly _UsuariosClass s_Usuarios = new _UsuariosClass();
            public _UsuariosClass Usuarios { get { return s_Usuarios; } }
            [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
            public partial class _UsuariosClass
            {
                static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
                public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
                public class _ViewNamesClass
                {
                    public readonly string _Entrar = "_Entrar";
                    public readonly string _Registrar = "_Registrar";
                }
                public readonly string _Entrar = "~/Views/Shared/Usuarios/_Entrar.cshtml";
                public readonly string _Registrar = "~/Views/Shared/Usuarios/_Registrar.cshtml";
            }
        }
    }

}

#endregion T4MVC
#pragma warning restore 1591
