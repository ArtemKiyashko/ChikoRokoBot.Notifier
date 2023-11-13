using System.Collections.Generic;
using System.Threading.Tasks;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;
using Microsoft.Azure.WebJobs;
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
        public async Task Run([QueueTrigger("notifydrops", Connection = "AzureWebJobsStorage")]UserDrop myQueueItem, ILogger log)
        {
            var dropDataProvider = _dataProviderFactory.GetDropDataProvider(myQueueItem.Drop);

            var firstRow = await GetPrimaryButtons(myQueueItem, dropDataProvider);
            var modelButtons = await GetModelButtons(myQueueItem, dropDataProvider);

            List<InlineKeyboardButton[]> buttons = new() { firstRow, modelButtons.ToArray() };

            InlineKeyboardMarkup inlineKeyboardMarkup = new(buttons);

            await _telegramBotClient.SendPhotoAsync(
                chatId: myQueueItem.ChatId,
                messageThreadId: myQueueItem.TopicId,
                replyMarkup: inlineKeyboardMarkup,
                caption: await dropDataProvider.GetDropCaption(myQueueItem.Drop),
                photo: InputFile.FromString(await dropDataProvider.GetDropImageUrl(myQueueItem.Drop)),
                parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2
            );
        }

        private static async Task<InlineKeyboardButton[]> GetPrimaryButtons(UserDrop myQueueItem, IDropDataProvider dropDataProvider)
        {
            InlineKeyboardButton homePage = InlineKeyboardButton.WithUrl(
                                text: "Home Page",
                                url: "https://artoys.app/");

            InlineKeyboardButton collectPage = InlineKeyboardButton.WithUrl(
                    text: "Collect",
                    url: await dropDataProvider.GetDropUrl(myQueueItem.Drop));

            InlineKeyboardButton[] firstRow = new InlineKeyboardButton[] { homePage, collectPage };
            return firstRow;
        }

        private static async Task<List<InlineKeyboardButton>> GetModelButtons(UserDrop myQueueItem, IDropDataProvider dropDataProvider)
        {
            var modelRow = new List<InlineKeyboardButton>();

            var usdzModelUrl = await dropDataProvider.GetDropModelUsdz(myQueueItem.Drop);

            if (!string.IsNullOrEmpty(usdzModelUrl))
            {
                InlineKeyboardButton usdzModel = InlineKeyboardButton.WithUrl(
                    text: "iOS Model",
                    url: usdzModelUrl);

                modelRow.Add(usdzModel);
            }

            var glbModelUrl = await dropDataProvider.GetDropModelGlb(myQueueItem.Drop);

            if (!string.IsNullOrEmpty(glbModelUrl))
            {
                InlineKeyboardButton glbModel = InlineKeyboardButton.WithUrl(
                    text: "Others Model",
                    url: glbModelUrl);

                modelRow.Add(glbModel);
            }

            return modelRow;
        }
    }
}

