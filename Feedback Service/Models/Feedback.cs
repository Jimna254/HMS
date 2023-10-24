namespace Feedback_Service.Models
{
	public class Feedback
	{
		public Guid Id { get; set; }

		public string PatientId { get; set; }

		public Guid DoctorId { get; set; }
		public string Rating { get; set; }

		public string Comment { get; set; }

		public Guid AppointmentID { get; set; }
		
	}
}
