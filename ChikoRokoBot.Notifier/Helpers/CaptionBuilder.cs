using System.Text;
using ChikoRokoBot.Notifier.Interfaces;

namespace ChikoRokoBot.Notifier.Helpers
{
	public class CaptionBuilder : ICaptionBuilder
	{
        private readonly StringBuilder _stringBuilder = new();


        public ICaptionBuilder AddSection(string content)
        {
            _stringBuilder
                .AppendLine(content)
                .AppendLine();
            return this;
        }

        public string Build() => _stringBuilder.ToString();
    }
}

