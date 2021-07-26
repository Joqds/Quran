using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quran.Server.Application.Quran;
using Quran.Server.Application.Quran.Queries.GetAyahChunk;

namespace Quran.Server.Api.Controllers
{
    public class QuranController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<AyatChunkDto>> GetAyatByPage([FromQuery] int startPage,
            [FromQuery] int? finishPage)
        {
            return await Mediator.Send(new GetAyatByPageQuery() {StartPageId = startPage, FinishPageId = finishPage});
        }
    }
}