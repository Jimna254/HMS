using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prescription_Service.Migrations;
using Prescription_Service.Models;
using Prescription_Service.Models.DTO;
using Prescription_Service.Service;
using System.Security.Claims;

namespace Prescription_Service.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PrescriptionController : ControllerBase
	{
        private readonly IMapper _mapper;
        private readonly IPrescriptionService _service;
        private readonly ResponseDTO _responseDTO;

        public PrescriptionController(IMapper mapper, IPrescriptionService prescriptionService)
        {
            _responseDTO = new ResponseDTO();
            _mapper = mapper;
            _service = prescriptionService;
        }

        [HttpGet]

		public async Task<ActionResult<ResponseDTO>> GetAsync()
		{
			try
			{
				_responseDTO.Result = await _service.GetAllPrescriptions();
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Prescriptions Succesfully Placed";
				return Ok(_responseDTO);


			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.Message;
				return BadRequest(_responseDTO);
			}
		}
		[HttpPost]
		[Authorize]
		public async Task<ActionResult<ResponseDTO>> AddAsync(PrescriptionDTO addprescription)
		{
			try
			{
				var newprescription = _mapper.Map<Prescription>(addprescription);

				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				newprescription.DoctorID = userIdClaim.Value;

				_responseDTO.Result = await _service.CreatePrescription(newprescription);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Prescription Made Successfully";

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
		public async Task<ActionResult<ResponseDTO>> UpdatePrescriptionAsync(PrescriptionDTO prescriptionDTO, Guid id)
		{

			try
			{
				//check if the prescription exists
				var prescription = await _service.GetPrescription(id);
				if (prescription == null)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "Prescription Not Found";
					return NotFound(_responseDTO);
				}

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				string userId = userIdClaim.Value;

				//only the user who created the Feedback can update it
				if (prescription.DoctorID != userId)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "You cant edit this prescription";
					return Unauthorized(_responseDTO);
				}

				var post = _mapper.Map(prescriptionDTO, prescription);

				_responseDTO.Result = await _service.UpdatePrescription(prescription);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Prescription Updated Successfully";
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
		public async Task<ActionResult<ResponseDTO>> DeletePrescriptionAsync(PrescriptionDTO prescriptionDTO, Guid id)
		{

			try
			{
				//check if the prescription exists
				var prescription = await _service.GetPrescription(id);
				if (prescription == null)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "Prescription Not Found";
					return NotFound(_responseDTO);
				}

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				string userId = userIdClaim.Value;

				//only the user who created can delete it
				if (prescription.DoctorID != userId)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "You cant Delete this prescription";
					return Unauthorized(_responseDTO);
				}

				var post = _mapper.Map(prescriptionDTO, prescription);

				_responseDTO.Result = await _service.DeletePrescription(prescription);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Prescription Deleted Successfully";
				return Ok(_responseDTO);


			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.InnerException.Message;
				return BadRequest(_responseDTO);
			}
		}
		//[HttpPost("{appointmentid}")]
		//public async Task<ActionResult<ResponseDTO>> GetAsyncbyID(Guid id)
		//{
		//	try
		//	{
		//		_responseDTO.Result = await _service.GetPrescription(id);
		//		_responseDTO.IsSuccess = true;
		//		_responseDTO.Message = "Prescription Succesfully fetched";
		//		return Ok(_responseDTO);

		//	}
		//	catch (Exception e)
		//	{
		//		_responseDTO.IsSuccess = false;
		//		_responseDTO.Message = e.Message;
		//		return BadRequest(_responseDTO);
		//	}
		//}
		[HttpGet("{id}")]
		public async Task<ActionResult<ResponseDTO>> GetAsyncbyAppointmentID(Guid appointmentid)
		{
			try
			{
				_responseDTO.Result = await _service.GetPrescriptionbyAppointment(appointmentid);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Prescription Succesfully fetched";
				return Ok(_responseDTO);

			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.Message;
				return BadRequest(_responseDTO);
			}
		}
		[HttpPost("StripePayment")]
		public async Task<ActionResult<ResponseDTO>> StripePayment(StripeRequestDTO stripeRequestDto)
		{
			try
			{
				var response = await _service.StripePayment(stripeRequestDto);
				_responseDTO.Result = response;
			}
			catch (Exception ex)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = ex.InnerException.Message;
				return BadRequest(_responseDTO);
			}
			return Ok(_responseDTO);
		}


	}
}
