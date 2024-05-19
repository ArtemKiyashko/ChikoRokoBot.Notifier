namespace ChikoRokoBot.Notifier.Interfaces
{
	public interface ITelegramHtmlSanitizer
	{
		public string Sanitize(string html);
    }
}

