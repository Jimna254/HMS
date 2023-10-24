using Appointments.Models;
using Appointments.Models.DTO_s;
using Appointments.Services.Interface;
using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Appointments.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppointmentController : ControllerBase
	{
		private readonly IAppointmentService _appointmentService;
		private readonly ResponseDTO _responseDTO;
		private readonly IMapper _mapper;
		public AppointmentController(IAppointmentService appointmentService, IMapper mapper)
		{
			_appointmentService = appointmentService;
			_mapper = mapper;
			_responseDTO = new ResponseDTO();
		}

		//Add
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<ResponseDTO>> AddAsync(AppointmentDTO addappointment)
		{
			try
			{
				//map addappointment to Appointment
				var newappointment = _mapper.Map<Appointment>(addappointment);

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				newappointment.PatientId = userIdClaim.Value;
				_responseDTO.Result = await _appointmentService.CreateAppointment(newappointment);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Appointment Placed Successfully";
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
		public async Task<ActionResult<ResponseDTO>> GetAppointment()
		{
			try
			{
				_responseDTO.Result = await _appointmentService.GetAppointments();
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Appointments Fetched Successfully";
				return Ok(_responseDTO);


			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.Message;
				return BadRequest(_responseDTO);
			}
		}

		//Get by id
		[HttpGet("{id}")]
		[Authorize]

		public async Task<ActionResult<ResponseDTO>> GetappointmentById(Guid id)
		{
			try
			{

				_responseDTO.Result = await _appointmentService.GetAppointmentById(id);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Appointment Fetched Successfully";
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
		public async Task<ActionResult<ResponseDTO>> UpdateAppointmentAsync(AppointmentDTO appointmentDTO, Guid id)
		{

			try
			{
				//check if the post exists
				var appointmentid = await _appointmentService.GetAppointmentById(id);
				if (appointmentid == null)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "Appointment Not Found";
					return NotFound(_responseDTO);
				}

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				string userId = userIdClaim.Value;

				//only the user who created the appointment can update it
				if (appointmentid.PatientId != userId)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "You are not authorized to update this Appointment";
					return Unauthorized(_responseDTO);
				}

				var post = _mapper.Map(appointmentDTO, appointmentid);

				_responseDTO.Result = await _appointmentService.UpdateAppointment(appointmentid);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Appointment Updated Successfully";
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
		public async Task<ActionResult<ResponseDTO>> DeleteAppointment(Guid id)
		{
			try
			{
				//check if appointment exists
				var appointmentid = await _appointmentService.GetAppointmentById(id);
				if (appointmentid == null)
				{
					return NotFound(_responseDTO);
				}

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				string userId = userIdClaim.Value;

				//only the user who created the delete it
				if (appointmentid.PatientId != userId)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "You are not authorized to delete appointment";
					return Unauthorized(_responseDTO);
				}

				//delete the post
				await _appointmentService.DeleteAppointment(appointmentid);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Appointment Deleted Successfully";
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
		[HttpGet("doctor/{doctorid}")]
		[Authorize]

		public async Task<ActionResult<ResponseDTO>> GetappointmentByDoctorId(Guid doctorid)
		{
			try
			{

				_responseDTO.Result = await _appointmentService.GetAppointmentByDoctorId(doctorid);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Doctor Appointments fetched";
				return Ok(_responseDTO);


			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.Message;
				return BadRequest(_responseDTO);
			}
		}

	}

}		
