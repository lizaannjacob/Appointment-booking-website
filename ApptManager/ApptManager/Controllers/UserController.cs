using ApptManager.Models;
using ApptManager.Services;
using ApptManager.Dtos;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace ApptManager.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
        {
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto userDto)
        {
            _logger.LogInformation("Registering user: {Email}", userDto.Email);
            var user = _mapper.Map<UserRegistrationInfo>(userDto);
            var success = await _userService.RegisterUserAsync(user);
            if (success)
            {
               Log.Information("User registered successfully: {Email}", userDto.Email);
                return Ok(new { message = "User registered successfully" });
            }
            _logger.LogWarning("User registration failed: {Email}", userDto.Email);
            return BadRequest(new { message = "Registration failed" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var role = await _userService.LoginAsync(request.Email, request.Password);
            if (role == null)
            {
                Log.Error("Invalid email or password");
                return Unauthorized("Invalid email or password");
            }

            return Ok(new
            {
                isAdmin = role == "admin",
                redirectUrl = role == "admin" ? "/admin-dashboard" : "/user-dashboard"
            });
        }

        [HttpGet("available-slots")]
        public async Task<IActionResult> GetAvailableSlots()
        {
            var professionalsWithSlots = await _userService.GetProfessionalsWithSlotsAsync();
            var dtoList = _mapper.Map<List<TaxProfessionalWithSlotsDto>>(professionalsWithSlots);
            return Ok(dtoList);
        }

        [HttpPut("book-slot")]
        public async Task<IActionResult> BookSlot([FromQuery] int slotId, [FromQuery] string email)
        {
            var result = await _userService.UpdateSlotStatusAsync(slotId, "booked", email);
            return result ? Ok(new { message = "Slot booked successfully" })
                          : BadRequest(new { message = "Failed to book slot" });
        }

        [HttpGet("my-appointments")]
        public async Task<IActionResult> GetBookedSlotsByEmail([FromQuery] string email)
        {
            var slots = await _userService.GetBookedSlotsByEmailAsync(email);
            var slotDtos = _mapper.Map<List<SlotWithProfessionalDto>>(slots);
            return Ok(slotDtos);
        }

        [HttpGet("available-slots-by-professional")]
        public async Task<IActionResult> GetAvailableSlotsByProfessional(int professionalId)
        {
            var slots = await _userService.GetAvailableSlotsByProfessionalAsync(professionalId);
            return Ok(slots);
        }

    }
}
