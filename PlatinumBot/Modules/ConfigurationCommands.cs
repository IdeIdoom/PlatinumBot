using Discord;
using Discord.Commands;

namespace PlatinumBot.Modules;

public class ConfigurationCommands : ModuleBase<ShardedCommandContext>
{
    public CommandService CommandService { get; set; }

    [Command("setprefix", RunMode = RunMode.Async)]
    public async Task SetPrefix(char prefix)
    {

    }
    [Command("getprefix", RunMode = RunMode.Async)]
    public async Task GetPrefix()
    {

    }
}
