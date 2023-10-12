using System.ComponentModel.DataAnnotations;

namespace Health_Man_Sys_FrontEnd.Models.User
{
    public class UserDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string Role { get; set; } = "Patient";

    }
}
