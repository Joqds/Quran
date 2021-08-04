using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quran.Server.Application.Common.Interfaces;

namespace Quran.Server.Application.Quran.Queries.GetSurahList
{
    public class GetSurahListQueryHandler : IRequestHandler<GetSurahListQuery, List<SurahDto>>
    {
        private readonly IApplicationDbContext _context;

        private readonly IMapper _mapper;

        public GetSurahListQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SurahDto>> Handle(GetSurahListQuery request, CancellationToken cancellationToken)
        {

            return await _context.Sovar
                .Include(x=>x.Ayat)
                .ProjectTo<SurahDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);

        }
    }
}