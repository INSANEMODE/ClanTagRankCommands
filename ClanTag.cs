
using SharedLibraryCore.Database.Models;
using SharedLibraryCore;
using SharedLibraryCore.Interfaces;

namespace ClanTagRankApi
{

    static class ExtensionMethods
    {
        public static string Truncate(this string input, int strLength)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Length <= strLength ? input : input.Substring(0, strLength);
        }
        public static string ClanTag(this EFClient.Permission level, Configuration Config)
        {
            string rankName;

            switch ((int)level)
            {
                case -1:
                    rankName = "Banned";  //this typically won't be seen. 
                    break;
                case 0:
                    rankName = Config.User;
                    break;
                case 1:
                    rankName = Config.User;  //1 = flagged, but don't want to show this in game.
                    break;
                case 2:
                    rankName = Config.Trusted;
                    break;
                case 3:
                    rankName = Config.Moderator;
                    break;
                case 4:
                    rankName = Config.Admin;
                    break;
                case 5:
                    rankName = Config.SeniorAdmin;
                    break;
                case 6:
                    rankName = Config.Owner;
                    break;
                case 7:
                    rankName = Config.Creator;
                    break;
                case 8:
                    rankName = Config.Console;
                    break;
                default:
                    rankName = Config.User;
                    break;
            }
            return rankName.Truncate(8);
        }



    }
}



