using System;
using ChikoRokoBot.Notifier.Helpers;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ChikoRokoBot.Notifier.Factories
{
	public class DataProviderFactory : IDataProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DataProviderFactory(IServiceProvider serviceProvider)
		{
            _serviceProvider = serviceProvider;
        }

        public IDropDataProvider GetDropDataProvider(Drop drop) => drop.Mechanic.ToLower() switch {
            "blindbox" => _serviceProvider.GetService<BlindboxDataProvider>(),
            _ => _serviceProvider.GetService<ToyDataProvider>()
        };

	}
}

