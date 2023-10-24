using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordsManagement.Data;
using RecordsManagement.Models;
using RecordsManagement.Models.DTO;
using RecordsManagement.Services;
using System.Security.Claims;

namespace RecordsManagement.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecordsController : ControllerBase
	{
		private readonly IRecordService _service;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responseDTO;
        public RecordsController(IRecordService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();   
        }
		[HttpGet]
		public async Task<ActionResult<ResponseDTO>> GetAsync()
		{
			try
			{
				_responseDTO.Result = await _service.GetAllRecords();
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Records Succesfully Fetched";
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
		public async Task<ActionResult<ResponseDTO>> AddAsync(RecordDTO addrecord)
		{
			try
			{
				var newrecord= _mapper.Map<Record>(addrecord);

				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				newrecord.DoctorId = userIdClaim.Value;

				_responseDTO.Result = await _service.CreateRecord(newrecord);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Record Created Successfully";

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
		public async Task<ActionResult<ResponseDTO>> UpdatePrescriptionAsync(RecordDTO recordDTO, Guid id)
		{

			try
			{
				//check if the prescription exists
				var record = await _service.GetRecord(id);
				if (record == null)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "Record Not Found";
					return NotFound(_responseDTO);
				}

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				string userId = userIdClaim.Value;

				//only the user who created the record can update it
				if (record.DoctorId != userId)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "You can't edit record";
					return Unauthorized(_responseDTO);
				}

				var post = _mapper.Map(recordDTO, record);

				_responseDTO.Result = await _service.UpdateRecord(record);
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
		public async Task<ActionResult<ResponseDTO>> DeleteAsync(RecordDTO recordDTO, Guid id)
		{

			try
			{
				//check if the record exists
				var record = await _service.GetRecord(id);
				if (record == null)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "Record Not Found";
					return NotFound(_responseDTO);
				}

				//get the user id from the token
				var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
				string userId = userIdClaim.Value;

				//only the user who created can delete it
				if (record.DoctorId != userId)
				{
					_responseDTO.IsSuccess = false;
					_responseDTO.Message = "You cant Delete this record";
					return Unauthorized(_responseDTO);
				}

				var post = _mapper.Map(recordDTO, record);

				_responseDTO.Result = await _service.DeleteRecord(record);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Record Deleted Successfully";
				return Ok(_responseDTO);


			}
			catch (Exception e)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = e.InnerException.Message;
				return BadRequest(_responseDTO);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ResponseDTO>> GetAsyncbyID(Guid id)
		{
			try
			{
				_responseDTO.Result = await _service.GetRecord(id);
				_responseDTO.IsSuccess = true;
				_responseDTO.Message = "Record Succesfully fetched";
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
