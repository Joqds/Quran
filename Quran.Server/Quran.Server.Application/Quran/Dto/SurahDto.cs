using AutoMapper;
using Quran.Server.Application.Common.Mappings;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Application.Quran
{
    public class SurahDto : IMapFrom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Surah, SurahDto>();
        }
    }
}