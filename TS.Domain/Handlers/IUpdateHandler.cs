using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TS.Domain.Factories;

public interface IUpdateHandler
{
    UpdateType Type { get; }
    Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}
