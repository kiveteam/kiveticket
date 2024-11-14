using TS.Domain.Commands;
using TS.Domain.Factories;

namespace TS.Infrastructure.Factories;

internal sealed class CommandFactory : ICommandFactory
{
    private Dictionary<string, ICommand> _commands;

    public CommandFactory(IEnumerable<ICommand> commands)
    {
        _commands = commands.ToDictionary(c => c.Type, c => c);
    }

    public ICommand? Create(string type)
    {
        _commands.TryGetValue(type, out ICommand? command);
        return command;
    }
}
