using Microsoft.AspNetCore.Identity;

namespace AuthService.Services.IServices
{
    public interface IJWTSevices
    {
        string GenerateToken(IdentityUser user, IEnumerable<string> roles);
    }
}
