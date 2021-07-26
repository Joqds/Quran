using MediatR;

using Quran.Server.Application.Common.Security;

namespace Quran.Server.Application.SampleEntities.Commands.DeleteSampleEntity
{
    [Authorize(Roles = "Administrator")]
    [Authorize(Policy = "CanDelete")]
    public class DeleteSampleEntityCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
