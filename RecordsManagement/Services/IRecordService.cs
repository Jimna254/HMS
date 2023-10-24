using RecordsManagement.Models;

namespace RecordsManagement.Services
{
	public interface IRecordService
	{
		Task<Record> GetRecord(Guid id);
		Task<List<Record>> GetAllRecords();
		Task<string> DeleteRecord(Record record);	
		Task<string> UpdateRecord(Record record);
		Task<string> CreateRecord(Record record);


	}
}
