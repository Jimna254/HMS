using AutoMapper;
using Feedback_Service.Data;
using Feedback_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedback_Service.Services
{
	public class FeedbackService : IFeedbackService

	{
		private readonly AppDbContext _context;
		private readonly IMapper _mapper;
		//private readonly IHttpClientFactory _httpClientFactory;
		public FeedbackService(AppDbContext appDbContext, IMapper mapper)
        {
			_context = appDbContext;
			_mapper = mapper;
            
        }
        public async Task<string> CreateFeedback(Feedback feedback)
		{
			_context.Feedbacks.Add(feedback);
			await _context.SaveChangesAsync();
			return "Feedback Created Succesfully";
		}

		public async Task<string> DeleteFeedback(Feedback feedback)
		{
			_context.Feedbacks.Remove(feedback);
			await _context.SaveChangesAsync();
			return "Feedback Deleted Succesfully";
		}

		public async Task<List<Feedback>> GetAllFeedbacks()
		{
			return await _context.Feedbacks.ToListAsync();
		}


		public async Task<Feedback> GetFeedbackbyID(Guid id)
		{
			return await _context.Feedbacks.FirstOrDefaultAsync(f => f.Id == id);

		}

		public async Task<string> UpdateFeedback(Feedback feedback)
		{
			_context.Feedbacks.Update(feedback);
			await _context.SaveChangesAsync();
			return "Feedback Updated Succesfully";

		}
	}
}
