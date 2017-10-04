namespace Padel.Web.Mvc.CastleWindsor
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using Controllers;
    using SharpArch.Web.Mvc.Castle;
    using System.Web.Mvc;

    public class MvcControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.RegisterMvcControllers(typeof(BaseController).Assembly);
            container.Register(Classes.FromAssemblyNamed("Elmah.Mvc")
                .BasedOn<IController>()
                .LifestyleTransient());
        }
    }
}