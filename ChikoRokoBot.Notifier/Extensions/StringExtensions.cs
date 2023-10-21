namespace ChikoRokoBot.Notifier.Extensions
{
    public static class StringEnxtensions
    {
        public static string LimitTo(this string str, int maxLength) =>
            str.Length > maxLength ? $"{str.Substring(0, maxLength-3)}..." : str;
    }
}

