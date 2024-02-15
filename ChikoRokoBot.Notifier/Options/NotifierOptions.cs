using System;
namespace ChikoRokoBot.Notifier.Options
{
	public class NotifierOptions
	{
		public string TelegramBotToken { get; set; }
		public Uri UserApiBaseAddress { get; set; } = new Uri("http://localhost:7071/");
		public string UserApiKey { get; set; }
	}
}

