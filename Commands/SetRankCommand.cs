using SharedLibraryCore;
using SharedLibraryCore.Commands;
using SharedLibraryCore.Configuration;
using SharedLibraryCore.Database.Models;
using SharedLibraryCore.Interfaces;
using System.Threading.Tasks;



namespace IW4ScriptCommands.Commands
{
    /// <summary>
    /// Example script command
    /// </summary>
    public class SetRankCommand : Command
    {
        readonly string rank = "rank";
        string rank_string;
        private readonly IMetaService _metaService;

        public SetRankCommand(CommandConfiguration config, ITranslationLookup lookup, IMetaService metaService) : base(config, lookup)
        {
            _metaService = metaService;
            Name = "SetRank";
            Description = "set a user's clan tag Rank (does not give permissions)";
            Alias = "sr";
            Permission = EFClient.Permission.Administrator;
            RequiresTarget = true;
            Arguments = new[]
            {
                new CommandArgument()
                {
                    Name = "rank",
                    Required = true
                }
            };
        }

        public override async Task ExecuteAsync(GameEvent E)
        {
            //var S = E.Owner;
            rank_string = "none";
            if (E.Data != null)
                rank_string = E.Data;
            var rank_player_var = await _metaService.GetPersistentMeta(rank, E.Target);
            if (rank_player_var == null)
            {
                await _metaService.AddPersistentMeta(rank, "none", E.Target);
                rank_player_var = await _metaService.GetPersistentMeta(rank, E.Target);
            }

            if (rank_string.Length > 0 && rank_string.Length < 9)
            {

                await _metaService.AddPersistentMeta(rank, rank_string, E.Target);
                rank_player_var = await _metaService.GetPersistentMeta(rank, E.Target);
                if(rank_player_var.Value == "none" || rank_player_var.Value == "None" || rank_player_var.Value == "NONE")
                    E.Origin.Tell(E.Target.Name + "'s Rank reset to none");

                E.Origin.Tell("New rank set: [" + rank_player_var.Value + "]" + E.Target.Name);
            }
            else
            {
                //rank_player_var = await _metaService.GetPersistentMeta(rank, E.Target);
                E.Origin.Tell($"invalid rank length (between 1-8 characters), set rank to none to reset");
            }
        }
    }
}
