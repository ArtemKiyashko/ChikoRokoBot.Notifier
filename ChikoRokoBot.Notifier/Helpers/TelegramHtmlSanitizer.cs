using ChikoRokoBot.Notifier.Interfaces;
using Ganss.Xss;

namespace ChikoRokoBot.Notifier.Helpers
{
	public class TelegramHtmlSanitizer : ITelegramHtmlSanitizer
    {
        private readonly IHtmlSanitizer _sanitizer;

        public TelegramHtmlSanitizer(IHtmlSanitizer sanitizer)
		{
            _sanitizer = sanitizer;

            _sanitizer.AllowedTags.Clear();
            _sanitizer.AllowedAttributes.Clear();
            _sanitizer.AllowedCssProperties.Clear();

            _sanitizer.AllowedTags.Add("b");
            _sanitizer.AllowedTags.Add("strong");
            _sanitizer.AllowedTags.Add("i");
            _sanitizer.AllowedTags.Add("em");
            _sanitizer.AllowedTags.Add("u");
            _sanitizer.AllowedTags.Add("ins");
            _sanitizer.AllowedTags.Add("s");
            _sanitizer.AllowedTags.Add("strike");
            _sanitizer.AllowedTags.Add("del");
            _sanitizer.AllowedTags.Add("a");
            _sanitizer.AllowedTags.Add("tg-emoji");
            _sanitizer.AllowedTags.Add("code");
            _sanitizer.AllowedTags.Add("pre");
            _sanitizer.AllowedTags.Add("blockquote");

            _sanitizer.AllowedAttributes.Add("href");
            _sanitizer.AllowedAttributes.Add("class");
            _sanitizer.AllowedAttributes.Add("emoji-id");

            _sanitizer.KeepChildNodes = true;
        }

        public string Sanitize(string html) => _sanitizer.Sanitize(html);
    }    
}

