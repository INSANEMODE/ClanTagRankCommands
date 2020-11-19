
using SharedLibraryCore.Database.Models;


namespace ClanTagRankApi
{
    public static class ExtensionMethods
    {
        public static string ClanTag(this EFClient.Permission level)
        {
            string rankName;
            switch ((int)level)
            {
                case -1:
                    rankName = "Banned";  //this typically won't be seen. 
                    break;
                case 0:
                    rankName = "User";
                    break;
                case 1:
                    rankName = "User";  //1 = flagged, but don't want to show this in game.
                    break;
                case 2:
                    rankName = "Trusted";
                    break;
                case 3:
                    rankName = "Mod";
                    break;
                case 4:
                    rankName = "Admin";
                    break;
                case 5:
                    rankName = "SrAdmin";
                    break;
                case 6:
                    rankName = "Owner";
                    break;
                case 7:
                    rankName = "Creator";
                    break;
                case 8:
                    rankName = "Console";
                    break;
                default:
                    rankName = "User";
                    break;
            }
            return rankName;
        }


    }
}



