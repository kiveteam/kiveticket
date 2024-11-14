using Autofac;
using TS.Bootstrapper;

namespace TS.Registration;

public class RegistrationModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder.RegisterType<ApplicationBootstrapper>()
            .As<IApplicationBootstrapper>()
            .SingleInstance();
    }
}
