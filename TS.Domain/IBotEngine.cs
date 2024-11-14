namespace TS.Domain;

public interface IBotEngine
{
    Task Run(CancellationToken cancellationToken);
}
