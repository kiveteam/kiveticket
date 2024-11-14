using Autofac;
using TS.Domain.Factories;

namespace TS.Bootstrapper.Factories;

internal class Factory<TResult> : IFactory<TResult>
{
    private readonly IComponentContext _componentContext;

    public Factory(IComponentContext componentContext)
    {
        _componentContext = componentContext;
    }

    public TResult Create()
    {
        Func<TResult> factory = _componentContext.Resolve<Func<TResult>>();

        return factory.Invoke();
    }
}
