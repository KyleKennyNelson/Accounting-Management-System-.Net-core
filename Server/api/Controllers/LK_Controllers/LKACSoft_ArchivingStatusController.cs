
using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/archivingStatuses")]
    [ApiController]
    [Authorize]
    public class LKACSoft_ArchivingStatusController : ControllerBase
    {
        private readonly ILKACSoft_ArchivingStatusRepository _archivingStatusRepo;

        public LKACSoft_ArchivingStatusController(ILKACSoft_ArchivingStatusRepository archivingStatusRepo)
        {
            _archivingStatusRepo = archivingStatusRepo;
        }

        // Get all users
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllArchivingStatuses()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var archivingStatusModels = await _archivingStatusRepo.GetAllAsync();
            var archivingStatusDtos = archivingStatusModels.Select(acvS => acvS.ToLKACSoft_ArchivingStatusDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(archivingStatusDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(archivingStatusDtos);
        }

        // Get archivingStatus by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetArchivingStatusById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var archivingStatus = await _archivingStatusRepo.GetByIdAsync(id);
            if (archivingStatus == null)
                return NotFound(new { message = "Archiving Status not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(archivingStatus.ToLKACSoft_ArchivingStatusDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(archivingStatus.ToLKACSoft_ArchivingStatusDto());
        }
    }
}
