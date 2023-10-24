namespace Feedback_Service.Models.DTO_s
{
	public class FeedbackDTO
	{
		public string PatientId { get; set; }

		public Guid DoctorId { get; set; }
		public string Rating { get; set; }

		public string Comment { get; set; }

		public Guid AppointmentID { get; set; }
	}
}
