﻿using System;
using System.Threading.Tasks;
using ChikoRokoBot.Notifier.Extensions;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Models;

namespace ChikoRokoBot.Notifier.Helpers
{
	public class BlindboxDataProvider : IDropDataProvider
	{
        public Task<string> GetDropCaption(Drop drop) => Task.FromResult($"<b>{drop.Title} - {drop.Mechanic}</b>");

        public Task<string> GetDropImageUrl(Drop drop) => Task.FromResult("https://chikoroko.b-cdn.net/blindbox/orange.original@2x.webp");

        public Task<string> GetDropUrl(Drop drop) => Task.FromResult($"https://chikoroko.art/blindbox/{drop.BlindBoxId}");
    }
}
