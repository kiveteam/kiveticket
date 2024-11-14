namespace TS.Domain;

public interface IBotEngine
{
    Task RunListen(CancellationToken cancellationToken);
}
