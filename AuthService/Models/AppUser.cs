using Microsoft.AspNetCore.Identity;

namespace AuthService.Models
{
    public class AppUser :IdentityUser
    {
        public string Licence { get; set; } = string.Empty;
        public string? Hospitalid { get; set; }


    }
}
