using AutoMapper;
using DoctorApplication.Model;
using DoctorApplication.Model.DTO_s;
using DoctorApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DoctorApplication.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ApplicationController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ResponseDTO _responseDTO;
		private  readonly IApplicationService _applicationService;

        public ApplicationController(IMapper mapper , IApplicationService applicationService)
        {
            _applicationService = applicationService;
			_mapper = mapper;
			_responseDTO = new ResponseDTO();	
        }

		[HttpGet]
		public async Task<ActionResult<ResponseDTO>> GetAllApplications()
		{

			var applications = await _applicationService.GetAllApplications();
			if (applications == null)
			{
				_responseDTO.IsSuccess = false;
				_responseDTO.Message = "Error Occured";
				return BadRequest(_responseDTO);
			}

			_responseDTO.Result = applications;


			return Ok(_responseDTO);
		}

		[HttpPost]
		
		public async Task<ActionResult<ResponseDTO>> AddApplicationAsync(DocApplicationDTO docApplicationDTO)
		{
			try
			{
				var newApplication = _mapper.Map<DocApplication>(docApplicationDTO);
				var response = await _applicationService.AddApplication(newApplication);
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
		public async Task<ActionResult<ResponseDTO>> GetApplicationAsync(Guid Id)
		{
			try
			{
				var application = await _applicationService.GetApplication(Id);
				if (application == null)
				{
					_responseDTO.IsSuccess = true;
					_responseDTO.Message = "Error Occured";
					return BadRequest(_responseDTO);
				}

				_responseDTO.Result = application;

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
