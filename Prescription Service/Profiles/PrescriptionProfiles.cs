using AutoMapper;
using Prescription_Service.Models;
using Prescription_Service.Models.DTO;

namespace Prescription_Service.Profiles
{
	public class PrescriptionProfiles : Profile
	{
        public PrescriptionProfiles()
        {
            CreateMap<PrescriptionDTO, Prescription>().ReverseMap();
        }

    }
}
