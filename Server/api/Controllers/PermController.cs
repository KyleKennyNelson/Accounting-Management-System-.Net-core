using api.Dtos.MenuResource;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace api.Controllers
{
    [Route("api/Permission")]
    [ApiController]
    [Authorize]
    public class PermController : ControllerBase
    {
        private readonly IMenuResource_Repository _tableMenuResouceRepo;
        public PermController(IMenuResource_Repository tableMenuResouceRepo)
        {
            this._tableMenuResouceRepo = tableMenuResouceRepo;
        }

        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllPerm()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var perms = await _tableMenuResouceRepo.getAllPermissions();

            var permModels = perms.Select(p => p.ToPermModel());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(permModels, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(permModels);
        }
    }
}
