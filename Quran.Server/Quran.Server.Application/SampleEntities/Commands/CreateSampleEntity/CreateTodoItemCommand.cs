using MediatR;

using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Domain.Entities;
using Quran.Server.Domain.Events;

using System.Threading;
using System.Threading.Tasks;

namespace Quran.Server.Application.SampleEntities.Commands.CreateSampleEntity
{
    public class CreateSampleEntityCommandHandler : IRequestHandler<CreateSampleEntityCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateSampleEntityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateSampleEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = new SampleEntity
            {
                SampleProperty = request.SampleProperty,
                Done = false,
            };

            entity.DomainEvents.Add(new SampleEvent(entity));

            _context.SampleEntities.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
