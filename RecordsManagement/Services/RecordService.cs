using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordsManagement.Data;
using RecordsManagement.Models;

namespace RecordsManagement.Services
{
	public class RecordService : IRecordService
	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
        public RecordService(IMapper mapper, AppDbContext appDbContext)
        {
            _context = appDbContext;
			_mapper = mapper;
        }
        public async Task<string> CreateRecord(Record record)
		{
			_context.Records.Add(record);
			await _context.SaveChangesAsync();
			return "Record Created Successfully";
			
		}

		public async Task<string> DeleteRecord(Record record)
		{
			_context.Records.Remove(record);
			await _context.SaveChangesAsync();
			return "Record Deleted Succesfully";
		}

		public async Task<List<Record>> GetAllRecords()
		{
			return await _context.Records.ToListAsync();
		}

		public async Task<Record> GetRecord(Guid id)
		{
			return await _context.Records.FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<string> UpdateRecord(Record record)
		{
			_context.Records.Update(record);
			await _context.SaveChangesAsync();
			return "Record Updated Succesfully";
		}
	}
}
