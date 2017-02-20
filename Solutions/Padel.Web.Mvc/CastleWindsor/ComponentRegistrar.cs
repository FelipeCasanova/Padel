namespace Padel.Web.Mvc.CastleWindsor
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using SharpArch.Domain.PersistenceSupport;
    using SharpArch.NHibernate;
    using SharpArch.NHibernate.Contracts.Repositories;
    using SharpArch.Web.Mvc.Castle;
    using System.Web.Mvc;

    public class ComponentRegistrar
    {
        public static void AddComponentsTo(IWindsorContainer container)
        {
            //AddElmah(container);
            AddGenericRepositoriesTo(container);
            AddCustomRepositoriesTo(container);
            //AddQueryObjectsTo(container);
            AddTasksTo(container);
            //AddHandlersTo(container);
        }

        //private static void AddElmah(IWindsorContainer container)
        //{
        //    container.Register(Classes.FromAssemblyNamed("Elmah.Mvc")
        //    .BasedOn<IController>()
        //    .LifestyleTransient());
        //}

        private static void AddTasksTo(IWindsorContainer container)
        {
            //container.Register(
            //    AllTypes
            //        .FromAssemblyNamed("Padel.Tasks")
            //        .Pick().If(t => t.Name.EndsWith("Tasks"))
            //        .WithService.FirstNonGenericCoreInterface("Padel.Domain"));
        }

        private static void AddCustomRepositoriesTo(IWindsorContainer container)
        {
            //container.Register(
            //    AllTypes
            //        .FromAssemblyNamed("Padel.Infrastructure")
            //        .BasedOn(typeof(IRepositoryWithTypedId<,>))
            //        .WithService.FirstNonGenericCoreInterface("Padel.Domain"));
        }

        private static void AddGenericRepositoriesTo(IWindsorContainer container)
        {
            //container.Register(
            //    Component.For(typeof(IEntityDuplicateChecker))
            //        .ImplementedBy(typeof(EntityDuplicateChecker))
            //        .Named("entityDuplicateChecker"));

            //container.Register(
            //    Component.For(typeof(INHibernateRepository<>))
            //        .ImplementedBy(typeof(NHibernateRepository<>))
            //        .Named("nhibernateRepositoryType")
            //        .Forward(typeof(IRepository<>)));

            //container.Register(
            //    Component.For(typeof(INHibernateRepositoryWithTypedId<,>))
            //        .ImplementedBy(typeof(NHibernateRepositoryWithTypedId<,>))
            //        .Named("nhibernateRepositoryWithTypedId")
            //        .Forward(typeof(IRepositoryWithTypedId<,>)));

            //container.Register(
            //        Component.For(typeof(ISessionFactoryKeyProvider))
            //            .ImplementedBy(typeof(DefaultSessionFactoryKeyProvider))
            //            .Named("sessionFactoryKeyProvider"));

            //container.Register(
            //        Component.For(typeof(ICommandProcessor))
            //            .ImplementedBy(typeof(CommandProcessor))
            //            .Named("commandProcessor"));
        }

        //private static void AddQueryObjectsTo(IWindsorContainer container)
        //{
        //    container.Register(
        //        AllTypes.FromAssemblyNamed("Padel.Web.Mvc")
        //            .BasedOn<NHibernateQuery>()
        //            .WithService.DefaultInterfaces());

        //    container.Register(
        //        AllTypes.FromAssemblyNamed("Padel.Infrastructure")
        //            .BasedOn(typeof(NHibernateQuery))
        //            .WithService.DefaultInterfaces());
        //}

        //private static void AddHandlersTo(IWindsorContainer container)
        //{
        //    container.Register(
        //        AllTypes.FromAssemblyNamed("Padel.Tasks")
        //            .BasedOn(typeof(ICommandHandler<>))
        //            .WithService.FirstInterface());

        //    container.Register(
        //        AllTypes.FromAssemblyNamed("Padel.Tasks")
        //            .BasedOn(typeof(ICommandHandler<,>))
        //            .WithService.FirstInterface());

        //    container.Register(
        //        AllTypes.FromAssemblyNamed("Padel.Tasks")
        //            .BasedOn(typeof(IHandles<>))
        //            .WithService.FirstInterface());
        //}
    }
}