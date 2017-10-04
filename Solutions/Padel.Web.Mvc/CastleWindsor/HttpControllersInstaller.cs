namespace Padel.Web.Mvc.CastleWindsor
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Controllers;
    using SharpArch.Web.Http.Castle;
    using System.Web.Http;

    public class HttpControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // For Web API
            //container.RegisterHttpControllers(typeof(BaseController).Assembly);
        }
    }
}