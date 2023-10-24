namespace Prescription_Service.Models.DTO
{
	public class PrescriptionDTO
	{
		public string Illness { get; set; }

		public int Price { get; set; }

		public Guid Patientid { get; set; }

		public string Diagnosis { get; set; }

		public string Drugs { get; set; }

		public string DoctorID { get; set; }

		public Guid AppointmentId { get; set; }
	}
}
