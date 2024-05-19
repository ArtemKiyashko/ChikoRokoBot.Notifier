using AngleSharp;
using ChikoRokoBot.Notifier.Clients;
using ChikoRokoBot.Notifier.Factories;
using ChikoRokoBot.Notifier.Helpers;
using ChikoRokoBot.Notifier.Interfaces;
using ChikoRokoBot.Notifier.Options;
using Ganss.Xss;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(ChikoRokoBot.Notifier.Startup))]
namespace ChikoRokoBot.Notifier
{
    public class Startup : FunctionsStartup
    {
        private IConfigurationRoot _functionConfig;
        private NotifierOptions _notifierOptions = new();

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _functionConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Configure<NotifierOptions>(_functionConfig.GetSection("NotifierOptions"));
            _functionConfig.GetSection("NotifierOptions").Bind(_notifierOptions);

            builder.Services.AddScoped<ITelegramBotClient>(factory => new TelegramBotClient(_notifierOptions.TelegramBotToken));

            builder.Services.AddTransient<IBrowsingContext>((provider) => { return new BrowsingContext(Configuration.Default.WithDefaultLoader()); });

            builder.Services.AddSingleton<IDataProviderFactory, DataProviderFactory>();
            builder.Services.AddScoped<ToyDataProvider>();
            builder.Services.AddScoped<BlindboxDataProvider>();
            builder.Services.AddHttpClient<UserApiClient>(client =>
            {
                client.BaseAddress = _notifierOptions.UserApiBaseAddress;
                client.DefaultRequestHeaders.Add("x-functions-key", _notifierOptions.UserApiKey);
            });
            builder.Services.AddScoped<IHtmlSanitizer, HtmlSanitizer>(factory => new HtmlSanitizer());
            builder.Services.AddScoped<ITelegramHtmlSanitizer, TelegramHtmlSanitizer>();
        }
    }
}

