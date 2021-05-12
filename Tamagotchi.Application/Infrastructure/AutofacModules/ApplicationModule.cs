using Autofac;
using AutofacSerilogIntegration;
using Tamagotchi.Application.Application.Queries;
using Tamagotchi.Domain.AggregatesModel.DragonAggregate;
using Tamagotchi.Infrastructure.Repositories;

namespace Tamagotchi.Application.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        public string QueriesConnectionString { get; }

        public ApplicationModule(string connstr)
        {
            QueriesConnectionString = connstr;

        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DragonQueries(QueriesConnectionString))
                .As<IDragonQueries>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DragonRepository>()
                .As<IDragonRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterLogger();
        }
    }
}
