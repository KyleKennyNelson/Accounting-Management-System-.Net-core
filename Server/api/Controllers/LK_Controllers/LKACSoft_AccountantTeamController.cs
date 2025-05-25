
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
    [Route("api/accountantTeams")]
    [ApiController]
    [Authorize]
    public class LKACSoft_AccountantTeamController : ControllerBase
    {
        private readonly ILKACSoft_AccountantTeamRepository _accountantTeamRepo;

        public LKACSoft_AccountantTeamController(ILKACSoft_AccountantTeamRepository accountantTeamRepo)
        {
            _accountantTeamRepo = accountantTeamRepo;
        }

        // Get all users
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllAccountantTeams()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountantTeamModels = await _accountantTeamRepo.GetAllAsync();
            var accountantTeamDtos = accountantTeamModels.Select(at => at.ToLKACSoft_AccountantTeamDto());

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(accountantTeamDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(accountantTeamDtos);
        }

        // Get accountantTeam by ID
        [HttpGet("{teamid}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAccountantTeamById(string teamid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var accountantTeam = await _accountantTeamRepo.GetByIdAsync(teamid);
            if (accountantTeam == null)
                return NotFound(new { message = "Accountant Team not found" });

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(accountantTeam.ToLKACSoft_AccountantTeamDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(accountantTeam.ToLKACSoft_AccountantTeamDto());
        }

        // Update an existing accountantTeam
        [HttpPut("{teamID}/{leaderID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UpdateLeaderOfTeam([FromRoute] string teamID, [FromRoute] string leaderID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var res = await _accountantTeamRepo.UpdateAsync(teamID, leaderID);

            if (res != null && res != "")
                return BadRequest(new { message = res });

            var updatedAccountantTeam = await _accountantTeamRepo.GetByIdAsync(teamID);

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(updatedAccountantTeam.ToLKACSoft_AccountantTeamDto(), jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(updatedAccountantTeam.ToLKACSoft_AccountantTeamDto());
        }
    }
}
