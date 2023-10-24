using Appointments.Models;

namespace Appointments.Services.Interface
{
    public interface IAppointmentService
    {
        Task<string> CreateAppointment(Appointment appointment);

        Task<List<Appointment>> GetAppointments();

        Task<string> UpdateAppointment(Appointment appointment);

        Task <string> DeleteAppointment(Appointment appointment);

        Task<Appointment> GetAppointmentById(Guid id);

		Task<Appointment> GetAppointmentByDoctorId(Guid id);


	}
}
