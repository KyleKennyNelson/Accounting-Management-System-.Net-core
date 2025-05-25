using api.Dtos.MenuResource;
using api.Identity;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace api.Controllers
{
    [Route("api/User")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUser_Roles_repository _user_Roles_Repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public UserController(IUser_Roles_repository user_Roles_Repository,
                            UserManager<ApplicationUser> userManager, 
                            RoleManager<ApplicationRole> roleManager)
        {
            _user_Roles_Repository = user_Roles_Repository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllUsersContainRoles()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (users, roles) = await _user_Roles_Repository.getUserContainRoles();

            // Map roles to roles model
            var roleModels = roles.Select(r => r.ToRolesModel()).ToList();

            // Group roles by UserId
            var rolesByUserId = roleModels
                .GroupBy(r => r.UserId)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Map users to user models and assign roles
            var userModels = users.Select(u => u.ToUserModel(
                rolesByUserId.ContainsKey(u.Id) ? rolesByUserId[u.Id] : new List<Roles>()
            )).ToList();

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(userModels, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(userModels);
        }

        // Add Role to User
        [HttpPost("{userId}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> AddRoleToUser(string userId, [FromBody] RoleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var role = await _roleManager.FindByIdAsync(request.RoleId);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            // Check if the user is already in the role
            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
            if (isInRole)
            {
                return BadRequest($"User is already in role '{role.Name}'.");
            }

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return Ok($"Role '{role.Name}' added to user '{user.UserName}'.");
            }

            return BadRequest(result.Errors);
        }

        // Remove Role from User
        [HttpDelete("{userId}/{roleId}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return NotFound("Role not found.");
            }

            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
            if (!isInRole)
            {
                return BadRequest($"User is not in role '{role.Name}'.");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return Ok($"Role '{role.Name}' removed from user '{user.UserName}'.");
            }

            return BadRequest(result.Errors);
        }
    }

    // Request Model
    public class RoleRequest
    {
        public string RoleId { get; set; }
    }
}
