using api.Dtos.MenuResource;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace api.Controllers
{
    [Route("api/RoleMenuPerm")]
    [ApiController]
    [Authorize]
    public class RoleMenuPermController : ControllerBase
    {
        private readonly IMenuResource_Repository _tableMenuResouceRepo;
        public RoleMenuPermController(IMenuResource_Repository tableMenuResouceRepo)
        {
            this._tableMenuResouceRepo = tableMenuResouceRepo;
        }

        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllRoleMenuPerm()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ////Roles
            //var roles = await _tableMenuResouceRepo.getAllRoles();

            //var roleModels = roles.Select(r => r.ToRoleModel());

            ////Menus
            //var menus = await _tableMenuResouceRepo.getAllMenus();

            //var menuModels = menus.Select(m => m.ToMenuModel());
            
            ////Permission
            //var perms = await _tableMenuResouceRepo.getAllPermissions();

            //var permModels = perms.Select(p => p.ToRolePermModel());

            var rolesPermAlls = await _tableMenuResouceRepo.getAllRolesPermAll();

            var roleMenuPerms = rolesPermAlls.ToRoleMenuPerm();

            //RoleMenuPerm
            //var rolemenuperms = new RoleMenuPerm();
            //var rolemenupermModels = roles.Select(rmp => rmp.ToRoleMenuPerm(menus, perms);

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(roleMenuPerms, jsonOptions);

            //return Content(jsonResult, "application/json");
            return Ok(roleMenuPerms);
        }

        [HttpPut]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> UpdateRoleMenuPerms([FromQuery] string roleId, [FromQuery] int menuId, [FromBody] UpdatePermPayload payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(roleId) || menuId <= 0 || payload == null || payload.PermId <= 0)
                return BadRequest("Invalid parameters or payload.");

            try
            {
                var roleMenuPerms = new RoleMenuPermAll
                {
                    MenuId = menuId,
                    RoleId = roleId,
                    PermId = payload.PermId
                };
                await _tableMenuResouceRepo.UpdateRoleMenuPerm(roleMenuPerms);
                return Ok(new
                {
                    message = "Permission updated successfully."
                });
            }
            catch (Exception ex)
            {
                // Handle errors
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while updating the permissions.",
                    details = ex.Message
                });
            }
        }

        [HttpPost]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> InsertRoleMenuPerms([FromQuery] string roleId, [FromQuery] int menuId, [FromBody] UpdatePermPayload payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(roleId) || menuId <= 0 || payload == null || payload.PermId <= 0)
                return BadRequest("Invalid parameters or payload.");

            try
            {
                // Insert new entry
                var roleMenuPerms = new RoleMenuPermAll
                {
                    MenuId = menuId,
                    RoleId = roleId,
                    PermId = payload.PermId
                };
                await _tableMenuResouceRepo.InsertRoleMenuPerm(roleMenuPerms);

                return Ok(new
                {
                    message = "successfully."
                });
            }
            catch (Exception ex)
            {
                // Handle errors
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while inserting the permissions.",
                    details = ex.Message
                });
            }
        }

        [HttpDelete]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> DeleteRoleMenuPerms([FromQuery] string roleId, [FromQuery] int menuId, [FromQuery] int permId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrEmpty(roleId) || menuId <= 0)
                return BadRequest("Invalid parameters.");

            try
            {
                var roleMenuPerms = new RoleMenuPermAll
                {
                    MenuId = menuId,
                    RoleId = roleId,
                    PermId = permId
                };
                // Delete the record
                await _tableMenuResouceRepo.DeleteRoleMenuPerm(roleMenuPerms);

                return Ok(new
                {
                    message = "successfully."
                });
            }
            catch (Exception ex)
            {
                // Handle errors
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    message = "An error occurred while deleting the permission.",
                    details = ex.Message
                });
            }
        }
    }

    public class UpdatePermPayload
    {
        public int PermId { get; set; }
    }
}
