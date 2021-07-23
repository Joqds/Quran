using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Infrastructure.Persistence.Configurations
{
    public class SampleEntityConfiguration : IEntityTypeConfiguration<SampleEntity>
    {
        public void Configure(EntityTypeBuilder<SampleEntity> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            
        }
    }
}