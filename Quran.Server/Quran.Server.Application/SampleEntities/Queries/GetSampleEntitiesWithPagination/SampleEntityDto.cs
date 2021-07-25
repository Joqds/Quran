using AutoMapper;

using Quran.Server.Application.Common.Mappings;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Application.SampleEntities.Queries.GetSampleEntitiesWithPagination
{
    public class SampleEntityDto : IMapFrom
    {
        public string SampleProperty { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SampleEntity, SampleEntityDto>();
        }
    }
}