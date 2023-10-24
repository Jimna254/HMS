using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.DTO_s
{
    public class RegisterDTO
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? Role { get; set; } = string.Empty;
		public string Licence { get; set; } = string.Empty;
		public string? Hospitalid { get; set; }

	}
}
