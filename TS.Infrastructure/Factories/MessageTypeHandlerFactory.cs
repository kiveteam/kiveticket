using Telegram.Bot.Types.Enums;
using TS.Domain.Factories;

namespace TS.Infrastructure.Factories;

internal sealed class MessageTypeHandlerFactory : IMessageTypeHandlerFactory
{
    private Dictionary<MessageType, IMessageTypeHandler> _handlers;

    public MessageTypeHandlerFactory(IEnumerable<IMessageTypeHandler> commands)
    {
        _handlers = commands.ToDictionary(c => c.Type, c => c);
    }

    public IMessageTypeHandler Create(MessageType type)
    {
        if (_handlers.TryGetValue(type, out IMessageTypeHandler? command))
            return command;
        throw new InvalidOperationException($"Обработчик для типа сообщения \"{type}\" не найдена");
    }
}
