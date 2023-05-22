using ResourceHandler.Resources;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


internal class Program
{ 
    private static void Main(string[] args)
    {
        ProjectInitializer.InitializeConfig(); // Base project Initialization done.

        TelegramBot telegramBot = new TelegramBot(); 
        telegramBot.Start();

        Console.ReadKey();
    }
}