namespace TS.Domain.Factories;

public interface IFactory<out TResult>
{
    TResult Create();
}
