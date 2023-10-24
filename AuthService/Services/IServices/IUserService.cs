using AuthService.Models;
using AuthService.Models.DTO_s;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Services.IServices
{
    public interface IUserService
    {
        public Task<bool> AssignUserRole(string email, string Rolename);
        Task<string> RegisterUser(RegisterDTO registrationDto);

        public Task<LoginResponseDTO> LoginUser(LoginDTO loginDto);

        Task<List<AppUser>> getAllUsers();

        Task<AppUser> GetUserById(string id);

    }
}
