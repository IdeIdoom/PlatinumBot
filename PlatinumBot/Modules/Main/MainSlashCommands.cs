using Discord;
using Discord.Interactions;
using PlatinumBot.Data;
using PlatinumBot.Services;

namespace PlatinumBot.Modules.Main
{
    public class MainSlashCommands : InteractionModuleBase<ShardedInteractionContext>
    {
        public DbService DbService { get; set; }
        public InteractionService InteractionService{ get; set; }

        [SlashCommand("operator", "display operator overview")]
        public async Task Operator([Summary("name", "the arknights operator"), Autocomplete(typeof(ArknightsOperatorAutoComplete))] String arknightsOperator)
        {
        var arknightsOp = DbService.ArknightsOperators[$"{arknightsOperator}"];
        await RespondAsync($"Here is {arknightsOp.Name}! \nAnd here's their description:\n{arknightsOp.Description}");
        }
    }

    public class ArknightsOperatorAutoComplete : AutocompleteHandler
    {
        public DbService DbService {get; set; }
        public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context, IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
        {
            List<AutocompleteResult> autocompleteResults = new List<AutocompleteResult>();

            foreach (var arknightsOperator in DbService.ArknightsOperators)
            {
                autocompleteResults.Add(new AutocompleteResult(arknightsOperator.Key, arknightsOperator.Value.Name));
                
            }
            IEnumerable<AutocompleteResult> results = autocompleteResults.AsEnumerable<AutocompleteResult>();

            return AutocompletionResult.FromSuccess(results.Take(25));
        }
    }
}