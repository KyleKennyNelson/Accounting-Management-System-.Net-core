
using api.Dtos.LK_Dtos.LKACSoft_DetailUsersDTO;
using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Models;
using LKACSoftModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace api.Controllers.LK_Controllers
{
    [Route("api/detailusers")]
    [ApiController]
    [Authorize]
    public class LKACSoft_DetailUserController : ControllerBase
    {
        private readonly ILKACSoft_DetailUserRepository _detailuserRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public LKACSoft_DetailUserController(ILKACSoft_DetailUserRepository detailuserRepo, 
                                        UserManager<ApplicationUser> userManager)
        {
            _detailuserRepo = detailuserRepo;
            _userManager = userManager;
        }

        // Get all tasks
        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllDetailUsers()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detailuserModels = await _detailuserRepo.GetAllAsync();

            if (detailuserModels == null || !detailuserModels.Any())
                return Ok(new List<LKACSoft_DetailUserDto>()); // Return an empty list if no users are found

            var detailuserDtos = new List<LKACSoft_DetailUserDto>();

            foreach (var detailuserModel in detailuserModels)
            {
                var user = await _userManager.FindByIdAsync(detailuserModel.UserID);
                if (user != null)
                {
                    // Retrieve roles for the user
                    var roles = await _userManager.GetRolesAsync(user);

                    // Map to DTO and pass roles
                    var detailUserDto = detailuserModel.ToLKACSoft_DetailUserDto(roles.Select(r => new ApplicationRole { Name = r }).ToList());
                    detailuserDtos.Add(detailUserDto);
                }
            }

            return Ok(detailuserDtos); // Always return a result
        }

        // Get user by ID
        [HttpGet("{userID}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDetailUserById(string userID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detailuser = await _detailuserRepo.GetByIdAsync(userID);
            if (detailuser == null)
                return NotFound(new { message = "DetailUser not found" });

            var user = await _userManager.FindByIdAsync(userID);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // Retrieve roles for the user
            var roles = await _userManager.GetRolesAsync(user);

            // Map to DTO and pass roles
            var detailUserDto = detailuser.ToLKACSoft_DetailUserDto(roles.Select(r => new ApplicationRole { Name = r }).ToList());

            return Ok(detailUserDto);
        }

        // Get user by ID
        [HttpGet("GetUserKPI/{userID}")]
        //[Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDetailUserKPIById(string userID)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var detailuserKPI = await _detailuserRepo.GetUserKPIAsync(userID);
            if (detailuserKPI == null)
                return NotFound(new { message = "DetailUserKPI not found" });

            // Map to DTO and pass roles
            var detailUserKPIDto = detailuserKPI.ToLKACSoft_DetailUserKPIDto();

            return Ok(detailUserKPIDto);
        }

        // GetUserInfor
        [HttpGet("GetUserInfor")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetDetailUserInfor()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Extract user ID from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                //return Unauthorized("User ID not found in token.");
            }

            var detailuser = await _detailuserRepo.GetByIdAsync(userId);
            if (detailuser == null)
                return NotFound(new { message = "DetailUser not found" });

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // Retrieve roles for the user
            var roles = await _userManager.GetRolesAsync(user);

            // Map to DTO and pass roles
            var detailUserDto = detailuser.ToLKACSoft_DetailUserDto(roles.Select(r => new ApplicationRole { Name = r }).ToList());

            return Ok(detailUserDto);
        }

    }
}
