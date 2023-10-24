namespace AuthService.Models.DTO_s
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; } = default!;

        public string Token { get; set; } = string.Empty;
    }
}
