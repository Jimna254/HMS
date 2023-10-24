namespace Appointments.Models.DTO_s
{
	public class DoctorAppRequest
	{
		
		public string PatientId { get; set; }

		public string Symptoms { get; set; }

		public DateOnly Date { get; set; }
		public string Time { get; set; }



	}
}
