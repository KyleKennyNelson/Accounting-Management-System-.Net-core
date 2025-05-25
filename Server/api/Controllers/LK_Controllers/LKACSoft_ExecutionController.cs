
using api.Dtos.LK_Dtos.LKACSoft_ExecutionDTO;
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
    [Route("api/executions")]
    [ApiController]
    [Authorize]
    public class LKACSoft_ExecutionController : ControllerBase
    {
        private readonly ILKACSoft_ExecutionRepository _executionRepo;

        public LKACSoft_ExecutionController(ILKACSoft_ExecutionRepository executionRepo)
        {
            _executionRepo = executionRepo;
        }

        // Get all executions
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllExecutions()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var executionModels = await _executionRepo.GetAllAsync();
            var executionDtos = executionModels.Select(e => e.ToLKACSoft_ExecutionDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(executionDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(executionDtos);
        }

        // Get execution by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetExecutionById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var execution = await _executionRepo.GetByIdAsync(id);
            if (execution == null)
                return NotFound(new { message = "Execution not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(execution.ToLKACSoft_ExecutionDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(execution.ToLKACSoft_ExecutionDto());
        }

        // Create a new execution
        [HttpPost]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> CreateExecution([FromBody] CreateLKACSoft_ExecutionDto executionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract user ID from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                //return Unauthorized("User ID not found in token.");
            }

            //// Set the CreatedBy field with the extracted user ID
            //executionDto.CreatedBy = userId;

            var newExecution = executionDto.CreateLKACSoft_ExecutionDto(userId);

            var res = await _executionRepo.AddAsync(newExecution);

            if (res.Item2 != null && res.Item2 != "")
                return BadRequest(new { message = res.Item2 });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(newExecution.ToLKACSoft_ExecutionDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(newExecution.ToLKACSoft_ExecutionDto());
        }
    }
}
