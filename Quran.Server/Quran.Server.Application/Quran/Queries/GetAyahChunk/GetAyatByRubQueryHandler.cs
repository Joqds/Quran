using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Application.Common.Mappings;

namespace Quran.Server.Application.Quran.Queries.GetAyahChunk
{
    public class GetAyatByRubQueryHandler : IRequestHandler<GetAyatByRubQuery, AyatChunkDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAyatByRubQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<AyatChunkDto> Handle(GetAyatByRubQuery request, CancellationToken cancellationToken)
        {
            var ayahs = _context.Ayat
                .Include(x=>x.Rub)
                .Include(x=>x.Surah)
                .Where(x=>x.RubId==request.RubId);

            

            return await ayahs.ProjectTo<AyahDto>(_mapper.ConfigurationProvider)
                .OrderBy(x=>x.Id)
                .ToAyatChunkAsync();
        }
    }
}