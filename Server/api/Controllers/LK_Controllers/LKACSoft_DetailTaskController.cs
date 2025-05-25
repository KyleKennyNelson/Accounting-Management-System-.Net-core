
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/detailtasks")]
    [ApiController]
    [Authorize]
    public class LKACSoft_DetailTaskController : ControllerBase
    {
        private readonly ILKACSoft_DetailTaskRepository _detailtaskRepo;

        public LKACSoft_DetailTaskController(ILKACSoft_DetailTaskRepository detailtaskRepo)
        {
            _detailtaskRepo = detailtaskRepo;
        }

        // Get all detailtasks
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllDetailTasks()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract user ID from claims

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var isManager = false;

            if (User.IsInRole("TRUONGPHONGKT"))
                isManager = true;

            var detailtaskModels = await _detailtaskRepo.GetAllAsync(userId, isManager);
            var detailtaskDtos = detailtaskModels.Select(dt => dt.ToLKACSoft_DetailTaskDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(detailtaskDtos, jsonOptions);

            //return Content(jsonResult, "application/json");
            return Ok(detailtaskDtos);
        }

        // Get detailtask by ID
        [HttpGet("{taskid}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDetailTaskById(string taskid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract user ID from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            var isManager = false;

            if (User.IsInRole("TRUONGPHONGKT"))
                isManager = true;

            var detailtask = await _detailtaskRepo.GetByIdAsync(userId, taskid, isManager);
            if (detailtask == null)
                return NotFound(new { message = "DetailTask not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(detailtask, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(detailtask);
        }

    }
}
