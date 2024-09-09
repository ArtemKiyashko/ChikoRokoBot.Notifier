using System.Linq;
using System.Threading.Tasks;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;
using Microsoft.Extensions.Logging;

namespace ChikoRokoBot.Notifier.Helpers
{
    public class ToyDataProvider : IDropDataProvider
    {
        private readonly ILogger<ToyDataProvider> _logger;
        private readonly ITelegramHtmlSanitizer _sanitizer;

        public ToyDataProvider(
            ILogger<ToyDataProvider> logger,
            ITelegramHtmlSanitizer sanitizer)
        {
            _logger = logger;
            _sanitizer = sanitizer;
        }

        public Task<string> GetDropCaption(Drop drop)
        {
            var tags = string.Empty;
            var captionBuilder = new CaptionBuilder();

            if (drop.Toy.Tags.Any())
            {
                tags = drop.Toy.Tags.Count > 1 ? drop.Toy.Tags.Aggregate((f, s) => $"#{f} #{s}") : $"#{drop.Toy.Tags[0]}";
            }

            var sanitizedCaption = captionBuilder
                .AddSection($"<b>{drop.Toy.Name} - {drop.Mechanic} - {drop.Toy.RarityType.GetDescription() ?? "Open edition"}</b>")
                .AddSection($"Supplied: {drop.Toy.Supplied ?? 0}")
                .AddSection($"{_sanitizer.Sanitize(drop.Toy.Description)}")
                .AddSection($"{tags}")
                .Build();

            _logger.LogInformation($"Sanitized caption: {sanitizedCaption}");

            return Task.FromResult(sanitizedCaption);
        }

        public Task<string> GetDropImageUrl(Drop drop) => Task.FromResult($"https://chikoroko.b-cdn.net/toys/main/{drop.Toy.Imageid}.original@2x.webp");

        public Task<string> GetDropModelGlb(Drop drop) => Task.FromResult(drop.Toy.ModelUrlGlb);

        public Task<string> GetDropModelUsdz(Drop drop) => Task.FromResult(drop.Toy.ModelUrlUsdz);

        public Task<string> GetDropUrl(Drop drop) => Task.FromResult($"https://r.toys/en/toy/{drop.Toy.Slug}");
    }
}

