using Telegram.Bot;

namespace TS;

internal static class Program
{
    static void Main(string[] args)
    {
        Start();
    }

    static async void Start()
    {
        var botClient = new TelegramBotClient("7540641862:AAHALgGrMfUOYtWvqXs0tS_qb17PLJKljec");

        var metBot = new BotEngine(botClient);

        await metBot.ListenForMessagesAsync();
    }
}
