using AutoMapper;
using Quran.Server.Application.Common.Mappings;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Application.Quran
{
    public class RubDto : IMapFrom
    {
        public int Id { get; set; }
        public int Joz { get; set; }
        public int RubInJoz { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Rub, RubDto>();
        }
    }
}