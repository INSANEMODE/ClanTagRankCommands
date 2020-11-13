
using SharedLibraryCore.Interfaces;

namespace ClanTagRankApi
{
    internal class Configuration : IBaseConfiguration
    {
        internal const string _name = "IW4MAdmin";

        public int RestartTimerLength { get; set; }

        public IBaseConfiguration Generate()
        {
            //this.RestartTimerLength = Utilities.PromptInt("How long in seconds until a server is killed after it is empty?", (string)null, 60, 86400, new int?(43200));
            return (IBaseConfiguration)this;
        }

        string IBaseConfiguration.Name() => "ClanTagRankApi";
    }
}
