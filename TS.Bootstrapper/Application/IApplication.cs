namespace TS.Bootstrapper;

public interface IApplication
{
    bool IsStarted { get; }
    Task Run();
    void Close();
}