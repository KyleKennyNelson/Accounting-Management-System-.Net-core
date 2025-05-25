
using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/accountingStatuses")]
    [ApiController]
    [Authorize]
    public class LKACSoft_AccountingStatusController : ControllerBase
    {
        private readonly ILKACSoft_AccountingStatusRepository _accountingStatusRepo;

        public LKACSoft_AccountingStatusController(ILKACSoft_AccountingStatusRepository accountingStatusRepo)
        {
            _accountingStatusRepo = accountingStatusRepo;
        }

        // Get all users
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllAccountingStatuses()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountingStatusModels = await _accountingStatusRepo.GetAllAsync();
            var accountingStatusDtos = accountingStatusModels.Select(acvS => acvS.ToLKACSoft_AccountingStatusDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(accountingStatusDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(accountingStatusDtos);
        }

        // Get accountingStatus by ID
        [HttpGet("{id}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAccountingStatusById(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountingStatus = await _accountingStatusRepo.GetByIdAsync(id);
            if (accountingStatus == null)
                return NotFound(new { message = "Accounting Status not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(accountingStatus.ToLKACSoft_AccountingStatusDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(accountingStatus.ToLKACSoft_AccountingStatusDto());
        }
    }
}
