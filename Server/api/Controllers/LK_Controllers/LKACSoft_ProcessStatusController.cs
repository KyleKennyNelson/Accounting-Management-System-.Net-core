
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/processStatuses")]
    [ApiController]
    [Authorize]
    public class LKACSoft_ProcessStatusController : ControllerBase
    {
        private readonly ILKACSoft_ProcessStatusRepository _processStatusRepo;

        public LKACSoft_ProcessStatusController(ILKACSoft_ProcessStatusRepository processStatusRepo)
        {
            _processStatusRepo = processStatusRepo;
        }

        // Get all processStatuses
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllProcessStatuses()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var processStatusModels = await _processStatusRepo.GetAllAsync();
            var processStatusDtos = processStatusModels.Select(ps => ps.ToLKACSoft_ProcessStatusDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(processStatusDtos, jsonOptions);

            //return Content(jsonResult, "application/json");
            return Ok(processStatusDtos);
        }

        // Get processStatus by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetProcessStatusById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var processStatus = await _processStatusRepo.GetByIdAsync(id);
            if (processStatus == null)
                return NotFound(new { message = "ProcessStatus not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(processStatus.ToLKACSoft_ProcessStatusDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(processStatus.ToLKACSoft_ProcessStatusDto());
        }
    }
}
