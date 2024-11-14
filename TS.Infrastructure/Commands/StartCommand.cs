using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TS.Domain.Commands;

namespace TS.Infrastructure.Commands;

internal sealed class StartCommand : ICommand
{
    public string Type => "/start";

    public string Alias => "Запуск";

    public async Task Execute(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        Chat chat = message.Chat;
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
}
