using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Infrastructure.Persistence.Configurations
{
    public class AyahConfiguration:IEntityTypeConfiguration<Ayah>
    {
        public void Configure(EntityTypeBuilder<Ayah> builder)
        {
            builder
                .HasMany(x => x.FirstRub)
                .WithOne(x => x.FirstAyah)
                .HasForeignKey(x => x.FirstAyahId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .HasMany(x => x.LastRub)
                .WithOne(x => x.LastAyah)
                .HasForeignKey(x => x.LastAyahId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}