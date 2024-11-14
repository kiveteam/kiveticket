using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TS.Domain;

namespace TS.Infrastructure;

internal sealed class BotUpdateHandler : IBotUpdateHandler
{
    private DateTime _startedDate;
    private bool _initialized;

    public void Initialize(DateTime startedDate)
    {
        _startedDate = startedDate;
        _initialized = true;
    }

    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (!_initialized)
                throw new InvalidOperationException("Bot update handler не инициализирован");
            if (update.Message is not { } message)
                return;

            if (message.Date < _startedDate)
                return;

            if (message.Text is not { } messageText)
                return;

            switch (update.Type)
            {
                case UpdateType.Message:
                    User? user = message.From;

                    Chat chat = message.Chat;

                    switch (message.Type)
                    {
                        case MessageType.Text:
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

                            return;
                    }
                    return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
