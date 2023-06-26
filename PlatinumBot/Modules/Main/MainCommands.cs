using Discord;
using Discord.Commands;
using RunMode = Discord.Commands.RunMode;
using PlatinumBot.Modules;
using PlatinumBot.Services;

namespace PlatinumBot.Modules.Main;

public class MainCommands : ModuleBase<ShardedCommandContext>
{
    public CommandService CommandService { get; set; }
    public DbService DbService { get; set; }

    [Command("operator", RunMode = RunMode.Async)]
    public async Task Operator(String arknightsOperator)
    {
        var arknightsOp = DbService.ArknightsOperators["arknightsOperator"];
        await Context.Message.Channel.SendMessageAsync($"Here is {arknightsOp.Name}! \nAnd here's their description:\n{arknightsOp.Description}");
    }
    [Command("skills", RunMode = RunMode.Async)]
    public async Task skills(String arknightsOperator)
    {

    }
    [Command("skill", RunMode = RunMode.Async)]
    public async Task skill(String arknightsOperator, int skillNumber, string skillLevel)
    {

    }
    [Command("cg", RunMode = RunMode.Async)]
    public async Task cg(String arknightsOperator, string cgName)
    {

    }
}
