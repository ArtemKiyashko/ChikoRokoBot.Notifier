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

            if (drop.Toy.Tags.Any())
            {
                tags = drop.Toy.Tags.Count > 1 ? drop.Toy.Tags.Aggregate((f, s) => $"#{f} #{s}") : $"#{drop.Toy.Tags[0]}";
            }

            var sanitizedCaption = $"<b>{drop.Toy.Name} - {drop.Mechanic} - {drop.Toy.RarityType.GetDescription() ?? "Open edition"}</b>\n\nSupplied: {drop.Toy.Supplied ?? 0}\n\n{_sanitizer.Sanitize(drop.Toy.Description)}\n\n{tags}";

            _logger.LogInformation($"Sanitized caption: {sanitizedCaption}");

            return Task.FromResult(sanitizedCaption);
        }

        public Task<string> GetDropImageUrl(Drop drop) => Task.FromResult($"https://chikoroko.b-cdn.net/toys/main/{drop.Toy.Imageid}.original@2x.webp");

        public Task<string> GetDropModelGlb(Drop drop) => Task.FromResult(drop.Toy.ModelUrlGlb);

        public Task<string> GetDropModelUsdz(Drop drop) => Task.FromResult(drop.Toy.ModelUrlUsdz);

        public Task<string> GetDropUrl(Drop drop) => Task.FromResult($"https://artoys.app/en/toy/{drop.Toy.Slug}");
    }
}

