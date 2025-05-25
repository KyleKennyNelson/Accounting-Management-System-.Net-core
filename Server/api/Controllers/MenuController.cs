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
    [Route("api/Menu")]
    [ApiController]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuResource_Repository _tableMenuResouceRepo;
        public MenuController(IMenuResource_Repository tableMenuResouceRepo)
        {
            this._tableMenuResouceRepo = tableMenuResouceRepo;
        }

        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllMenu()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var menus = await _tableMenuResouceRepo.getAllMenus();

            var menuModels = menus.Select(m => m.ToMenuModel());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(menuModels, jsonOptions);

            //return Content(jsonResult, "application/json");
            return Ok(menuModels);
        }
    }
}
