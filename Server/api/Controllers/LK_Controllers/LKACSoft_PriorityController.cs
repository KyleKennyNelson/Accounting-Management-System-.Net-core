
using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading.Tasks;

namespace api.Controllers.LK_Controllers
{
    [Route("api/priorities")]
    [ApiController]
    [Authorize]
    public class LKACSoft_PriorityController : ControllerBase
    {
        private readonly ILKACSoft_PriorityRepository _priorityRepo;

        public LKACSoft_PriorityController(ILKACSoft_PriorityRepository priorityRepo)
        {
            _priorityRepo = priorityRepo;
        }

        // Get all priorities
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllPriorities()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var priorityModels = await _priorityRepo.GetAllAsync();
            var priorityDtos = priorityModels.Select(pri => pri.ToLKACSoft_PriorityDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(priorityDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(priorityDtos);
        }

        // Get priority by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetPriorityById([FromRoute]string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var priority = await _priorityRepo.GetByIdAsync(id);
            if (priority == null)
                return NotFound(new { message = "Priority not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(priority.ToLKACSoft_PriorityDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(priority.ToLKACSoft_PriorityDto());
        }
    }
}
