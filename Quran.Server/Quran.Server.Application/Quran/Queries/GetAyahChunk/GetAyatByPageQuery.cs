using MediatR;

namespace Quran.Server.Application.Quran.Queries.GetAyahChunk
{
    public class GetAyatByPageQuery : IRequest<AyatChunkDto>
    {
        public int StartPageId { get; set; }
        public int? FinishPageId { get; set; }
    }
}
