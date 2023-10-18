using Discord;
using Discord.Interactions;
using PlatinumBot.Data;
using PlatinumBot.Services;
using System.Xml.Linq;

namespace PlatinumBot.Modules.Main
{
    public class MainSlashCommands : InteractionModuleBase<ShardedInteractionContext>
    {
        public DbService DbService { get; set; }
        public InteractionService InteractionService{ get; set; }

        // TODO: Embed implementation

        [SlashCommand("operator", "display operator overview")]
        public async Task Operator([Summary("name", "the arknights operator"), Autocomplete(typeof(ArknightsOperatorAutoComplete))] string arknightsOperator)
        {
            var arknightsOp = DbService.ArknightsOperators[$"{arknightsOperator}"];
            await RespondAsync($"Here is {arknightsOp.Name}! \nAnd here's their description:\n{arknightsOp.Description}");
        }

        [SlashCommand("cg", "display operator cg")]
        public async Task CG([Summary("name", "the arknights operator"), Autocomplete(typeof(ArknightsOperatorAutoComplete))] string arknightsOperator,
            [Summary("cg", "the name of cg"), Autocomplete(typeof(ArknightsOperatorCGAutoComplete))] string cgName)
        {
            var arknightsOp = DbService.ArknightsOperators[$"{arknightsOperator}"];
            var cg = DbService.OperatorCGPaths[$"{arknightsOperator}"].Where(p => p.Filename == cgName).First();
            var filepath = "./data/CG/";
            //await Context.Message.Channel.SendFileAsync($"{filepath}" + $"{DbService.OperatorCGPaths[$"{arknightsOperator}" + $" {cgName}"]}", DbService.ArknightsOperators[$"{arknightsOperator}"].Name);
            FileAttachment attachment = new FileAttachment(filepath+cg.Filename, cg.Filename);
            
            await RespondWithFileAsync(attachment, $"Here is the {arknightsOp.Name} with the {cg.Name} skin.");
        }

        public async Task skill()
        {
            // TODO: implementation of the /skill command
            // Provides info of one skill
        }

        public async Task skills()
        {
            // TODO: implementation of the /skills command
            // Provides info of the skills an operator has
        }
    }

    public class ArknightsOperatorCGAutoComplete : AutocompleteHandler
    {
        public DbService DbService { get; set;}
        public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context, IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
        {
            List<AutocompleteResult> autocompleteResults = new();

            var arknightsOp = autocompleteInteraction.Data.Options.First().Value.ToString();

            foreach (var cg in DbService.OperatorCGPaths[arknightsOp])
            {
                if (cg.Name.ToLower().StartsWith(autocompleteInteraction.Data.Current.Value.ToString()))
                    autocompleteResults.Add(new AutocompleteResult(cg.Name, cg.Filename));
            }

            IEnumerable<AutocompleteResult> results = autocompleteResults.AsEnumerable<AutocompleteResult>();

            return AutocompletionResult.FromSuccess(results.Take(25));
        }
    }

    public class ArknightsOperatorAutoComplete : AutocompleteHandler
    {
        public DbService DbService {get; set; }
        public override async Task<AutocompletionResult> GenerateSuggestionsAsync(IInteractionContext context, IAutocompleteInteraction autocompleteInteraction, IParameterInfo parameter, IServiceProvider services)
        {
            List<AutocompleteResult> autocompleteResults = new();

            foreach (var arknightsOperator in DbService.ArknightsOperators)
            {
                if (arknightsOperator.Value.Name.ToLower().StartsWith(autocompleteInteraction.Data.Current.Value.ToString()))
                    autocompleteResults.Add(new AutocompleteResult(arknightsOperator.Key, arknightsOperator.Value.Name));
            }

            IEnumerable<AutocompleteResult> results = autocompleteResults.AsEnumerable<AutocompleteResult>();

            return AutocompletionResult.FromSuccess(results.Take(25));
        }
    }
}