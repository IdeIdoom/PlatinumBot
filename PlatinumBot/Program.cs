using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using PlatinumBot.Common;
using PlatinumBot.Init;
using PlatinumBot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlatinumBot.Data;

var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json")
    .AddEnvironmentVariables()
    .Build();
var client = new DiscordShardedClient( new DiscordSocketConfig
                                {   MessageCacheSize = 100,
                                    GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent });


var commands = new CommandService(new CommandServiceConfig
{
    // Again, log level:
    LogLevel = LogSeverity.Info,

    // There's a few more properties you can set,
    // for example, case-insensitive commands.
    CaseSensitiveCommands = false,
});

var interactions = new InteractionService(client, new InteractionServiceConfig
{
    EnableAutocompleteHandlers = true,
    LogLevel = LogSeverity.Info,
    AutoServiceScopes = true
});

var db = new DbService();
db.Init();

// Setup your DI container.
Bootstrapper.Init();
Bootstrapper.RegisterInstance(client);
Bootstrapper.RegisterInstance(interactions);
Bootstrapper.RegisterInstance(commands);
Bootstrapper.RegisterType<ICommandHandler, CommandHandler>();
Bootstrapper.RegisterInstance(config);
Bootstrapper.RegisterInstance(db);

await MainAsync();

async Task MainAsync()
{
    await Bootstrapper.ServiceProvider.GetRequiredService<ICommandHandler>().InitializeAsync();

    client.ShardReady += async shard =>
    {
        await interactions.RegisterCommandsGloballyAsync();
        await Logger.Log(LogSeverity.Info, "ShardReady", $"Shard Number {shard.ShardId} is connected and ready!");
    };

    // Login and connect.
    var token = config.GetRequiredSection("Settings:DiscordToken").Value;
    if (string.IsNullOrWhiteSpace(token))
    {
        token = config.GetRequiredSection("DISCORDTOKEN").Value; //EnvironmentVariable
        if(string.IsNullOrWhiteSpace(token))
        {
            await Logger.Log(LogSeverity.Error, $"{nameof(Program)} | {nameof(MainAsync)}", "Token is null or empty.");
            return;
        }

    }

    await client.LoginAsync(TokenType.Bot, token);
    await client.StartAsync();

    // Wait infinitely so your bot actually stays connected.
    await Task.Delay(Timeout.Infinite);
}
