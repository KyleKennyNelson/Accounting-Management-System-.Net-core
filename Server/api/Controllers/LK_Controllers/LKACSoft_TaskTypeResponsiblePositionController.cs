using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/tasktypeResponsiblePositions")]
    [ApiController]
    [Authorize]
    public class LKACSoft_TaskTypeResponsiblePositionController : ControllerBase
    {
        private readonly ILKACSoft_TaskTypeResponsiblePositionRepository _tasktypeResponsiblePositionRepo;

        public LKACSoft_TaskTypeResponsiblePositionController(ILKACSoft_TaskTypeResponsiblePositionRepository tasktypeResponsiblePositionRepo)
        {
            _tasktypeResponsiblePositionRepo = tasktypeResponsiblePositionRepo;
        }

        // Get all tasktypeResponsiblePosition
        [HttpGet]
        //[Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllTaskTypeResponsiblePositions()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tasktypeResponsiblePositionModels = await _tasktypeResponsiblePositionRepo.GetAllAsync();
            var tasktypeResponsiblePositionDtos = tasktypeResponsiblePositionModels.Select(tt => tt.ToLKACSoft_TaskTypeResponsiblePositionDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(tasktypeResponsiblePositionDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(tasktypeResponsiblePositionDtos);
        }

        // Get tasktypeResponsiblePosition by ID
        [HttpGet("{id}")]
        //[Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetTaskTypeResponsiblePositionById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tasktypeResponsiblePosition = await _tasktypeResponsiblePositionRepo.GetByIdAsync(id);
            if (tasktypeResponsiblePosition == null)
                return NotFound(new { message = "TaskTypeResponsiblePosition not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(tasktypeResponsiblePosition.ToLKACSoft_TaskTypeResponsiblePositionDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(tasktypeResponsiblePosition.ToLKACSoft_TaskTypeResponsiblePositionDto());
        }

    }
}
