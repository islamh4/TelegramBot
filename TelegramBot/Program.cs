using System;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("5353439699:AAHoH2Q3TiWLUUoZBGwFOCgOVIoYeNxWfSc");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var keyboard = new ReplyKeyboardMarkup(
                new[]
                {
                    new []
                    {
                        new KeyboardButton ("Start"),
                        new KeyboardButton ("Images"),
                        new KeyboardButton ("Stop")
                    }
                });
            keyboard.ResizeKeyboard = true;
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Text.ToLower() == "start")
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat, "Hello", ParseMode.Html, replyMarkup: keyboard);
                    return;
                }
                if (update.Message.Text.ToLower() == "images")
                {
                    await botClient.SendPhotoAsync(update.Message.Chat, new InputOnlineFile("https://yandex.ru/images/search?from=tabbar&text=Anime"), "Аниме тянки");
                    return;
                }
                if (update.Message.Text.ToLower() == "stop")
                {
                    await botClient.SendTextMessageAsync(update.Message.Chat, "Good bay");
                    return;
                }
                await botClient.SendTextMessageAsync(update.Message.Chat, "Write text \"start\" or \"imags\"");
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    }
}
