using ApptManager.Models;
using ApptManager.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationInfo user)
    {
        var success = await _userService.RegisterUserAsync(user);
        if (success)
            return Ok(new { message = "User registered successfully" });
        else
            return BadRequest(new { message = "Registration failed" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var role = await _userService.LoginAsync(request.Email, request.Password);
        if (role == null)
            return Unauthorized("Invalid email or password");

        if (role == "admin")
            return Ok(new { isAdmin = true, redirectUrl = "/admin-dashboard" });
        else if (role == "user")
            return Ok(new { isAdmin = false, redirectUrl = "/user-dashboard" });
        else
            return BadRequest("Unknown role");
    }

    [HttpGet("available-slots")]
    public async Task<IActionResult> GetAvailableSlots()
    {
        var professionalsWithSlots = await _userService.GetProfessionalsWithSlotsAsync();
        return Ok(professionalsWithSlots);
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
        return Ok(slots);
    }


    [HttpGet("available-slots-by-professional")]
    public async Task<IActionResult> GetAvailableSlotsByProfessional(int professionalId)
    {
        var slots = await _userService.GetAvailableSlotsByProfessionalAsync(professionalId);
        return Ok(slots);
    }


}
