using Discord.Interactions;
using PlatinumBot.Services;

namespace PlatinumBot.Modules.Main
{
    public class MainSlashCommands : InteractionModuleBase<ShardedInteractionContext>
    {
        public DbService DbService { get; set; }
        public InteractionService InteractionService{ get; set; }

        [SlashCommand("operator", "display operator overview")]
        public async Task Operator(String arknightsOperator)
        {
        var arknightsOp = DbService.ArknightsOperators["arknightsOperator"];
        await RespondAsync($"Here is {arknightsOp.Name}! \nAnd here's their description:\n{arknightsOp.Description}");
        }
    }
}