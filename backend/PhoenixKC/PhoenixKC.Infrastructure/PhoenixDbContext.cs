using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhoenixKC.Infrastructure.Features.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PhoenixKC.Infrastructure;

public sealed class PhoenixDbContext(
    DbContextOptions<PhoenixDbContext> options
) : IdentityDbContext<PhoenixUserEntity, IdentityRole<Guid>, Guid>(options)
{
    #region Instance
    // Add DbSet properties for your entities here
    #endregion

    #region Base
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Add other entity configurations here
    }
    #endregion
}