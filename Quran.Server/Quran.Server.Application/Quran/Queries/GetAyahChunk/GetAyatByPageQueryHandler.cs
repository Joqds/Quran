using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Application.Common.Mappings;

namespace Quran.Server.Application.Quran.Queries.GetAyahChunk
{
    public class GetAyatByPageQueryHandler : IRequestHandler<GetAyatByPageQuery, AyatChunkDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAyatByPageQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AyatChunkDto> Handle(GetAyatByPageQuery request, CancellationToken cancellationToken)
        {
            var ayahs = _context.Ayat.AsQueryable();

            if (request.FinishPageId.HasValue)
                ayahs = ayahs.Where(x => x.PageId >= request.StartPageId && x.PageId <= request.FinishPageId);
            else
                ayahs = ayahs.Where(x => x.PageId == request.StartPageId);

            return await ayahs.ProjectTo<AyahDto>(_mapper.ConfigurationProvider)
                .ToChunkAyatAsync();
        }
    }
}