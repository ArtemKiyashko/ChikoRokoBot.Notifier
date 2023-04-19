using System;
using ChikoRokoBot.Notifier.Models;

namespace ChikoRokoBot.Notifier.Interfaces
{
	public interface IDataProviderFactory
	{
        public IDropDataProvider GetDropDataProvider(Drop drop);

    }
}

