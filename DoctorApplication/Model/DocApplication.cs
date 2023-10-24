using System.ComponentModel.DataAnnotations;

namespace DoctorApplication.Model
{
	public class DocApplication
	{
		[Key]
		public Guid Appointmentid { get; set; }

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
