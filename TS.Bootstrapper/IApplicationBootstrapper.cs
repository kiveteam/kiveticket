namespace TS.Bootstrapper;

public interface IApplicationBootstrapper : IDisposable
{
    IApplication CreateApplication();
}
