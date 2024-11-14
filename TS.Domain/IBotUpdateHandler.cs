using Telegram.Bot;
using Telegram.Bot.Types;

namespace TS.Domain;

public interface IBotUpdateHandler
{
    void Initialize(DateTime startedDate);
    Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken);
}
