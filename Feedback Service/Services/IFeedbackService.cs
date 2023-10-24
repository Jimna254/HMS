using Feedback_Service.Models;

namespace Feedback_Service.Services
{
	public interface IFeedbackService
	{
		Task<string> CreateFeedback(Feedback feedback);

		Task<List<Feedback>> GetAllFeedbacks();

		Task<Feedback> GetFeedbackbyID(Guid id);

		Task<string> DeleteFeedback(Feedback feedback);

		Task<string>UpdateFeedback(Feedback feedback);

	}
}
