namespace ChikoRokoBot.Notifier.Interfaces
{
	public interface ICaptionBuilder
	{
		ICaptionBuilder AddSection(string content);
		string Build();
	}
}

