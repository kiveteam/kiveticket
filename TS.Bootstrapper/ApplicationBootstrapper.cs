using Autofac;

namespace TS.Bootstrapper;

public class ApplicationBootstrapper : IApplicationBootstrapper
{
    private readonly ILifetimeScope _bootstrapperLifetimeScope;

    public ApplicationBootstrapper(ILifetimeScope lifetimeScope)
    {
        _bootstrapperLifetimeScope = lifetimeScope.BeginLifetimeScope(RegisterDependencies);

        InitializeDependencies();
    }

    public IApplication CreateApplication() =>
        _bootstrapperLifetimeScope.Resolve<IApplication>();

    private void InitializeDependencies()
    {
        
    }

    private void RegisterDependencies(ContainerBuilder containerBuilder)
    {
        containerBuilder.RegisterType<Application>()
            .As<IApplication>()
            .SingleInstance();
        
    }

    public void Dispose()
    {
        _bootstrapperLifetimeScope.Dispose();
        GC.SuppressFinalize(this);
    }
}