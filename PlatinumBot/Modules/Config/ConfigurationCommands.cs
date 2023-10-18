using Discord;
using Discord.Commands;
using PlatinumBot.Services;

namespace PlatinumBot.Modules.Config;

public class ConfigurationCommands : ModuleBase<ShardedCommandContext>
{
    public CommandService CommandService { get; set; }
    public ICommandHandler commandHandler { get; set; }

    [Command("setprefix", RunMode = RunMode.Async)]
    public async Task SetPrefix(string prefix)
    {
        await Task.Run(() => commandHandler.SetPrefix(prefix));
    }
    [Command("getprefix", RunMode = RunMode.Async)]
    public async Task GetPrefix()
    {

    }
}
