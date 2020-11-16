using SharedLibraryCore;
using SharedLibraryCore.Interfaces;
using System.Threading.Tasks;


namespace ClanTagRankApi
{

    public class Plugin : IPlugin
    {
        private readonly IConfigurationHandler<Configuration> _configurationHandler;
        private Configuration Config;
        readonly string rank = "rank";



        public string Name => "ClanTagRankApi";

        public float Version => 1.01f;

        public string Author => "INSANEMODE";

        private readonly IMetaService _metaService;

        public Plugin(IMetaService metaService, IConfigurationHandlerFactory configurationHandlerFactory)
        {
            _metaService = metaService;
            _configurationHandler = (IConfigurationHandler<Configuration>)configurationHandlerFactory.GetConfigurationHandler<Configuration>("ClanTagRankApi");
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
            manager.GetLogger(0L).WriteVerbose(str);
            
            return Task.CompletedTask;
        }
        public async Task OnEventAsync(GameEvent E, Server S)// => Task.CompletedTask;
        {
            if (E.Type == GameEvent.EventType.Join)
            {
                var rank_player_var = await _metaService.GetPersistentMeta(rank, E.Target);
                if (rank_player_var == null)
                {
                    await _metaService.AddPersistentMeta(rank, "none", E.Target);
                    rank_player_var = await _metaService.GetPersistentMeta(rank, E.Target);
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
