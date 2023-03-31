using System;
namespace ChikoRokoBot.Notifier.Options
{
	public class NotifierOptions
	{
		public string TelegramBotToken { get; set; }
		public string StorageAccount { get; set; } = "UseDevelopmentStorage=true";
		public string UsersTableName { get; set; } = "users";
	}
}

