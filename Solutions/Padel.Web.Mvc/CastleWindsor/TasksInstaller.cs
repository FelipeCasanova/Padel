using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SharpArch.Web.Mvc.Castle;

namespace Padel.Web.Mvc.CastleWindsor
{
    public class TasksInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Types
                    .FromAssemblyNamed("Padel.Tasks")
                    .Pick().If(t => t.Name.EndsWith("Tasks"))
                    .WithService.FirstNonGenericCoreInterface("Padel.Domain")
                    .LifestyleTransient());
        }
    }
}