using System.ComponentModel;

namespace ChikoRokoBot.Notifier.Extensions
{
	public static class EnumExtensions
	{
        public static string GetDescription<T>(this T val)
        {
            if (val is null) return default;

            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())?
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes?.Length > 0 ? attributes[0].Description : "Unknown";
        }
    }
}


