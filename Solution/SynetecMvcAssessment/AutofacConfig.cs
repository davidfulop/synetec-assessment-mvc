using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using InterviewTestTemplatev2.ControllerServices;
using InterviewTestTemplatev2.Data;

namespace InterviewTestTemplatev2
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterFilterProvider();
            builder.RegisterSource(new ViewRegistrationSource());

            builder.RegisterType<BonusPoolModelData>().As<IBonusPoolModelData>();
            builder.RegisterType<MapperFactory>().As<IMapperFactory>();
            builder.RegisterType<BonusPoolControllerService>().As<IBonusPoolControllerService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
