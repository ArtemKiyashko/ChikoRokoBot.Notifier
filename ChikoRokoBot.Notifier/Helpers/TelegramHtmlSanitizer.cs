using System.Linq;
using AngleSharp.Dom;
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
            _sanitizer.AllowedTags.Add("span");
            _sanitizer.AllowedTags.Add("a");
            _sanitizer.AllowedTags.Add("tg-emoji");
            _sanitizer.AllowedTags.Add("code");
            _sanitizer.AllowedTags.Add("pre");
            _sanitizer.AllowedTags.Add("blockquote");

            _sanitizer.AllowedAttributes.Add("href");
            _sanitizer.AllowedAttributes.Add("class");
            _sanitizer.AllowedAttributes.Add("emoji-id");

            _sanitizer.RemovingTag += UnwrapTag;
        }

        public string Sanitize(string html) => _sanitizer.Sanitize(html);

        private void UnwrapTag(object sender, RemovingTagEventArgs args)
        {
            args.Cancel = true;
            UnwrapTag(args.Tag);
        }

        private static void UnwrapTag(IElement tag)
        {
            if (tag.Children.Any())
            {
                tag.InnerHtml = WrapTag(tag);
                tag.Replace(tag.ChildNodes.ToArray());
            }
            else
            {
                tag.OuterHtml = WrapTag(tag);
            }
        }

        private static string WrapTag(IElement tag) => tag.TagName.ToLower() switch
        {
            "p" => $"\n{tag.InnerHtml}\n",
            _ => tag.InnerHtml,
        };
    }    
}

