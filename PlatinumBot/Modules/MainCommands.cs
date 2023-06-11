using Discord;
using Discord.Commands;
using RunMode = Discord.Commands.RunMode;
using PlatinumBot.Modules;

namespace PlatinumBot.Modules;

public class MainCommands : ModuleBase<ShardedCommandContext>
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
    [Command("operator", RunMode = RunMode.Async)]
    public async Task Operator(String arknightsOperator)
    {
        var path = "";
        var jsonLoader = new JSONLoader();
        jsonLoader.Load(path);
        var arknightsOp = jsonLoader.GetOperator(arknightsOperator);
        await Context.Message.Channel.SendMessageAsync($"Here is {arknightsOp.Name}! \nAnd here's their description:\n{arknightsOp.Description}");
    }
}
