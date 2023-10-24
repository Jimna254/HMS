using DoctorApplication.Data;
using DoctorApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace DoctorApplication.Services
{
	public class ApplicationService : IApplicationService
	{
		private readonly AppDbContext _context;

		public ApplicationService(AppDbContext context)
		{
			_context = context;
		}
		public async Task<string> AddApplication(DocApplication application)
		{
			_context.DoctorApplications.Add(application);
			await _context.SaveChangesAsync();
			return "Application Added Succesfully";
		}

		public async Task<DocApplication> GetApplication(Guid id)
		{
			return await _context.DoctorApplications.FirstOrDefaultAsync(a => a.Appointmentid == id);	
		}

		public async Task<List<DocApplication>> GetAllApplications()
		{
			return await _context.DoctorApplications.ToListAsync();
		}
	}
	
}
