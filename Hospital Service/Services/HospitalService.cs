using AutoMapper;
using Hospital_Service.Data;
using Hospital_Service.Model;
using Microsoft.EntityFrameworkCore;

namespace Hospital_Service.Services
{
	public class HospitalService : IHospitalService
	{
		private readonly AppDbContext _context;

		public HospitalService(AppDbContext appDbContext)
		{
			_context = appDbContext;
		}


		public async Task<string> AddHospital(Hospital hospital)
		{
			_context.Hospitals.Add(hospital);
			await _context.SaveChangesAsync();
			return "Hospital Added Succesfully";
		}

		public async Task<string> DeletHospital(Hospital hospital)
		{
			_context.Hospitals.Remove(hospital);
			await _context.SaveChangesAsync();
			return "Hospital Deleted Successfully";
		}

		public async Task<Hospital> GetHospitalbyID(Guid id)
		{
			return await _context.Hospitals.FirstOrDefaultAsync(h => h.Id == id);

		}

		public async Task<List<Hospital>> GetAllHospitals()
		{
			return await _context.Hospitals.ToListAsync();
		}

		public async Task<string> UpdateHospital(Hospital hospital)
		{
			_context.Hospitals.Update(hospital);
			await _context.SaveChangesAsync();
			return "Hospital Updated Succesfully";
		}
	}
}
