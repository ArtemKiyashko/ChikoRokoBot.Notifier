using System;
using System.Threading.Tasks;
using AngleSharp;
using Azure.Data.Tables;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ChikoRokoBot.Notifier
{
    public class Notify
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IDataProviderFactory _dataProviderFactory;

        public Notify(ITelegramBotClient telegramBotClient, IDataProviderFactory dataProviderFactory)
        {
            _telegramBotClient = telegramBotClient;
            _dataProviderFactory = dataProviderFactory;
        }

        [FunctionName("Notify")]
        [FixedDelayRetry(5, "00:00:05")]
        public async Task Run([QueueTrigger("notifydrops", Connection = "AzureWebJobsStorage")]UserDrop myQueueItem, ILogger log)
        {
            var dropDataProvider = _dataProviderFactory.GetDropDataProvider(myQueueItem.Drop);

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                InlineKeyboardButton.WithUrl(
                    text: "Collect",
                    url: await dropDataProvider.GetDropUrl(myQueueItem.Drop))
            });

            await _telegramBotClient.SendPhotoAsync(
                chatId: myQueueItem.ChatId,
                messageThreadId: myQueueItem.TopicId,
                replyMarkup: inlineKeyboard,
                caption: await dropDataProvider.GetDropCaption(myQueueItem.Drop),
                photo: InputFile.FromString(await dropDataProvider.GetDropImageUrl(myQueueItem.Drop)),
                parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2
            );
        }
    }
}

