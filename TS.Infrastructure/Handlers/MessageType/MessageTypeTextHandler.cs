using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TS.Domain.Commands;
using TS.Domain.Factories;

namespace TS.Infrastructure.Handlers;

internal sealed class MessageTypeTextHandler : IMessageTypeHandler
{
    private readonly ICommandFactory _commandFactory;

    public MessageType Type => MessageType.Text;

    public MessageTypeTextHandler(ICommandFactory commandFactory)
    {
        _commandFactory = commandFactory;
    }

    public async Task Handle(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        if (message.Text is not { } messageText)
            return;

        Chat chat = message.Chat;

        ICommand? command = _commandFactory.Create(messageText);
        if (command == null)
        {
            await botClient.DeleteMessage(chat.Id, message.Id, cancellationToken);
            await botClient.SendMessage(
                chat.Id,
                $"Нет указанной команды: \"{message.Text}\"\n" +
                $"Попробуйте /start",
                cancellationToken: cancellationToken);
        }
        else await command.Execute(botClient, message, cancellationToken);
    }
}
