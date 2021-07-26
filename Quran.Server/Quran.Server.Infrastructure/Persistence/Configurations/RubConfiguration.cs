using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Infrastructure.Persistence.Configurations
{
    public class RubConfiguration:IEntityTypeConfiguration<Rub>
    {
        public void Configure(EntityTypeBuilder<Rub> builder)
        {
            builder
                .HasMany(x => x.Ayat)
                .WithOne(x => x.Rub)
                .HasForeignKey(x => x.RubId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Ayah_Rub");
        }
    }
}