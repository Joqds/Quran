using MediatR;

namespace Quran.Server.Application.Quran.Queries.GetAyahChunk
{
    public class GetAyatByJozQuery : IRequest<AyatChunkDto>
    {
        public int JozId { get; set; }
    }
}