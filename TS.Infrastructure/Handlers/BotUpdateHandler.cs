using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using TS.Domain;
using TS.Domain.Factories;

namespace TS.Infrastructure;

internal sealed class BotUpdateHandler : IBotUpdateHandler
{
    private DateTime _startedDate;
    private bool _initialized;
    private readonly IUpdateHandlerFactory _updateCommandFactory;

    public BotUpdateHandler(IUpdateHandlerFactory updateCommandFactory)
    {
        _updateCommandFactory = updateCommandFactory;
    }

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

            if (update.Message is { } message && message.Date < _startedDate)
                return;

            IUpdateHandler command = _updateCommandFactory.Create(update.Type);
            await command.Handle(botClient, update, cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
