using Microsoft.Identity.Client;

namespace RecordsManagement.Models
{
	public class Record
	{
		public Guid Id { get; set; }

		public string DoctorId { get; set; }

		public Guid PatientId { get; set; }
		public string Date { get; set; }

		public string Time { get; set; }

		public string Diagnosis { get; set; }

		public string Drugs { get; set; }
		public string Illness { get; set; }

		public int Price { get; set; }
		
		public string PaymentStatus { get; set; }

		public Guid AppointmentId { get; set; }






	}
}
