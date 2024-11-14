using Telegram.Bot.Types.Enums;

namespace TS.Domain.Factories;

public interface IMessageTypeHandlerFactory
{
    IMessageTypeHandler Create(MessageType type);
}