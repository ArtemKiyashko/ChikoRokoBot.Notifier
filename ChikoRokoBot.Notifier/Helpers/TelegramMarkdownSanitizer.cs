using System;
using System.Text;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;

namespace ChikoRokoBot.Notifier.Helpers
{
	public class TelegramMarkdownSanitizer
    {
        public static string Sanitize(string content)
        {
            StringBuilder builder = new StringBuilder(content.LimitTo(1024));

            builder
                .Replace("-", "\\-")
                .Replace(".", "\\.")
                .Replace("!", "\\!")
                .Replace("<u>", string.Empty)
                .Replace("</u>", string.Empty)
                .Replace("`", "\\`")
                .Replace("=", "\\=");

            return builder.ToString();
        }
    }
}

