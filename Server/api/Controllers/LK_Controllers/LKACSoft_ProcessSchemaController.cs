
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
    [Route("api/processSchemas")]
    [ApiController]
    [Authorize]
    public class LKACSoft_ProcessSchemaController : ControllerBase
    {
        private readonly ILKACSoft_ProcessSchemaRepository _processSchemaRepo;

        public LKACSoft_ProcessSchemaController(ILKACSoft_ProcessSchemaRepository processSchemaRepo)
        {
            _processSchemaRepo = processSchemaRepo;
        }

        // Get all processSchemas
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllProcessSchemas()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var processSchemaModels = await _processSchemaRepo.GetAllAsync();
            var processSchemaDtos = processSchemaModels.Select(ps => ps.ToLKACSoft_ProcessSchemaDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(processSchemaDtos, jsonOptions);

            //return Content(jsonResult, "application/json");
            return Ok(processSchemaDtos);
        }

        // Get processSchema by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetProcessSchemaById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var processSchema = await _processSchemaRepo.GetByIdAsync(id);
            if (processSchema == null)
                return NotFound(new { message = "ProcessSchema not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(processSchema.ToLKACSoft_ProcessSchemaDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(processSchema.ToLKACSoft_ProcessSchemaDto());
        }
    }
}
