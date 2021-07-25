using MediatR;

using Quran.Server.Application.Common.Exceptions;
using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Domain.Entities;

using System.Threading;
using System.Threading.Tasks;

namespace Quran.Server.Application.SampleEntities.Commands.DeleteSampleEntity
{
    public class DeleteSampleEntityCommandHandler : IRequestHandler<DeleteSampleEntityCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSampleEntityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteSampleEntityCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.SampleEntities.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(SampleEntity), request.Id);
            }

            _context.SampleEntities.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}