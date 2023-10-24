using Prescription_Service.Models;
using Prescription_Service.Models.DTO;

namespace Prescription_Service.Service
{
	public interface IPrescriptionService
	{
		Task<string> CreatePrescription(Prescription prescription);
		Task<string> UpdatePrescription(Prescription prescription);

		Task<StripeRequestDTO> StripePayment(StripeRequestDTO stripeRequestDto);
		Task<string> DeletePrescription(Prescription prescription);

		Task<Prescription> GetPrescriptionbyAppointment(Guid appointmentid);
		Task<List<Prescription>> GetAllPrescriptions();
		Task<Prescription> GetPrescription(Guid id);


	}
}
