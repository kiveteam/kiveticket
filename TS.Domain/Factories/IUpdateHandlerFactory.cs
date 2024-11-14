using Telegram.Bot.Types.Enums;

namespace TS.Domain.Factories;

public interface IUpdateHandlerFactory
{
    IUpdateHandler Create(UpdateType type);
}
