using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoenixKC.Infrastructure.Features.Auth;
using PhoenixKC.Infrastructure.Features.Health;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PhoenixKC.Infrastructure;

public sealed class PhoenixDbContext(
    DbContextOptions<PhoenixDbContext> options
) : IdentityDbContext<PhoenixUserEntity, IdentityRole<Guid>, Guid>(options)
{
    #region Instance
    public DbSet<HealthEntity> Health { get; set; } = null!; //Init by EFCore
    #endregion

    #region Base
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(PhoenixDbContext).Assembly);
    }
    #endregion
}