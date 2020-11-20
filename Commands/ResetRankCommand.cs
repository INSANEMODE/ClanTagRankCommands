using SharedLibraryCore;
using SharedLibraryCore.Commands;
using SharedLibraryCore.Configuration;
using SharedLibraryCore.Database.Models;
using SharedLibraryCore.Interfaces;
using System;
using System.Threading.Tasks;



namespace ClanTagRankApi.Commands
{
    /// <summary>
    /// Example script command
    /// </summary>
    public class ResetRankCommand : Command
    {
        readonly string rank = "rank";
        string rank_string;
        private readonly IMetaService _metaService;
        private readonly IConfigurationHandler<Configuration> _configurationHandler;
        private Configuration Config;

        public ResetRankCommand(CommandConfiguration config, ITranslationLookup lookup, IMetaService metaService, IConfigurationHandlerFactory configurationHandlerFactory) : base(config, lookup)
        {
            _metaService = metaService;
            _configurationHandler = configurationHandlerFactory.GetConfigurationHandler<Configuration>("ClanTagRankCommands");
            if (_configurationHandler.Configuration() == null)
            {
                _configurationHandler.Set((Configuration)new Configuration().Generate());
                _configurationHandler.Save();
            }
            Config = _configurationHandler.Configuration();

            Name = "ResetRank";
            Description = "set a user's clan tag Rank (does not give permissions)";
            Alias = "rr";
            Permission = EFClient.Permission.Administrator;
            RequiresTarget = true;
            Arguments = new[]
            {
                new CommandArgument()
                {
                    //Name = "rank",
                    //Required = false
                }
            };
        }

        public override async Task ExecuteAsync(GameEvent E)
        {
            //var S = E.Owner;
            rank_string = "none";
            await _metaService.AddPersistentMeta(rank, rank_string, E.Target);
            rank_string = E.Target.Level.ClanTag(Config);
            E.Origin.Tell(E.Target.Name + "'s rank has been reset to: " + rank_string);
            await E.Owner.ExecuteCommandAsync("setrank" + " " + E.Target.ClientNumber + " " + rank_string);
        }
    }
}
