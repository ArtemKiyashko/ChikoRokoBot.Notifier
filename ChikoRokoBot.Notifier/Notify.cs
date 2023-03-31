using System;
using System.Threading.Tasks;
using Azure.Data.Tables;
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
        private readonly TableClient _tableClient;
        private readonly string PARTITION_NAME = "primary";

        public Notify(ITelegramBotClient telegramBotClient, TableClient tableClient)
        {
            _telegramBotClient = telegramBotClient;
            _tableClient = tableClient;
        }

        [FunctionName("Notify")]
        public async Task Run([QueueTrigger("notifydrops", Connection = "AzureWebJobsStorage")]Drop myQueueItem, ILogger log)
        {
            var img = $"https://chikoroko.b-cdn.net/toys/main/{myQueueItem.Toy.Imageid}.original@2x.webp";
            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                InlineKeyboardButton.WithUrl(
                    text: "Забрать",
                    url: $"https://chikoroko.art/shop/toy/{myQueueItem.Toy.Slug}")
            });

            var usersToNotify = _tableClient.QueryAsync<UserTableEntity>($"PartitionKey eq '{PARTITION_NAME}'");

            await foreach (var user in usersToNotify)
            {
                await _telegramBotClient.SendPhotoAsync(
                    chatId: user.ChatId,
                    replyMarkup: inlineKeyboard,
                    caption: $"<b>{myQueueItem.Title} - {myQueueItem.Mechanic}</b>",
                    photo: img,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Html
                );
            }
        }
    }
}

