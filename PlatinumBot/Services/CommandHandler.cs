using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using PlatinumBot.Common;
using PlatinumBot.Init;

namespace PlatinumBot.Services;

public class CommandHandler : ICommandHandler
{
    private readonly DiscordShardedClient _client;
    private readonly CommandService _commands;

    public CommandHandler(
        DiscordShardedClient client,
        CommandService commands)
    {
        _client = client;
        _commands = commands;
    }

    public async Task InitializeAsync()
    {
        // add the public modules that inherit InteractionModuleBase<T> to the InteractionService
        await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), Bootstrapper.ServiceProvider);

        // Subscribe a handler to see if a message invokes a command.
        _client.MessageReceived += HandleCommandAsync;

        _commands.CommandExecuted += async (optional, context, result) =>
        {
            if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
            {
                // the command failed, let's notify the user that something happened.
                await context.Channel.SendMessageAsync($"error: {result}");
            }
        };

        foreach (var module in _commands.Modules)
        {
            await Logger.Log(LogSeverity.Info, $"{nameof(CommandHandler)} | Commands", $"Module '{module.Name}' initialized.");
        }
    }

    private async Task HandleCommandAsync(SocketMessage arg)
    {
        // Bail out if it's a System Message.
        if (arg is not SocketUserMessage msg)
            return;

        // We don't want the bot to respond to itself or other bots.
        if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot)
            return;

        // Create a Command Context.
        var context = new ShardedCommandContext(_client, msg);
        var config = new ConfigurationBuilder().AddJsonFile($"appsettings.json").Build();
        var prefix = config.GetRequiredSection("Settings:SetPrefix").Value;
        if(string.IsNullOrWhiteSpace(prefix))
        {
            prefix = config.GetRequiredSection("Settings:DefaultPrefix").Value;
            if(string.IsNullOrWhiteSpace(prefix))
            prefix = "!";
        }

        var markPos = 0;
        if (msg.HasCharPrefix(prefix.ToCharArray()[0], ref markPos))
        {
            var result = await _commands.ExecuteAsync(context, markPos, Bootstrapper.ServiceProvider);
        }
    }
}
