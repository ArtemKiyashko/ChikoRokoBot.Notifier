using System.Threading.Tasks;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;

namespace ChikoRokoBot.Notifier.Helpers
{
    public class ToyDataProvider : IDropDataProvider
    {
        public Task<string> GetDropCaption(Drop drop) => Task.FromResult($"{TelegramMarkdownSanitizer.Sanitize($"*{drop.Title} - {drop.Mechanic} - {drop.Toy.RarityType.GetDescription()}*\n\n")}{drop.MarkdownDescription}");

        public Task<string> GetDropImageUrl(Drop drop) => Task.FromResult($"https://chikoroko.b-cdn.net/toys/main/{drop.Toy.Imageid}.original@2x.webp");

        public Task<string> GetDropModelGlb(Drop drop) => Task.FromResult(drop.Toy.ModelUrlGlb);

        public Task<string> GetDropModelUsdz(Drop drop) => Task.FromResult(drop.Toy.ModelUrlUsdz);

        public Task<string> GetDropUrl(Drop drop) => Task.FromResult($"https://artoys.app/en/toy/{drop.Toy.Slug}");
    }
}

