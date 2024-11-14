using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TS.Domain.Factories;

namespace TS.Infrastructure.Handlers;

internal sealed class MessageTypeTextHandler : IMessageTypeHandler
{
    public MessageType Type => MessageType.Text;

    public async Task Handle(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        if (message.Text is not { } messageText)
            return;

        User? user = message.From;
        Chat chat = message.Chat;
        if (message.Text.Equals("/start"))
        {
            await botClient.DeleteMessage(chat.Id, message.Id, cancellationToken);
            ReplyKeyboardMarkup replyKeyboard = new(
                [
                    [
                                    new("Вход для администратора"),
                                ],
                                [
                                    new("Запросить доступ"),
                                ]
                ])
            {
                ResizeKeyboard = true,
            };
            await botClient.SendMessage(
                chat.Id,
                "Добро пожаловать в тикет-систему \"KIVETicket\"\n\n" +
                "Вы не зарегистрированы\n" +
                "- Для получения доступа нажмите на кнопку \"Запросить доступ\" или введите команду /register;\n" +
                "- Если вы являетесь администратором, нажмите на кнопку \"Вход для администратора\" или введите команду /admin",
                cancellationToken: cancellationToken,
                replyMarkup: replyKeyboard);
        }
        else if (message.Text.Equals("Запросить доступ") || message.Text.Equals("/register"))
        {
            await botClient.DeleteMessage(chat.Id, message.Id, cancellationToken);
        }
        else if (message.Text.Equals("Вход для администратора") || message.Text.Equals("/admin"))
        {
            await botClient.DeleteMessage(chat.Id, message.Id, cancellationToken);
        }
        else
        {
            await botClient.DeleteMessage(chat.Id, message.Id, cancellationToken);
            await botClient.SendMessage(
                chat.Id,
                $"Нет указанной команды: \"{message.Text}\"\n",
                cancellationToken: cancellationToken);
        }
    }
}
