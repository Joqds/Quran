using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using IdentityServer4.EntityFramework.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Quran.Server.Application.Common.Interfaces;
using Quran.Server.Domain.Common;
using Quran.Server.Domain.Entities;
using Quran.Server.Infrastructure.Identity;

namespace Quran.Server.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser,ApplicationRole,Guid>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }

        public DbSet<SampleEntity> SampleEntities { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<ISoftDeleteEntity>())
            {
                if (entry.State != EntityState.Deleted) continue;
                entry.Entity.Deleted = _dateTime.Now;
                entry.Entity.DeletedBy = _currentUserService.UserId;
                entry.Entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            RegisterSoftDelete(builder);
            base.OnModelCreating(builder);
        }

        private void RegisterSoftDelete(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeleteEntity).IsAssignableFrom(entityType.ClrType))
                {
                    //                    builder.Entity(entityType.ClrType, typeBuilder =>
                    //                        {
                    //                            typeBuilder.HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted")==false);
                    //                        }
                    var isDeletedProperty = entityType.FindProperty("IsDeleted");
                    if (isDeletedProperty!=null&& isDeletedProperty.ClrType==typeof(bool))
                    {
                        var parameter = Expression.Parameter(entityType.ClrType, "p");
                        var filter = Expression.Lambda(Expression.Negate(Expression.Property(parameter, isDeletedProperty.PropertyInfo)), parameter);
                        entityType.SetQueryFilter(filter);
                    }
                }
            }
        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker
                    .Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .FirstOrDefault(domainEvent => !domainEvent.IsPublished);
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }
    }
}
