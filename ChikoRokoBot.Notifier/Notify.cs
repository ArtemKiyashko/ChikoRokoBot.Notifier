using System;
using System.Threading.Tasks;
using AngleSharp;
using Azure.Data.Tables;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChikoRokoBot.Notifier
{
    public class Notify
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IBrowsingContext _browsingContext;

        public Notify(ITelegramBotClient telegramBotClient, IBrowsingContext browsingContext)
        {
            _telegramBotClient = telegramBotClient;
            _browsingContext = browsingContext;
        }

        [FunctionName("Notify")]
        [FixedDelayRetry(5, "00:00:05")]
        public async Task Run([QueueTrigger("notifydrops", Connection = "AzureWebJobsStorage")]UserDrop myQueueItem, ILogger log)
        {
            var img = $"https://chikoroko.b-cdn.net/toys/main/{myQueueItem.Drop.Toy.Imageid}.original@2x.webp";
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                InlineKeyboardButton.WithUrl(
                    text: "Забрать",
                    url: $"https://chikoroko.art/shop/toy/{myQueueItem.Drop.Toy.Slug}")
            });

            var descriptionHtml = await _browsingContext.OpenAsync(req => req.Content(myQueueItem.Drop.Description));

            await _telegramBotClient.SendPhotoAsync(
                chatId: myQueueItem.ChatId,
                replyMarkup: inlineKeyboard,
                caption: $"<b>{myQueueItem.Drop.Title} - {myQueueItem.Drop.Mechanic} - {myQueueItem.Drop.Toy.RarityType.GetDescription()}</b>\n\n{descriptionHtml.Body.TextContent}",
                photo: img,
                parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
            );
        }
    }
}

