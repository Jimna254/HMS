using AuthService.Models.DTO_s;
using AuthService.Services.IServices;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService userService;
        private readonly ResponseDTO responseDTO;
        //private readonly IMessageBus _messageBus;
        //private readonly IConfiguration _configuration;
        public AuthController(IUserService _userService)
        {
            userService = _userService;
            responseDTO = new ResponseDTO();

            
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO>> AddUSer(RegisterDTO registerRequestDto)
        {
            var errorMessage = await userService.RegisterUser(registerRequestDto);
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                //error
                responseDTO.IsSuccess = false;
               responseDTO.Message = errorMessage;

                return BadRequest(responseDTO);
            }
            //send a message to our ServiceBus --Queue
          //  var queueName = _configuration.GetSection("QueuesandTopics:RegisterUser").Get<string>();

            var message = new UserMessageDTO()
            {
                Email = registerRequestDto.Email,
                Name = registerRequestDto.Name,
            };
            responseDTO.Result = "User Registered Successfully";
          //  await _messageBus.PublishMessage(message, queueName);
            
            return Ok(responseDTO);
        }

        [HttpPost("login")]
       
        public async Task<ActionResult<ResponseDTO>> LoginUserAsync(LoginDTO loginRequestDto)
        {
            var response = await userService.LoginUser(loginRequestDto);
            if (response == null)
            {
                //error
                responseDTO.IsSuccess = false;
                responseDTO.Message = "Invalid Credential";

                return BadRequest(responseDTO);
            }
            responseDTO.Result = response;
            return Ok(responseDTO);
        }
        [HttpPost("AssignRole")]
        public async Task<ActionResult<ResponseDTO>> AssignRole(RegisterDTO registerRequestDto)
        {
            var response = await userService.AssignUserRole(registerRequestDto.Email, registerRequestDto.Role);
            if (!response)
            {
                //error
                responseDTO.IsSuccess = false;
                responseDTO.Message = "Error Occured";

                return BadRequest(responseDTO);
            }
            responseDTO.Result = response;
            return Ok(responseDTO);
        }

    }
}
