using Discord;
using Discord.Commands;
using RunMode = Discord.Commands.RunMode;
using PlatinumBot.Modules;
using PlatinumBot.Data;
using Newtonsoft.Json;

namespace PlatinumBot.Modules.Misc;


public class MiscCommands : ModuleBase<ShardedCommandContext>
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

    [Command("8ball", RunMode = RunMode.Async)]
    public async Task EightBall()
    {
        JSONLoader jsonLoader = new JSONLoader();
        jsonLoader.Load("./data/eightball.json");
        EightBallResponses? responses = JsonConvert.DeserializeObject<EightBallResponses>(jsonLoader.JsonData);
        Random random = new Random();

        await Context.Message.ReplyAsync($"{Context.User.Username}'s result: {responses?.Responses.ElementAt(random.Next(0, responses.Responses.Count)).Response}");
    }
}