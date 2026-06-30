using Microsoft.AspNetCore.Identity;

namespace PhoenixKC.Infrastructure.Features.Auth;

public sealed class PhoenixUserEntity : IdentityUser<Guid>
{
    public PhoenixUserEntity()
    {
        base.Id = Guid.CreateVersion7();
    }
}