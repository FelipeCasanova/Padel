namespace Padel.Web.Mvc.CastleWindsor
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using MediatR;
    using Padel.Tasks.CommandHandlers.Usuarios;
    using Padel.Tasks.EventHandlers.Usuarios;

    /// <summary>
    /// Installs Command and Query handlers.
    /// </summary>
    public class HandlersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<EntrarUsuarioCommandHandler>()
                    .BasedOn(typeof(IRequestHandler<>))
                    .WithService.AllInterfaces()
                    .LifestylePerWebRequest());

            container.Register(
                Classes.FromAssemblyContaining<EntrarUsuarioCommandHandler>()
                    .BasedOn(typeof(IRequestHandler<,>))
                    .WithService.AllInterfaces()
                    .LifestylePerWebRequest());

            container.Register(
                Classes.FromAssemblyContaining<EntrarEventHandler>()
                    .BasedOn(typeof(INotificationHandler<>))
                    .WithService.AllInterfaces()
                    .LifestylePerWebRequest());
        }
    }
}
