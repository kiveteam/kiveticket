using Autofac;
using TS.Domain;
using TS.Domain.Factories;
using TS.Infrastructure.Handlers;
using TS.Infrastructure.Factories;
using TS.Infrastructure.Commands;
using TS.Domain.Commands;

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

        builder.RegisterType<UpdateHandlerFactory>()
            .As<IUpdateHandlerFactory>()
            .InstancePerDependency();
        builder.RegisterType<MessageUpdateHandler>()
            .As<IUpdateHandler>()
            .InstancePerDependency();

        builder.RegisterType<MessageTypeHandlerFactory>()
            .As<IMessageTypeHandlerFactory>()
            .InstancePerDependency();
        builder.RegisterType<MessageTypeTextHandler>()
            .As<IMessageTypeHandler>()
            .InstancePerDependency();

        builder.RegisterType<CommandFactory>()
            .As<ICommandFactory>()
            .InstancePerDependency();
        builder.RegisterType<StartCommand>()
            .As<ICommand>()
            .InstancePerDependency();
    }
}
