using AutoMapper;
using Hospital_Service.Model;
using Hospital_Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hospital_Service.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HospitalController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ResponseDTO _responseDTO;
		private readonly IHospitalService _hospitalservice;

        public HospitalController(IMapper mapper, IHospitalService hospitalService)
        {
			_responseDTO = new ResponseDTO();
			_mapper = mapper;
			_hospitalservice = hospitalService;
        }
		[HttpGet]
		public async Task<ActionResult<ResponseDTO>> GetAllHospitals()
		{

			var products = await _hospitalservice.GetAllHospitals();
			if (products == null)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = "Error Occured";
				return BadRequest(_responseDTO);
			}

			_responseDTO.Result = products;


			return Ok(_responseDTO);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<ResponseDTO>> AddHospital(HospitalDTO hospitalDTO)
		{
			try
			{
				var newHospital = _mapper.Map<Hospital>(hospitalDTO);
				var response = await _hospitalservice.AddHospital(newHospital);
				if (string.IsNullOrWhiteSpace(response))
				{
					_responseDTO.IsSuccess = true;
					_responseDTO.Message = "Error Occured";
					return BadRequest(_responseDTO);
				}

				_responseDTO.Result = response;

			}
			catch (Exception ex)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = ex.Message;
				return BadRequest(_responseDTO);
			}

			return Ok(_responseDTO);
		}

		[HttpGet("GetById/{Id}")]
		public async Task<ActionResult<ResponseDTO>> GetHospital(Guid Id)
		{
			try
			{
				var hospital = await _hospitalservice.GetHospitalbyID(Id);
				if (hospital == null)
				{
					_responseDTO.IsSuccess = true;
					_responseDTO.Message = "Error Occured";
					return BadRequest(_responseDTO);
				}

				_responseDTO.Result = hospital;

			}
			catch (Exception ex)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = ex.Message;
				return BadRequest(_responseDTO);
			}

			return Ok(_responseDTO);
		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<ResponseDTO>> UpdateCoupon(Guid id, HospitalDTO hospitalDTO)
		{
			try
			{
				var hospital = await _hospitalservice.GetHospitalbyID(id);
				if (hospital == null)
				{
					_responseDTO.IsSuccess = true;
					_responseDTO.Message = "Error Occured";
					return BadRequest(_responseDTO);
				}
				//update
				var updated = _mapper.Map(hospitalDTO, hospital);
				var response = await _hospitalservice.UpdateHospital(updated);
				_responseDTO.Result = response;

			}
			catch (Exception ex)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = ex.Message;
				return BadRequest(_responseDTO);

			}
			return Ok(_responseDTO);
		}

		[HttpDelete]
		[Authorize(Roles = "Admin")]
		public async Task<ActionResult<ResponseDTO>> DeleteProduct(Guid id)
		{
			try
			{
				var hospital = await _hospitalservice.GetHospitalbyID(id);
				if (hospital == null)
				{
					_responseDTO.IsSuccess = true;
					_responseDTO.Message = "Error Occured";
					return BadRequest(_responseDTO);
				}
				//delete
				var response = await _hospitalservice.DeletHospital(hospital);
				_responseDTO.Result = response;

			}
			catch (Exception ex)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = ex.Message;
				return BadRequest(_responseDTO);

			}
			return Ok(_responseDTO);
		}
	}
}
