using ClanTagRankApi;
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

                rankName = clientInfo.Level.ClanTag();

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

                rankName = clientInfo.Level.ClanTag();

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
