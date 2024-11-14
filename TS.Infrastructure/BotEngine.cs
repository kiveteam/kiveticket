using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TS.Domain;
using TS.Domain.Factories;

namespace TS.Infrastructure;

internal class BotEngine : IBotEngine
{
    private readonly IFactory<ITelegramBotClient> _botClientFactory;
    private ITelegramBotClient _botClient;
    private DateTime _startedDate;
    private CancellationToken _cancellationToken;

    public BotEngine(IFactory<ITelegramBotClient> botClientFactory)
    {
        _botClientFactory = botClientFactory;
    }

    public Task RunListen(CancellationToken cancellationToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = []
        };

        _cancellationToken = cancellationToken;
        _botClient = _botClientFactory.Create();
        _startedDate = DateTime.UtcNow;
        return _botClient.ReceiveAsync(
            updateHandler: HandleUpdateAsync,
            errorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: _cancellationToken
        );
    }

    private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
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
                                            new("Запросить доступ"),
                                        ]
                                    ])
                                {
                                    ResizeKeyboard = true,
                                };
                                await botClient.SendMessage(
                                    chat.Id,
                                    $"{user.Username}, Добро пожаловать в тикет-систему \"KIVETicket\"\n\n" +
                                    "Вы не зарегистрированы, для того чтобы получить доступ " +
                                    "нажмите на кнопку \"Запросить доступ\" или введите команду /register",
                                    cancellationToken: cancellationToken,
                                    replyMarkup: replyKeyboard);
                            }
                            else if (message.Text.Equals("Запросить доступ") || message.Text.Equals("/register"))
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

    private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}
