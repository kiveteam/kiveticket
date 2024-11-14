using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TS.Domain.Factories;

namespace TS.Infrastructure.Handlers;

internal sealed class MessageUpdateHandler : IUpdateHandler
{
    private readonly IMessageTypeHandlerFactory _messageTypeHandlerFactory;

    public UpdateType Type => UpdateType.Message;

    public MessageUpdateHandler(IMessageTypeHandlerFactory messageTypeHandlerFactory)
    {
        _messageTypeHandlerFactory = messageTypeHandlerFactory;
    }

    public async Task Handle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message)
            return;

        IMessageTypeHandler handler = _messageTypeHandlerFactory.Create(message.Type);
        await handler.Handle(botClient, message, cancellationToken);
    }
}
