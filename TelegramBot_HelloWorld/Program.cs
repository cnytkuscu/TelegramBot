using Telegram.Bot;
using Telegram.Bot.Types;

internal class Program
{
    private static void Main(string[] args)
    {
        TelegramBotClient? botClient = new TelegramBotClient("6059318905:AAEd43DBh6BFRwH1EbRch6zxDeF3S1OzACk"); 

        botClient.StartReceiving(UpdateHandler, ErrorHandler);
        Console.WriteLine("Bot started successfully and listening the specific channel.");
        Console.ReadKey();
    }

    private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken token)
    {
        string responseMessage = (update.Message.Text == "/hello") ? "Hello World!" : "Couldn't understand, sorry :(";
        await bot.SendTextMessageAsync(update.Message.Chat.Id, responseMessage);

        

    }
    private static async Task ErrorHandler(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        Console.WriteLine("Error happened.");
    }
}