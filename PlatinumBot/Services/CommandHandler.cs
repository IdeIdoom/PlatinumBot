using System.Reflection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Interactions;
using Microsoft.Extensions.Configuration;
using PlatinumBot.Common;
using PlatinumBot.Init;
using Microsoft.Extensions.DependencyInjection;

namespace PlatinumBot.Services;

public class CommandHandler : ICommandHandler
{
    private readonly DiscordShardedClient _client;
    private readonly CommandService _commands;
    private readonly InteractionService _slashcommands;
    private char prefix = '.';

    public CommandHandler(
        DiscordShardedClient client,
        InteractionService slashcommands,
        CommandService commands)
    {
        _client = client;
        _slashcommands = slashcommands;
        _commands = commands;
    }

    public async Task InitializeAsync()
    {
        // add the public modules that inherit InteractionModuleBase<T> to the InteractionService
        await _commands.AddModulesAsync(Assembly.GetExecutingAssembly(), Bootstrapper.ServiceProvider);
        await _slashcommands.AddModulesAsync(Assembly.GetExecutingAssembly(), Bootstrapper.ServiceProvider);

        // Subscribe a handler to see if a message invokes a command.
        _client.MessageReceived += HandleCommandAsync;

        _client.InteractionCreated += HandleSlashCommand;

        _client.AutocompleteExecuted += async (SocketAutocompleteInteraction arg) => {
            var context = new Discord.Interactions.InteractionContext(_client, arg, arg.Channel);
            await _slashcommands.ExecuteCommandAsync(context,services: Bootstrapper.ServiceProvider);
        };

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

        var markPos = 0;
        if (msg.HasCharPrefix(prefix, ref markPos))
        {
            var result = await _commands.ExecuteAsync(context, markPos, Bootstrapper.ServiceProvider);
        }
    }

    private async Task HandleSlashCommand (SocketInteraction arg) 
    {
        try
            {
                var ctx = new ShardedInteractionContext(_client, arg);
                await _slashcommands.ExecuteCommandAsync(ctx, Bootstrapper.ServiceProvider);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // if a Slash Command execution fails it is most likely that the original interaction acknowledgement will persist. It is a good idea to delete the original
                // response, or at least let the user know that something went wrong during the command execution.
                if(arg.Type == InteractionType.ApplicationCommand)
                {
                    await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
                }
            }
    }

    public void SetPrefix(String prefix) 
    {
        this.prefix = prefix.ToCharArray()[0];
    }
}
