
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
    [Route("api/departments")]
    [ApiController]
    [Authorize]
    public class LKACSoft_DepartmentController : ControllerBase
    {
        private readonly ILKACSoft_DepartmentRepository _departmentRepo;

        public LKACSoft_DepartmentController(ILKACSoft_DepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }

        // Get all departments
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllDepartments()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentModels = await _departmentRepo.GetAllAsync();
            var departmentDtos = departmentModels.Select(d => d.ToLKACSoft_DepartmentDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(pepartmentDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(departmentDtos);
        }

        // Get department by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDepartmentById([FromRoute]string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var department = await _departmentRepo.GetByIdAsync(id);
            if (department == null)
                return NotFound(new { message = "Department not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(pepartment.ToLKACSoft_PepartmentDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(department.ToLKACSoft_DepartmentDto());
        }
    }
}
