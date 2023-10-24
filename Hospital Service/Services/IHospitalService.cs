using Hospital_Service.Migrations;
using Hospital_Service.Model;

namespace Hospital_Service.Services
{
	public interface IHospitalService
	{
		Task<string> AddHospital(Hospital hospital);

		Task<List<Hospital>> GetAllHospitals();

		Task<Hospital> GetHospitalbyID(Guid id);

		Task<string> DeletHospital(Hospital hospital);

		Task<string> UpdateHospital(Hospital hospital);
	}
}
