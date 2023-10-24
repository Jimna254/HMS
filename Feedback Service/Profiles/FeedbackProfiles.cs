using AutoMapper;
using Feedback_Service.Models;
using Feedback_Service.Models.DTO_s;

namespace Feedback_Service.Profiles
{
	public class FeedbackProfiles : Profile
	{
		public FeedbackProfiles() { 
		CreateMap<FeedbackDTO, Feedback>().ReverseMap();
		
		}
	}
}
