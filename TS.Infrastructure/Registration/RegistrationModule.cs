using Autofac;
using TS.Domain;

namespace TS.Infrastructure.Registration;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<BotEngine>()
            .As<IBotEngine>()
            .SingleInstance();
        
        builder.RegisterType<BotUpdateHandler>()
            .As<IBotUpdateHandler>()
            .InstancePerDependency();
        
        builder.RegisterType<BotErrorHandler>()
            .As<IBotErrorHandler>()
            .InstancePerDependency();
    }
}
