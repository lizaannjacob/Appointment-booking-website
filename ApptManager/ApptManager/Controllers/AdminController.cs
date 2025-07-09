using ApptManager.Models;
using ApptManager.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApptManager.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("add-professional")]
        public async Task<IActionResult> AddProfessional([FromBody] TaxProfessional professional)
        {
            bool result = await _adminService.AddProfessionalAsync(professional);
            if (result)
                return Ok(new { message = "Professional added successfully" });
            else
                return BadRequest(new { message = "Failed to add professional" });
        }

        [HttpPost("generate-slots")]
        public async Task<IActionResult> GenerateSlots([FromQuery] int professionalId, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            bool result = await _adminService.GenerateSlotsAsync(professionalId, startTime, endTime);
            if (result)
                return Ok(new { message = "Slots generated successfully" });
            else
                return BadRequest(new { message = "Failed to generate slots" });
        }

        [HttpPut("update-slot-status")]
        public async Task<IActionResult> UpdateSlotStatus([FromQuery] int slotId, [FromQuery] string status)
        {
            await _adminService.UpdateSlotStatusAsync(slotId, status);
            return Ok(new { message = "Slot status updated" });
        }

        [HttpDelete("delete-slot")]
        public async Task<IActionResult> DeleteSlot([FromQuery] int slotId)
        {
            await _adminService.DeleteSlotAsync(slotId);
            return Ok(new { message = "Slot deleted" });
        }

        [HttpGet("get-slots")]
        public async Task<IActionResult> GetSlots([FromQuery] int professionalId)
        {
            var slots = await _adminService.GetSlotsByProfessionalIdAsync(professionalId);
            return Ok(slots);
        }

        [HttpGet("get-professionals")]
        public async Task<IActionResult> GetProfessionals()
        {
            var list = await _adminService.GetAllProfessionalsAsync();
            return Ok(list);
        }

        [HttpGet("all-slot-details")]
        public async Task<IActionResult> GetAllSlotDetails()
        {
            var result = await _adminService.GetAllSlotDetailsAsync();
            if (result == null || !result.Any())
                return Ok(new List<object>());
            return Ok(result);
        }





    }
}
