using System.ComponentModel.DataAnnotations;

namespace DoctorApplication.Model.DTO_s
{
	public class DocApplicationDTO
	{
		public string Licence { get; set; } = string.Empty;
		public Guid Hospitalid { get; set; }

		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public string Name { get; set; } = string.Empty;
		[Required]
		public string PhoneNumber { get; set; } = string.Empty;
	}
}
