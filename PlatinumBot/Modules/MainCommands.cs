using Discord;
using Discord.Commands;
using RunMode = Discord.Commands.RunMode;
using PlatinumBot.Modules;

namespace PlatinumBot.Modules;

public class MainCommands : ModuleBase<ShardedCommandContext>
{
    public CommandService CommandService { get; set; }

    [Command("operator", RunMode = RunMode.Async)]
    public async Task Operator(String arknightsOperator)
    {
        var path = "";
        var jsonLoader = new JSONLoader();
        jsonLoader.Load(path);
        var arknightsOp = jsonLoader.GetOperator(arknightsOperator);
        await Context.Message.Channel.SendMessageAsync($"Here is {arknightsOp.Name}! \nAnd here's their description:\n{arknightsOp.Description}");
    }
    [Command("skills", RunMode = RunMode.Async)]
    public async Task skills(String arknightsOperator)
    {

    }
    [Command("operator", RunMode = RunMode.Async)]
    public async Task skill(String arknightsOperator, int skillNumber, string skillLevel)
    {

    }
    [Command("operator", RunMode = RunMode.Async)]
    public async Task cg(String arknightsOperator, string cgName)
    {

    }
}
