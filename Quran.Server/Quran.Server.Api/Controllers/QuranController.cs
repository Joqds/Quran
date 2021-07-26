using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Quran.Server.Application.Quran;
using Quran.Server.Application.Quran.Queries.GetAyahChunk;
using Quran.Server.Application.Quran.Queries.GetSurahChunk;

namespace Quran.Server.Api.Controllers
{
    public class QuranController : ApiControllerBase
    {
        [HttpGet("GetAyatByPage")]
        public async Task<ActionResult<AyatChunkDto>> GetAyatByPage([FromQuery] int startPage,
            [FromQuery] int? endPage)
        {
            return await Mediator.Send(new GetAyatByPageQuery() {StartPageId = startPage, EndPageId = endPage});
        }


        [HttpGet("GetAyatBySurah")]
        public async Task<ActionResult<SurahChunkDto>> GetAyatBySurah([FromQuery]int surahId, [FromQuery] int? startPage,
            [FromQuery] int? endPage)
        {
            return await Mediator.Send(new GetAyatBySurahQuery
            {
                SurahId = surahId,
                StartPage = startPage,
                EndPage = endPage
            });
        }
    }
}