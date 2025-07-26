using ApptManager.Models;
using ApptManager.Services;
using ApptManager.Dtos;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApptManager.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpPost("add-professional")]
        public async Task<IActionResult> AddProfessional([FromBody] TaxProfessionalDto dto)
        {
            var professional = _mapper.Map<TaxProfessional>(dto);
            bool result = await _adminService.AddProfessionalAsync(professional);
            return result
                ? Ok(new { message = "Professional added successfully" })
                : BadRequest(new { message = "Failed to add professional" });
        }

        [HttpPost("generate-slots")]
        public async Task<IActionResult> GenerateSlots([FromQuery] int professionalId, [FromQuery] DateTime startTime, [FromQuery] DateTime endTime)
        {
            bool result = await _adminService.GenerateSlotsAsync(professionalId, startTime, endTime);
            return result
                ? Ok(new { message = "Slots generated successfully" })
                : BadRequest(new { message = "Failed to generate slots" });
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
            var slotDtos = _mapper.Map<List<AvailabilitySlotDto>>(slots);
            return Ok(slotDtos);
        }

        [HttpGet("get-professionals")]
        public async Task<IActionResult> GetProfessionals()
        {
            var list = await _adminService.GetAllProfessionalsAsync();
            var dtoList = _mapper.Map<List<TaxProfessionalDto>>(list);
            return Ok(dtoList);
        }

        [HttpGet("all-slot-details")]
        public async Task<IActionResult> GetAllSlotDetails()
        {
            var result = await _adminService.GetAllSlotDetailsAsync();
            if (result == null || !result.Any())
                return Ok(new List<object>());

            var dtoList = _mapper.Map<List<TaxProfessionalWithSlotsDto>>(result);
            return Ok(dtoList);
        }
    }
}
