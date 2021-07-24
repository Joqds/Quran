using MediatR;
using Quran.Server.Application.Common.Security;

namespace Quran.Server.Application.SampleEntities.Commands.UpdateSampleEntity
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "CanUpdate")]
    public class UpdateSampleEntityCommand : IRequest
    {
        public int Id { get; set; }

        public string SampleProperty { get; set; }

        public bool Done { get; set; }
    }
}
