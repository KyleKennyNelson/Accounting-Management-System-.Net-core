
using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/detailexecutions")]
    [ApiController]
    [Authorize]
    public class LKACSoft_DetailExecutionController : ControllerBase
    {
        private readonly ILKACSoft_DetailExecutionRepository _detailexecutionRepo;

        public LKACSoft_DetailExecutionController(ILKACSoft_DetailExecutionRepository detailexecutionRepo)
        {
            _detailexecutionRepo = detailexecutionRepo;
        }

        // Get all processes
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllDetailExecutions([FromQuery] QueryObject_DetailCustomer queryByCustomer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detailexecutionModels = await _detailexecutionRepo.GetAllAsync(queryByCustomer);
            var detailexecutionDtos = detailexecutionModels.Select(de => de.ToLKACSoft_DetailExecutionDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(detailexecutionDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(detailexecutionDtos);
        }

        // Get execution by executionID
        [HttpGet("{executionID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDetailExecutionById([FromRoute]string executionID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detailexecution = await _detailexecutionRepo.GetByIdAsync(executionID);
            if (detailexecution == null)
                return NotFound(new { message = "DetailExecution not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(detailexecution.ToLKACSoft_DetailExecutionDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(detailexecution.ToLKACSoft_DetailExecutionDto());
        }

    }
}
