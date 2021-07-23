using MediatR;
using Quran.Server.Application.Common.Models;

namespace Quran.Server.Application.SampleEntities.Queries.GetSampleEntitiesWithPagination
{
    public class GetSampleEntitiesWithPaginationQuery : IRequest<PaginatedList<SampleEntityDto>>
    {
        public string SamplePropertySearch { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}