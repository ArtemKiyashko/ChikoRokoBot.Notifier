using System;
namespace ChikoRokoBot.Notifier.Models
{
	public record UserDrop(
        long? ChatId,
        int? TopicId,
        Drop Drop);
}

