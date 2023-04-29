using System;
using System.Threading.Tasks;
using AngleSharp;
using Azure.Storage.Queues.Models;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;

namespace ChikoRokoBot.Notifier.Helpers
{
    public class ToyDataProvider : IDropDataProvider
    {
        private readonly IBrowsingContext _browsingContext;

        public ToyDataProvider(IBrowsingContext browsingContext)
        {
            _browsingContext = browsingContext;
        }

        public async Task<string> GetDropCaption(Drop drop)
        {
            var descriptionHtml = await _browsingContext.OpenAsync(req => req.Content(drop.Description));
            return $"<b>{drop.Title} - {drop.Mechanic} - {drop.Toy.RarityType.GetDescription()}</b>\n\n{descriptionHtml.Body.TextContent}";
        }

        public Task<string> GetDropImageUrl(Drop drop) => Task.FromResult($"https://chikoroko.b-cdn.net/toys/main/{drop.Toy.Imageid}.original@2x.webp");
        public Task<string> GetDropUrl(Drop drop) => Task.FromResult($"https://chikoroko.art/");
    }
}

