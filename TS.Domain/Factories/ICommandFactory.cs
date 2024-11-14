using TS.Domain.Commands;

namespace TS.Domain.Factories;

public interface ICommandFactory
{
    ICommand? Create(string type);
}
