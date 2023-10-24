using AutoMapper;
using Hospital_Service.Model;

namespace Hospital_Service.Profiles
{
	public class HospitalProfiles : Profile
	{
        public HospitalProfiles()
        {
            CreateMap<HospitalDTO, Hospital>().ReverseMap();
        }
    }
}
