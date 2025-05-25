
using api.Dtos.LK_Dtos.LKACSoft_TaskStatusDTO;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/taskstatuses")]
    [ApiController]
    [Authorize]
    public class LKACSoft_TaskStatusController : ControllerBase
    {
        private readonly ILKACSoft_TaskStatusRepository _taskstatusRepo;

        public LKACSoft_TaskStatusController(ILKACSoft_TaskStatusRepository taskstatusRepo)
        {
            _taskstatusRepo = taskstatusRepo;
        }

        // Get all taskstatuses
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllTaskStatuses()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskstatusModels = await _taskstatusRepo.GetAllAsync();
            var taskstatusDtos = taskstatusModels.Select(ts => ts.ToLKACSoft_TaskStatusDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(taskstatusDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(taskstatusDtos);
        }

        // Get taskstatus by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetTaskStatusById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskstatus = await _taskstatusRepo.GetByIdAsync(id);
            if (taskstatus == null)
                return NotFound(new { message = "TaskStatus not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(taskstatus.ToLKACSoft_TaskStatusDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(taskstatus.ToLKACSoft_TaskStatusDto());
        }

    }
}
