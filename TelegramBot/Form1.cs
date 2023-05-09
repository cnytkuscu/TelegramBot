using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Resources;

namespace TelegramBot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static TelegramBotClient Bot = new TelegramBotClient(ProjectInitializer.Config.API_KEY);

        private Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
        {

            var id = update.Message.Chat.Id;


            if (CheckIfCommandOrMessage(update.Message.Text))
            {
                if (update.Message.Type != MessageType.Text)
                {
                    await bot.SendTextMessageAsync(id, "Üzgünüm sizi anlayamadım. Şuan için sadece düz metin içeriklerine cevap verebiliyorum.");
                }
                else
                {
                    string text = update.Message.Text.Replace(ProjectInitializer.Config.CommandPrefix.ToString(), "");
                    string? username = update.Message.Chat.Username;
                    if (text == "sa") await bot.SendTextMessageAsync(id, "Komut başarıyla çalıştırıldı.");
                }
            }
            else
            {
                await bot.SendTextMessageAsync(id, "Üzgünüm sizi anlayamadım. Komut işleminden önce [ " + ProjectInitializer.Config.CommandPrefix + " ] prefix'i kullandığınızdan emin olun !");
                await bot.SendTextMessageAsync(id, "Sizi dinlemememi istiyorsanız lütfen " + ProjectInitializer.Config.CommandPrefix + "dur yazınız.");
                await bot.SendTextMessageAsync(id, "Tekrar hizmete girmem için" + ProjectInitializer.Config.CommandPrefix + "basla yazınız.");
            }




        }

        private static bool CheckIfCommandOrMessage(string? text)
        {
            return text.Substring(0, 1).Equals(ProjectInitializer.Config.CommandPrefix.ToString());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ProjectInitializer.InitializeConfig(); // Base project Initialization done.

            Thread.Sleep(1000); // 1 second system delay.



            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
               {
                    UpdateType.Message,
                    UpdateType.EditedMessage
               }
            };

            Bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);
        }
    }
}