using Telegram.Bot;
using Telegram.Bot.Exceptions;
using TS.Domain;

namespace TS.Infrastructure;

internal sealed class BotErrorHandler : IBotErrorHandler
{
    public Task Handle(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}
