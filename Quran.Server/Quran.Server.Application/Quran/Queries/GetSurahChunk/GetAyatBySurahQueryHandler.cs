using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Application.Quran.Queries.GetSurahChunk
{
    public class GetAyatBySurahQueryHandler : IRequestHandler<GetAyatBySurahQuery, SurahChunkDto>
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;

        public GetAyatBySurahQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<SurahChunkDto> Handle(GetAyatBySurahQuery request, CancellationToken cancellationToken)
        {
            var ayahs = _context.Ayat
                .Include(x=>x.Surah)
                .Include(x=>x.Rub)
                .Where(x => x.SurahId == request.SurahId);

            if (request.StartPage.HasValue)
                ayahs = ayahs.Where(x => x.PageId >= request.StartPage);
            if (request.EndPage.HasValue)
                ayahs = ayahs.Where(x => x.PageId <= request.EndPage);

            Surah surah = await _context.Sovar.Include(x=>x.Ayat).SingleAsync(x=>x.Id==request.SurahId, cancellationToken: cancellationToken);

            return new SurahChunkDto(
                await ayahs.ProjectTo<AyahDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken),
                surah.Page,
                surah.Ayat.Max(x => x.PageId)
            );
        }
    }
}