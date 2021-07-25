using MediatR;

using Quran.Server.Application.Common.Exceptions;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Domain.Entities;

using System.Threading;
using System.Threading.Tasks;

namespace Quran.Server.Application.SampleEntities.Commands.UpdateSampleEntity
{
    public class UpdateSampleEntityCommandHandler : IRequestHandler<UpdateSampleEntityCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateSampleEntityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateSampleEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SampleEntities.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(SampleEntity), request.Id);
            }

            entity.SampleProperty = request.SampleProperty;
            entity.Done = request.Done;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}