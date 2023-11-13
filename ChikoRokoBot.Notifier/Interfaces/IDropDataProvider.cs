using System;
using System.Threading.Tasks;
using ChikoRokoBot.Notifier.Models;

namespace ChikoRokoBot.Notifier.Interfaces
{
	public interface IDropDataProvider
	{
		public Task<string> GetDropUrl(Drop drop);
		public Task<string> GetDropImageUrl(Drop drop);
        public Task<string> GetDropCaption(Drop drop);
		public Task<string> GetDropModelUsdz(Drop drop);
        public Task<string> GetDropModelGlb(Drop drop);
    }
}

