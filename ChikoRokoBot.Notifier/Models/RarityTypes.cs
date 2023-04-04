using System;
using System.ComponentModel;

namespace ChikoRokoBot.Notifier.Models
{
	public enum RarityTypes
	{
		[Description("Common")]
		Common = 2,
        [Description("Rare")]
        Rare = 3,
        [Description("Super Rare")]
        SuperRare = 4,
        [Description("Legendary")]
        Legendary = 6
    }
}
