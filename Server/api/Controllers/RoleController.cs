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
    [Route("api/Role")]
    [ApiController]
    [Authorize]
    public class RoleController : ControllerBase
    {
        private readonly IMenuResource_Repository _tableMenuResouceRepo;
        public RoleController(IMenuResource_Repository tableMenuResouceRepo)
        {
            this._tableMenuResouceRepo = tableMenuResouceRepo;
        }

        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllRole()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var roles = await _tableMenuResouceRepo.getAllRoles();

            var roleModels = roles.Select(r => r.ToRoleModel());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(roleModels, jsonOptions);

            //return Content(jsonResult, "application/json");
            return Ok(roleModels);
        }
    }
}
