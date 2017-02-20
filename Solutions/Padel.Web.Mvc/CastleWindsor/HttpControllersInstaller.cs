namespace Padel.Web.Mvc.CastleWindsor
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Controllers;
    using SharpArch.Web.Http.Castle;

    public class HttpControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.RegisterHttpControllers(typeof(ApiController).Assembly);
        }
    }
}