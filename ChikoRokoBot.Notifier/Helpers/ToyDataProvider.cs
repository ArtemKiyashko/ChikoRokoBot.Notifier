﻿using System.Threading.Tasks;
using AngleSharp;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;
using Microsoft.Extensions.Logging;
using ReverseMarkdown;

namespace ChikoRokoBot.Notifier.Helpers
{
    public class ToyDataProvider : IDropDataProvider
    {
        private readonly IBrowsingContext _browsingContext;
        private readonly Converter _converter;
        private readonly ILogger<ToyDataProvider> _logger;

        public ToyDataProvider(IBrowsingContext browsingContext, Converter converter, ILogger<ToyDataProvider> logger)
        {
            _browsingContext = browsingContext;
            _converter = converter;
            _logger = logger;
        }

        public async Task<string> GetDropCaption(Drop drop)
        {
            var descriptionHtml = await _browsingContext.OpenAsync(req => req.Content(drop.Description));

            var markdown = _converter.Convert(descriptionHtml.Body.InnerHtml);

            var sanitizedCaption = TelegramMarkdownSanitizer.Sanitize($"*{drop.Title} - {drop.Mechanic} - {drop.Toy.RarityType.GetDescription()}*\n\n{markdown}");

            _logger.LogInformation($"Sanitized caption: {sanitizedCaption}");

            return sanitizedCaption;
        }

        public Task<string> GetDropImageUrl(Drop drop) => Task.FromResult($"https://chikoroko.b-cdn.net/toys/main/{drop.Toy.Imageid}.original@2x.webp");

        public Task<string> GetDropModelGlb(Drop drop) => Task.FromResult(drop.Toy.ModelUrlGlb);

        public Task<string> GetDropModelUsdz(Drop drop) => Task.FromResult(drop.Toy.ModelUrlUsdz);

        public Task<string> GetDropUrl(Drop drop) => Task.FromResult($"https://artoys.app/en/toy/{drop.Toy.Slug}");
    }
}

