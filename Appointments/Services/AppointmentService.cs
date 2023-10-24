
using Appointments.Data;
using Appointments.Models;
using Appointments.Services.Interface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Services
{
	public class AppointmentService : IAppointmentService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		
		public AppointmentService(AppDbContext appDbContext, IMapper mapper)
		{
			_context = appDbContext;
			_mapper = mapper;
			
		}



		public async Task<string> CreateAppointment(Appointment appointment)
		{
			_context.Appointments.Add(appointment);
			await _context.SaveChangesAsync();
			return "Appointment Submitted Succesfully";


		}

		public async Task<string> DeleteAppointment(Appointment appointment)
		{
			_context.Appointments.Remove(appointment);
			await _context.SaveChangesAsync();
			return "Appointment Deleted Succesfully";
		}

		public async Task<Appointment> GetAppointmentByDoctorId(Guid id)
		{
			return await _context.Appointments.FirstOrDefaultAsync(a => a.DoctorId == id);
		}

		public async Task<Appointment> GetAppointmentById(Guid id)
		{
			var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);
			if(appointment == null)
			{
				throw new Exception("Appointment not Found");
			}
			return appointment;
		}

		public Task<List<Appointment>> GetAppointments()
		{
			return _context.Appointments.ToListAsync();
		}

		public async Task<string> UpdateAppointment(Appointment appointment)
		{
			_context.Appointments.Update(appointment);
			await _context.SaveChangesAsync();
			return "Appointment Updated Succesfully";
		}
	}
}
