using Discord;
using Discord.Commands;
using RunMode = Discord.Commands.RunMode;

namespace PlatinumBot.Modules;

public class ExampleCommands : ModuleBase<ShardedCommandContext>
{
    public CommandService CommandService { get; set; }

    [Command("hello", RunMode = RunMode.Async)]
    public async Task Hello()
    {
        await Context.Message.ReplyAsync($"Hello {Context.User.Username}. Nice to meet you!");
    }

    [Command("slaps", RunMode = RunMode.Async)]
    public async Task Slaps(IUser target_user)
    {
        await Context.Message.Channel.SendMessageAsync($"{Context.User.Username} slapped {target_user.Username} with a trout.");
    }
}
