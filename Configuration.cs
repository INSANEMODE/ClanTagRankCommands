
using SharedLibraryCore.Interfaces;

namespace ClanTagRankApi
{
    public class Configuration : IBaseConfiguration
    {
        internal const string _name = "IW4MAdmin";

        public string WARNING { get; set; }
        public string User { get; set; }
        public string Trusted { get; set; }
        public string Moderator { get; set; }
        public string Admin { get; set; }
        public string SeniorAdmin { get; set; }
        public string Owner { get; set; }
        public string Creator { get; set; }
        public string Console { get; set; }


        public IBaseConfiguration Generate()
        {
            //this.RestartTimerLength = Utilities.PromptInt("How long in seconds until a server is killed after it is empty?", (string)null, 60, 86400, new int?(43200));
            this.WARNING = "Do Not Exceed 8 characters, names will be truncated.";
            this.User = "User";
            this.Trusted = "Trusted";
            this.Moderator = "Mod";
            this.Admin = "Admin";
            this.SeniorAdmin = "SrAdmin";
            this.Owner = "Owner";
            this.Creator = "Creator";
            this.Console = "Console";
            return (IBaseConfiguration)this;
        }

        string IBaseConfiguration.Name() => "ClanTagRankCommands";
    }
}
