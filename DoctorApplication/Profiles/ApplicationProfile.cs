using AutoMapper;
using DoctorApplication.Model;
using DoctorApplication.Model.DTO_s;

namespace DoctorApplication.Profiles
{
	public class ApplicationProfile : Profile
	{
		public ApplicationProfile() {
			CreateMap<DocApplicationDTO, DocApplication>().ReverseMap();
		
		}
	}
}
