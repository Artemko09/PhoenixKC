using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhoenixKC.Infrastructure.Features.Health;

public sealed class HealthConfiguration : IEntityTypeConfiguration<HealthEntity>
{
    #region IEntityTypeConfiguration<HealthEntity>
    public void Configure(EntityTypeBuilder<HealthEntity> builder)
    {
        builder.ToTable("Health");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(HealthConstants.NameMaxLength);
    }
    #endregion
}