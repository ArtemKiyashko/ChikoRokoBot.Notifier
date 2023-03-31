using System;
using Azure.Data.Tables;
using Azure.Identity;
using ChikoRokoBot.Notifier.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Azure;
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

            builder.Services.AddAzureClients(clientBuilder => {
                clientBuilder.UseCredential(new DefaultAzureCredential());
                clientBuilder.AddTableServiceClient(_notifierOptions.StorageAccount);
            });

            builder.Services.AddScoped<TableClient>((factory) => {
                var service = factory.GetRequiredService<TableServiceClient>();
                var client = service.GetTableClient(_notifierOptions.UsersTableName);
                client.CreateIfNotExists();
                return client;
            });

            builder.Services.AddScoped<ITelegramBotClient>(factory => new TelegramBotClient(_notifierOptions.TelegramBotToken));
        }
    }
}

