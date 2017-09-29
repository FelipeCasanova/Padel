namespace Padel.Web.Mvc
{
    using System;
    using System.Reflection;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Castle.Windsor;

    // Padel.Web.Mvc.CastleWindsor
    using CastleWindsor;

    using CommonServiceLocator.WindsorAdapter;

    using Controllers;

    using Infrastructure.NHibernateMaps;

    using log4net.Config;

    using Microsoft.Practices.ServiceLocation;

    using SharpArch.NHibernate;
    using SharpArch.Web.Mvc.Castle;
    using SharpArch.Web.Mvc.ModelBinder;
    using System.ComponentModel.DataAnnotations;
    using Padel.Web.Mvc.Validation;
    using Padel.Web.Mvc.App_Start;
    using System.Configuration;
    using Microsoft.IdentityModel.Web;
    using System.Web.Optimization;
    using SharpArch.Domain.Reflection;
    using Castle.Windsor.Installer;


    /// <summary>
    /// Represents the MVC Application
    /// </summary>
    /// <remarks>
    /// For instructions on enabling IIS6 or IIS7 classic mode, 
    /// visit http://go.microsoft.com/?LinkId=9394801
    /// </remarks>
    public class MvcApplication : System.Web.HttpApplication
    {
        private TypePropertyDescriptorCache injectablePropertiesCache;
        private IWindsorContainer container;

        protected void Application_Error(object sender, EventArgs e) 
        {
            // Useful for debugging
            Exception ex = this.Server.GetLastError();
            var reflectionTypeLoadException = ex as ReflectionTypeLoadException;
        }

        protected void Application_Start()
        {
            XmlConfigurator.Configure();

            AutoMapper.Mapper.Initialize(c =>
            {
               
            });

            // Container
            injectablePropertiesCache = new TypePropertyDescriptorCache();
            container = InitializeContainer();

            // MVC
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());

            ModelBinders.Binders.DefaultBinder = new SharpModelBinder();
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(DataTypeAttribute), typeof(DataTypeAttributeAdapter));

            FilterProviders.Providers.InstallMvcFilterProvider(container, injectablePropertiesCache);
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));

            AreaRegistration.RegisterAllAreas();

            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteRegistrar.RegisterRoutesTo(RouteTable.Routes);

            /*JsonConvert.DefaultSettings = (() =>
            {
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                return settings;
            });*/

            Application["NewUserLimitMonths"] = int.Parse(ConfigurationManager.AppSettings["NewUserLimitMonths"]);
        }

        /// <summary>
        ///     Instantiate the container and add all Controllers that derive from
        ///     WindsorController to the container. Also associate the Controller
        ///     with the WindsorContainer ControllerFactory.
        /// </summary>
        protected IWindsorContainer InitializeContainer()
        {
            IWindsorContainer c = new WindsorContainer();
            c.Install(FromAssembly.This());
            return c;
        }

        void SessionAuthenticationModule_SessionSecurityTokenReceived(object sender, SessionSecurityTokenReceivedEventArgs e)
        {
            DateTime now = DateTime.UtcNow;
            DateTime validFrom = e.SessionToken.ValidFrom;
            DateTime validTo = e.SessionToken.ValidTo;
            if ((now < validTo) && (new TimeSpan(0,10,0) > (validTo - now)))
            {
                SessionAuthenticationModule sam = sender as SessionAuthenticationModule;
                e.SessionToken = sam.CreateSessionSecurityToken(
                    e.SessionToken.ClaimsPrincipal,
                    e.SessionToken.Context,
                    now,
                    now.AddMinutes(30),
                    e.SessionToken.IsPersistent);
                e.ReissueCookie = true;
            }
            else
            {
                if (now > validTo)
                {
                    var sessionAuthenticationModule = (SessionAuthenticationModule)sender;

                    sessionAuthenticationModule.DeleteSessionTokenCookie();

                    e.Cancel = true;
                }
            }
        } 
    }
}