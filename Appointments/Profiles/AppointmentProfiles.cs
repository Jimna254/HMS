using Appointments.Models;
using Appointments.Models.DTO_s;
using AutoMapper;

namespace Appointments.Profiles
{
	public class AppointmentProfiles : Profile
	{
        public AppointmentProfiles()
        {
            CreateMap<AppointmentDTO, Appointment>().ReverseMap();
            
        }

    }
}
