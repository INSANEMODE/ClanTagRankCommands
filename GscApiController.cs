using Microsoft.AspNetCore.Mvc;
using SharedLibraryCore;
using SharedLibraryCore.Database.Models;
using SharedLibraryCore.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebfrontCore.Controllers.API
{

    [Route("api/gsc/[action]")]
    public class GscApiController : BaseController
    {
        string rankName;
        EFMeta customRankName;
        private readonly IMetaService _metaService;
        public GscApiController(IManager manager, IMetaService metaService) : base(manager)
        {
            _metaService = metaService;
        }

        /// <summary>
        /// grabs basic info about the client from IW4MAdmin
        /// </summary>
        /// <param name="clientname"></param>
        /// <returns></returns>
        [HttpGet("{clientguid}")]
        public async Task<IActionResult> Clientguid(string clientguid)
        {
            long decimalNetworkId = clientguid.ConvertGuidToLong(System.Globalization.NumberStyles.Integer);
            var clientInfo = Manager.GetActiveClients()
                .FirstOrDefault(c => c.NetworkId == decimalNetworkId);

            if (clientInfo != null)
            {

                switch ((int)clientInfo.Level)
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
                        break;
                }

                customRankName = await _metaService.GetPersistentMeta("rank", clientInfo);
                if (customRankName == null)
                {
                    await _metaService.AddPersistentMeta("rank", "none", clientInfo);
                    customRankName = await _metaService.GetPersistentMeta("rank", clientInfo);
                }

                if (!(customRankName.Value.Contains("none")) && !(customRankName.Value.Contains("None")) && !(customRankName.Value.Contains("NONE")))
                {
                    rankName = customRankName.Value;

                }

                var sb = new StringBuilder();
                sb.AppendLine(rankName);

                return Content(sb.ToString());
            }

            return Content("Error: Client info is null");
        }
        [HttpGet("{clientname}")]
        public async Task<IActionResult> Clientname(string clientname)
        {

            var clientInfo = Manager.GetActiveClients()
                .FirstOrDefault(c => c.Name == clientname);


            if (clientInfo != null)
            {

                switch ((int)clientInfo.Level)
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
                        break;
                }

                customRankName = await _metaService.GetPersistentMeta("rank", clientInfo);
                if (customRankName == null)
                {
                    await _metaService.AddPersistentMeta("rank", "none", clientInfo);
                    customRankName = await _metaService.GetPersistentMeta("rank", clientInfo);
                }

                if (!(customRankName.Value.Contains("none")) || !(customRankName.Value.Contains("None")) || !(customRankName.Value.Contains("NONE")))
                {
                    rankName = customRankName.Value;

                }

                var sb = new StringBuilder();
                sb.AppendLine(rankName);

                return Content(sb.ToString());
            }

            return Content("Error: Client info is null");
        }
    }
}
