using System;
using System.Threading.Tasks;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;

namespace ChikoRokoBot.Notifier.Helpers
{
	public class BlindboxDataProvider : IDropDataProvider
	{
        public Task<string> GetDropCaption(Drop drop) => Task.FromResult(TelegramMarkdownSanitizer.Sanitize($"*{drop.Title} - {drop.Mechanic}*"));

        public Task<string> GetDropImageUrl(Drop drop) => Task.FromResult("https://chikoroko.b-cdn.net/blindbox/orange.original@2x.webp");

        public Task<string> GetDropModelGlb(Drop drop) => Task.FromResult((string)null);

        public Task<string> GetDropModelUsdz(Drop drop) => Task.FromResult((string)null);

        public Task<string> GetDropUrl(Drop drop) => Task.FromResult($"https://artoys.app");
    }
}

