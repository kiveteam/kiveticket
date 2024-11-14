using Telegram.Bot;
using Telegram.Bot.Polling;
using TS.Domain;
using TS.Domain.Factories;

namespace TS.Infrastructure;

internal sealed class BotEngine : IBotEngine
{
    private readonly IFactory<ITelegramBotClient> _botClientFactory;
    private readonly IBotUpdateHandler _botUpdateHandler;
    private readonly IBotErrorHandler _botErrorHandler;
    private ITelegramBotClient _botClient;
    private CancellationToken _cancellationToken;

    public BotEngine(IFactory<ITelegramBotClient> botClientFactory,
        IBotUpdateHandler botUpdateHandler,
        IBotErrorHandler botErrorHandler)
    {
        _botClientFactory = botClientFactory;
        _botUpdateHandler = botUpdateHandler;
        _botErrorHandler = botErrorHandler;
    }

    public Task Run(CancellationToken cancellationToken)
    {
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = []
        };

        _cancellationToken = cancellationToken;
        _botClient = _botClientFactory.Create();
        _botUpdateHandler.Initialize(DateTime.UtcNow);
        return _botClient.ReceiveAsync(
            updateHandler: _botUpdateHandler.Handle,
            errorHandler: _botErrorHandler.Handle,
            receiverOptions: receiverOptions,
            cancellationToken: _cancellationToken
        );
    }
}
