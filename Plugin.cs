using SharedLibraryCore;
using SharedLibraryCore.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using SharedLibraryCore.Database.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;


namespace ClanTagRankApi
{

    public class Plugin : IPlugin
    {
        private readonly IConfigurationHandler<Configuration> _configurationHandler;
        private readonly ILogger _logger;
        private Configuration Config;
        readonly string rank = "rank";
        string rankName = "none";



        public string Name => "ClanTagRankCommands";

        public float Version => 1.31f;

        public string Author => "INSANEMODE";

        private readonly IMetaService _metaService;

        public Plugin(IMetaService metaService, IConfigurationHandlerFactory configurationHandlerFactory, ILogger<Plugin> logger) 
        {
            _logger = logger;
            _metaService = metaService;
            _configurationHandler = (IConfigurationHandler<Configuration>)configurationHandlerFactory.GetConfigurationHandler<Configuration>("ClanTagRankCommands");
        }
        public Task OnLoadAsync(IManager manager)// => Task.CompletedTask;
        {
            if (_configurationHandler.Configuration() == null)
            {
                _configurationHandler.Set((Configuration)new Configuration().Generate());
                _configurationHandler.Save();
            }
            Config = _configurationHandler.Configuration();
            string version = manager.Version;
            string str = string.Format("Loaded {0} ({1}) by {2} in {3} ({4})!", (object)((IPlugin)this).Name, (object)((IPlugin)this).Version, (object)((IPlugin)this).Author, (object)"IW4MAdmin", (object)version);
            _logger.LogInformation(str);
            
            return Task.CompletedTask;
        }
        public async Task OnEventAsync(GameEvent E, Server S)// => Task.CompletedTask;
        {
            if (E.Type == GameEvent.EventType.Join ||E.Type == GameEvent.EventType.ChangePermission)
            {
                Thread.Sleep(10000);
                var rank_player_var = await _metaService.GetPersistentMeta(rank, E.Origin);
                rankName = E.Origin.Level.ClanTag(Config);

                rank_player_var = await _metaService.GetPersistentMeta("rank", E.Origin);
                if (rank_player_var == null)
                {
                    await _metaService.AddPersistentMeta("rank", "none", E.Origin);
                    rank_player_var = await _metaService.GetPersistentMeta("rank", E.Origin);
                }

                if (!(rank_player_var.Value.Contains("none")) && !(rank_player_var.Value.Contains("None")) && !(rank_player_var.Value.Contains("NONE")))
                {
                    rankName = rank_player_var.Value;

                }

                await S.ExecuteCommandAsync("setrank" + " " + E.Origin.ClientNumber + " " + rankName);
            }
            if (E.Type == GameEvent.EventType.Start || E.Type == GameEvent.EventType.MapEnd || E.Type == GameEvent.EventType.MapChange)
            {
                Thread.Sleep(10000);
                IList<EFClient> currentclients = E.Owner.Manager.GetActiveClients();
                foreach(EFClient client in currentclients)
                {
                    var rank_player_var = await _metaService.GetPersistentMeta(rank, client);
                    rankName = client.Level.ClanTag(Config);

                    rank_player_var = await _metaService.GetPersistentMeta("rank", client);
                    if (rank_player_var == null)
                    {
                        await _metaService.AddPersistentMeta("rank", "none", client);
                        rank_player_var = await _metaService.GetPersistentMeta("rank", client);
                    }

                    if (!(rank_player_var.Value.Contains("none")) && !(rank_player_var.Value.Contains("None")) && !(rank_player_var.Value.Contains("NONE")))
                    {
                        rankName = rank_player_var.Value;

                    }

                    await S.ExecuteCommandAsync("setrank" + " " + client.ClientNumber + " " + rankName);

                }

            }
            //return Task.CompletedTask;
        }
        public Task OnTickAsync(Server S)// =>
        {

            return Task.CompletedTask;

        }

        public Task OnUnloadAsync() => Task.CompletedTask;


    }
}
