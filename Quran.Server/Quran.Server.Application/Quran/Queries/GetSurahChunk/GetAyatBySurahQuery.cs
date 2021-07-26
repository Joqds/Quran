using MediatR;

namespace Quran.Server.Application.Quran.Queries.GetSurahChunk
{
    public class GetAyatBySurahQuery : IRequest<SurahChunkDto>
    {
        public int SurahId { get; set; }
        public int? StartPage { get; set; }
        public int? EndPage { get; set; }
    }
}