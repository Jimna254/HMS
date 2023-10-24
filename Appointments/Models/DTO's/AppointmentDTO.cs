using Appointments.Models.Enum;

namespace Appointments.Models.DTO_s
{
    public class AppointmentDTO
    {
		public Guid Hospital { get; set; }
		public Guid DoctorId { get; set; }
		public Guid PatientId { get; set; }
		public string Symptoms { get; set; }
		public Departments Department { get; set; } = Departments.Dental;
		public string Date { get; set; }
		public string Time { get; set; }
	}
}
