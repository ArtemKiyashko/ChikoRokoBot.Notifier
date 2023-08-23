using System.Threading.Tasks;
using AngleSharp;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;
using ReverseMarkdown;

namespace ChikoRokoBot.Notifier.Helpers
{
    public class ToyDataProvider : IDropDataProvider
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly Converter _converter;

        public ToyDataProvider(IBrowsingContext browsingContext, Converter converter)
        {
            _browsingContext = browsingContext;
            _converter = converter;
        }

        public async Task<string> GetDropCaption(Drop drop)
        {
            var descriptionHtml = await _browsingContext.OpenAsync(req => req.Content(drop.Description));

            var markdown = _converter.Convert(descriptionHtml.Body.InnerHtml);

            return TelegramMarkdownSanitizer.Sanitize($"*{drop.Title} - {drop.Mechanic} - {drop.Toy.RarityType.GetDescription()}*\n\n{markdown}");
        }

        public Task<string> GetDropImageUrl(Drop drop) => Task.FromResult($"https://chikoroko.b-cdn.net/toys/main/{drop.Toy.Imageid}.original@2x.webp");
        public Task<string> GetDropUrl(Drop drop) => Task.FromResult($"https://chikoroko.art/en/shop/toy/{drop.Toy.Slug}");
    }
}

