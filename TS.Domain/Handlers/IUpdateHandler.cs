using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TS.Domain.Factories;

public interface IUpdateHandler
{
    UpdateType Type { get; }
    Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}

public interface IMessageTypeHandler
{
    MessageType Type { get; }
    Task Handle(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
}
