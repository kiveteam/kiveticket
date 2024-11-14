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
    }
}
