using AuthService.Data;
using AuthService.Services.IServices;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using AuthService.Models;
using AuthService.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Services
{
    public class UserServices: IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IJWTSevices _tokenGenerator;
        private readonly AppDbContext _appDbContext;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserServices(IMapper mapper, UserManager<AppUser> userManager, IJWTSevices jWTSevices, AppDbContext appDbContext, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _roleManager = roleManager;
            _tokenGenerator = jWTSevices;


        }

        public Task<List<AppUser>> getAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<AppUser> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDTO> LoginUser(LoginDTO loginDto)
        {
            // Get user by username
            var user = await _appDbContext.AppUsers.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginDto.Username.ToLower());
            if (user == null)
            {

                return null;
            }

            // Check if password is the right one
            var isValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isValid)
            {
                // Handle the case when the password is wrong
                return null;
            }

            // User provided the correct credentials
            var roles = await _userManager.GetRolesAsync(user);
            // Create Token
            var token = _tokenGenerator.GenerateToken(user, roles);

            var loggedUser = new LoginResponseDTO()
            {
                User = _mapper.Map<UserDTO>(user),
                Token = token
            };

            return loggedUser;
        }

        public async Task<bool> AssignUserRole(string email, string Rolename)
        {

            //Get user by email
            var user = await _appDbContext.AppUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {

                //user Exist and we can assign a role
                //check if role exist
                if (!_roleManager.RoleExistsAsync(Rolename).GetAwaiter().GetResult())
                {
                    //first create it
                    _roleManager.CreateAsync(new IdentityRole(Rolename)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, Rolename);

                return true;
            }
            return false;
        }
        public async Task<string> RegisterUser(RegisterDTO registrationDto)
        {
            var user = _mapper.Map<AppUser>(registrationDto);

            try
            {
                var result = await _userManager.CreateAsync(user, registrationDto.Password);

                if (result.Succeeded)
                {
                    // User registration was successful, return a success message.
                    return "User registered successfully.";
                }
                else
                {
                    // User registration failed, return an appropriate error message.
                    var errorMessage = string.Join(", ", result.Errors.Select(error => error.Description));

                    return errorMessage;
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }

}
