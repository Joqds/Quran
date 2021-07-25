
using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Application.Common.Mappings;
using Quran.Server.Application.Common.Models;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Quran.Server.Application.SampleEntities.Queries.GetSampleEntitiesWithPagination
{
    public class GetSampleEntitiesWithPaginationQueryHandler : IRequestHandler<GetSampleEntitiesWithPaginationQuery, PaginatedList<SampleEntityDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSampleEntitiesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SampleEntityDto>> Handle(GetSampleEntitiesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _context.SampleEntities
                .Where(x => x.SampleProperty.Contains(request.SamplePropertySearch))
                .OrderBy(x => x.Id)
                .ProjectTo<SampleEntityDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}
