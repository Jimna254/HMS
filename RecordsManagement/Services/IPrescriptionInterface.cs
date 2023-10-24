using RecordsManagement.Models.DTO;

namespace RecordsManagement.Services
{
	public interface IPrescriptionInterface
	{
		Task<List<Prescription>> GetPrescriptionbyId(Guid id);
	}
}
