using Telegram.Bot;
using Telegram.Bot.Types;

namespace TS.Domain.Commands;

public interface ICommand
{
    string Type { get; }
    string Alias { get; }
    Task Execute(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken);
}
