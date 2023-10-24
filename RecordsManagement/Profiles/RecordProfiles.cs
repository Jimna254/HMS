using AutoMapper;
using RecordsManagement.Models;
using RecordsManagement.Models.DTO;

namespace RecordsManagement.Profiles
{
	public class RecordProfiles : Profile
	{
        public RecordProfiles()
        {
            CreateMap<RecordDTO, Record>().ReverseMap();
        }
    }
}
