
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
    [Route("api/processSchemaStatuses")]
    [ApiController]
    [Authorize]
    public class LKACSoft_DetailProcessSchemaStatusController : ControllerBase
    {
        private readonly ILKACSoft_DetailProcessSchemaStatusRepository _detailProcessSchemaStatusRepo;

        public LKACSoft_DetailProcessSchemaStatusController(ILKACSoft_DetailProcessSchemaStatusRepository detailProcessSchemaStatusRepo)
        {
            _detailProcessSchemaStatusRepo = detailProcessSchemaStatusRepo;
        }

        // Get all processSchemaStatues
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllProcessSchemaStatuses()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var processSchemaStatusModels = await _detailProcessSchemaStatusRepo.GetAllAsync();
            var processSchemaStatusDtos = processSchemaStatusModels.Select(pss => pss.ToLKACSoft_DetailProcessSchemaStatusDto());

            return Ok(processSchemaStatusDtos);
        }

        // Get processSchemaStatus by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetProcessSchemaStatusById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var processSchemaStatus = await _detailProcessSchemaStatusRepo.GetByIdAsync(id);
            if (processSchemaStatus == null)
                return NotFound(new { message = "ProcessSchemaStatus not found" });

            return Ok(processSchemaStatus.ToLKACSoft_DetailProcessSchemaStatusDto());
        }
    }
}
