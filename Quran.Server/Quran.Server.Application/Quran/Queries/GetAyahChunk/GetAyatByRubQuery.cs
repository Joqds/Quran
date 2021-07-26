using MediatR;

namespace Quran.Server.Application.Quran.Queries.GetAyahChunk
{
    public class GetAyatByRubQuery : IRequest<AyatChunkDto>
    {
        public int RubId { get; set; }
    }
}