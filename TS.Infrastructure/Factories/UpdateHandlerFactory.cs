using Telegram.Bot.Types.Enums;
using TS.Domain.Factories;

namespace TS.Infrastructure.Factories;

internal sealed class UpdateHandlerFactory : IUpdateHandlerFactory
{
    private Dictionary<UpdateType, IUpdateHandler> _handlers;

    public UpdateHandlerFactory(IEnumerable<IUpdateHandler> commands)
    {
        _handlers = commands.ToDictionary(c => c.Type, c => c);
    }

    public IUpdateHandler Create(UpdateType type)
    {
        if (_handlers.TryGetValue(type, out IUpdateHandler? command))
            return command;
        throw new InvalidOperationException($"Обработчик для типа обновления \"{type}\" не найдена");
    }
}
