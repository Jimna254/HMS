using System.ComponentModel.DataAnnotations.Schema;

namespace Prescription_Service.Models
{
	public class Prescription
	{
		public Guid Id { get; set; }
		public string Illness { get; set; }

		public int Price { get; set; }

		public Guid Patientid{ get; set; }

		public string Diagnosis { get; set; }

		public string Drugs { get; set; }

		public string DoctorID { get; set; }	
		
		public Guid AppointmentId { get; set; }
		public string PaymentIntentId { get; set; }
		public string StripeSessionId { get; set; }
	}
}
