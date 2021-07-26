using AutoMapper;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

using Quran.Server.Application.Common.Models;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quran.Server.Application.Quran;

namespace Quran.Server.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
            => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize);

        public static async Task<AyatChunkDto> ToAyatChunkAsync(this IQueryable<AyahDto> queryable)
            => new AyatChunkDto(await queryable.ToListAsync());

        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration)
            => queryable.ProjectTo<TDestination>(configuration).ToListAsync();
    }
}
