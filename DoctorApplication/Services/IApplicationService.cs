using DoctorApplication.Model;

namespace DoctorApplication.Services
{
	public interface IApplicationService
	{
		Task<string> AddApplication(DocApplication application);

		Task<List<DocApplication>> GetAllApplications();

		Task<DocApplication> GetApplication(Guid id);

		
	}
}
