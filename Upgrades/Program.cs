using System.Diagnostics.Metrics;
using System.Net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Upgrades;
using Upgrades.MetApi;
using Upgrades.Models;
using System.Speech.Synthesis;
using System.Threading;

internal class Program
{
    private static IMetApi? _metApi;
    private static void Main(string[] args)
    {
        TelegramBotClient? botClient = new TelegramBotClient("6059318905:AAEd43DBh6BFRwH1EbRch6zxDeF3S1OzACk");
        _metApi = new MetApi();

        botClient.StartReceiving(UpdateHandler, ErrorHandler);
        Console.WriteLine("Bot started successfully and listening the specific channel.");
        Console.ReadKey();
    }

    public static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken cts)
    {
        bool isCommand = update.Message.Entities != null ? !String.IsNullOrEmpty(update.Message.Entities[0].Type.ToString()) : false;
        if (isCommand)
        {
            // Send Random Art as Photo from Metropolitan Museum.
            if (update.Message.Text == "/randomArt")
            {
                var randomArt = await GetRandomArtAsync();
                await SendPhotoMessageAsync(bot, update.Message, randomArt, cts);
            }
            if (update.Message.Text == "/voice")
            {
                await SendVoiceMessageAsync(bot, update.Message, cts);
            }
            if (update.Message.Text == "/location")
            {
                await SendLocationMessageAsync(bot, update.Message, cts);
            }
        }
    }


    private static async Task ErrorHandler(ITelegramBotClient bot, Exception exception, CancellationToken token)
    {
        Console.WriteLine("Error happened." + exception.Message);
    }


    #region Send Message as Voice
    private static async Task SendVoiceMessageAsync(ITelegramBotClient bot, Message message, CancellationToken cts)
    {
        string[] songNames = new string[] { "Dreams", "All That", "Creative Minds", "Hey", "Evolution", "Inspire", "Ukulele", "Jazzy Frenchy", "Elevate", "Once Again", "Photo Album", "Memories", " Fun Day", "Clap And Yell", "Happy Rock" };

        Message sendVoice = await bot.SendVoiceAsync(
            chatId: message.Chat.Id,
            voice: "https://www.bensound.com/bensound-music/bensound-" + songNames[new Random().Next(0, songNames.Count())].ToLower().Replace(" ", "") + ".mp3",
            caption: "Random Sound from Web.",
            parseMode: ParseMode.Html,
            cancellationToken: cts);
    }

    #endregion

    #region Send Message as Photo
    private static async Task SendPhotoMessageAsync(ITelegramBotClient botClient, Message message, CollectionItem collectionItem, CancellationToken cancellationToken)
    {
        Message sendArtwork = await botClient.SendPhotoAsync(
                chatId: message.Chat.Id,
                photo: collectionItem.primaryImage,
                caption: "<b>" + collectionItem.artistDisplayName + "</b>" + " <i>Artwork</i>: " + collectionItem.title,
                parseMode: ParseMode.Html,
                cancellationToken: cancellationToken);
    }
    private static async Task<CollectionItem> GetRandomArtAsync()
    {
        var objectList = await _metApi.GetCollectionObjectsAsync();
        var validImage = false;

        while (!validImage)
        {
            var collectionObject = HelperMethods.RandomNumberFromList(objectList.objectIDs);
            var collectionItem = await _metApi.GetCollectionItemAsync(collectionObject.ToString());

            if (!string.IsNullOrEmpty(collectionItem.primaryImage))
            {
                validImage = true;
                return collectionItem;
            }
        }
        throw new Exception("Error: Couldn't get random image");
    }
    #endregion

    #region Send Message As Location
    private static async Task SendLocationMessageAsync(ITelegramBotClient bot, Message message, CancellationToken cts)
    {
        double randomLatitude = new Random().NextDouble() * 180 - 90;
        double randomLongitude = new Random().NextDouble() * 360 - 180;

        Location location = new Location
        {
            Latitude = randomLatitude,
            Longitude = randomLongitude
        };

        await bot.SendLocationAsync(
            chatId: message.Chat.Id,
            latitude: location.Latitude,
            longitude: location.Longitude,
            cancellationToken: cts);
    }
    #endregion 

}