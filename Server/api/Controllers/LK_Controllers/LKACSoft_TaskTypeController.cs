using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/tasktypes")]
    [ApiController]
    [Authorize]
    public class LKACSoft_TaskTypeController : ControllerBase
    {
        private readonly ILKACSoft_TaskTypeRepository _tasktypeRepo;

        public LKACSoft_TaskTypeController(ILKACSoft_TaskTypeRepository tasktypeRepo)
        {
            _tasktypeRepo = tasktypeRepo;
        }

        // Get all tasktypes
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllTaskTypes()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tasktypeModels = await _tasktypeRepo.GetAllAsync();
            var tasktypeDtos = tasktypeModels.Select(tt => tt.ToLKACSoft_TaskTypeDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(tasktypeDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(tasktypeDtos);
        }

        // Get tasktype by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetTaskTypeById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tasktype = await _tasktypeRepo.GetByIdAsync(id);
            if (tasktype == null)
                return NotFound(new { message = "TaskType not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(tasktype.ToLKACSoft_TaskTypeDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(tasktype.ToLKACSoft_TaskTypeDto());
        }

    }
}
