using AutoMapper;
using Feedback_Service.Models;
using Feedback_Service.Models.DTO_s;
using Feedback_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Feedback_Service.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeedbackController : ControllerBase
	{
        private readonly IMapper _mapper;
        private readonly IFeedbackService _feedbackService;
        private readonly ResponseDTO _responseDTO;
        public FeedbackController(IMapper mapper, IFeedbackService feedbackService)
        {
            _mapper = mapper;
            _feedbackService = feedbackService;
            _responseDTO = new ResponseDTO();
            
        }

		[HttpPost]
		[Authorize]
		public async Task<ActionResult<ResponseDTO>> AddAsync(FeedbackDTO addfeedback)
		{
			try
			{
				//map the addfeedback to Feedback
				var newfeedback = _mapper.Map<Feedback>(addfeedback);

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				newfeedback.PatientId = userIdClaim.Value;
				_responseDTO.Result = await _feedbackService.CreateFeedback(newfeedback);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Feedback Made Succesfully";
				return Ok(_responseDTO);

			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.Message;
				return BadRequest(_responseDTO);
			}
		}
		[HttpGet]
		public async Task<ActionResult<ResponseDTO>> GetFeedbackAsync()
		{
			try
			{
				_responseDTO.Result = await _feedbackService.GetAllFeedbacks();
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Feedbacks fetched Succesfully";
				return Ok(_responseDTO);


			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.Message;
				return BadRequest(_responseDTO);
			}
		}

		[HttpGet("{id}")]
		[Authorize]

		public async Task<ActionResult<ResponseDTO>> GetFeedbackById(Guid id)
		{
			try
			{

				_responseDTO.Result = await _feedbackService.GetFeedbackbyID(id);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Feedback Fetched Successfully";
				return Ok(_responseDTO);


			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.Message;
				return BadRequest(_responseDTO);
			}
		}
		[HttpPut]
		[Authorize]
		public async Task<ActionResult<ResponseDTO>> UpdateAppointmentAsync(FeedbackDTO feedbackDTO, Guid id)
		{

			try
			{
				//check if the post exists
				var feedback = await _feedbackService.GetFeedbackbyID(id);
				if (feedback == null)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "Feedback Not Found";
					return NotFound(_responseDTO);
				}

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				string userId = userIdClaim.Value;

				//only the user who created the Feedback can update it
				if (feedback.PatientId != userId)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "You are not authorized to update this feedback";
					return Unauthorized(_responseDTO);
				}

				var post = _mapper.Map(feedbackDTO, feedback);

				_responseDTO.Result = await _feedbackService.UpdateFeedback(feedback);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Feedback Updated Successfully";
				return Ok(_responseDTO);


			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.InnerException.Message;
				return BadRequest(_responseDTO);
			}
		}
		[HttpDelete]
		[Authorize]
		public async Task<ActionResult<ResponseDTO>> DeleteFeedbackAsync(Guid id)
		{
			try
			{
				//check if feedback exists
				var feedback = await _feedbackService.GetFeedbackbyID(id);
				if (feedback == null)
				{
					return NotFound(_responseDTO);
				}

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				string userId = userIdClaim.Value;

				//only the user who created the delete it
				if (feedback.PatientId != userId)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "You are not authorized to delete feedback";
					return Unauthorized(_responseDTO);
				}

				//Delete Feedback
				await _feedbackService.DeleteFeedback(feedback);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Feedback Deleted Successfully";
				return Ok(_responseDTO);

			}
			catch
			(Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.Message;
				return BadRequest(_responseDTO);
			}
		}


	}
}
