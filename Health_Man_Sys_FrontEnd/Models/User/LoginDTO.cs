using System.ComponentModel.DataAnnotations;

namespace Health_Man_Sys_FrontEnd.Models.User
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
