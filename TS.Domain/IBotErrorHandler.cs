using Telegram.Bot;

namespace TS.Domain;

public interface IBotErrorHandler
{
    Task Handle(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken);
}