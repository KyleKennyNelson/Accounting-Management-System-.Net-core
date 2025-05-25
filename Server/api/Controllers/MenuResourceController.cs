using api.Dtos.MenuResource;
using api.Interfaces;
using api.Mappers;
using api.Models.AuthModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Text.Json;

namespace api.Controllers
{
    [Route("api/MenuResources")]
    [ApiController]
    [Authorize]
    public class MenuResourceController : Controller
    {
        private readonly IMenuResource_Repository _tableMenuResouceRepo;
        public MenuResourceController(IMenuResource_Repository tableMenuResouceRepo)
        {
            this._tableMenuResouceRepo = tableMenuResouceRepo;
        }

        [HttpGet]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetAllMenuResource()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (menus, roleMenus, permissionRoles) = await _tableMenuResouceRepo.getAllMenuResource();

            // Map permissions
            var permissionMap = permissionRoles
                .GroupBy(p => new { p.rolemenuID, p.permissionID })
                .ToDictionary(
                    group => new { group.Key.rolemenuID, group.Key.permissionID },
                    group => group.First().ToPermissionDto()
                );

            // Map roles and their permissions grouped by menuID
            var rolePermissionMap = roleMenus
                .GroupBy(r => r.menuID)
                .ToDictionary(
                    group => group.Key, // Key is the menuID
                    group => group
                        .GroupBy(roleMenu => roleMenu.roleName) // Group roles by roleName
                        .Select(roleGroup => roleGroup.First().ToRolePermissionDto( // Use the first roleMenu to create the DTO
                            roleGroup
                                .SelectMany(roleMenu =>
                                            permissionMap
                                                .Where(p => p.Key.rolemenuID == roleMenu.rolemenuID)
                                                .Select(p => p.Value) // Use existing permission DTOs
                                            )
                                                .Distinct()
                                                .ToList() // Consolidate permissions for the role
                        ))
                    .ToList()
                );

            // Recursive function to build the menu tree
            List<MenuResourceDto> BuildMenuTree(int? parentId)
            {
                return menus
                    .Where(menu => menu.menuParent == parentId)
                    .Select(menu => menu.ToMenuResourceDto(
                        BuildMenuTree(menu.menuID), // Recursive call for nested children
                        rolePermissionMap.ContainsKey(menu.menuID)
                        ? rolePermissionMap[menu.menuID] // Roles for this menu
                        : new List<RolePermissionDto>() // No roles found
                    ))
                    .ToList();
            }

            // Build menu tree starting from the root (where parentId is null)
            var menuDtos = BuildMenuTree(null);

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(menuDtos, jsonOptions);

            //return Content(jsonResult, "application/json");
            return Ok(menuDtos);
        }

        [HttpGet("{MenuName}")]
        [Authorize(Policy = "ApiPermissionPolicy")]
        public async Task<IActionResult> GetMenuResourceByMenuName([FromRoute] string MenuName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (menus, roleMenus, permissionRoles) = await _tableMenuResouceRepo.getAllMenuResource();

            // Map permissions
            var permissionMap = permissionRoles
                .GroupBy(p => new { p.rolemenuID, p.permissionID })
                .ToDictionary(
                    group => new { group.Key.rolemenuID, group.Key.permissionID },
                    group => group.First().ToPermissionDto()
                );

            // Map roles and their permissions grouped by menuID
            var rolePermissionMap = roleMenus
                .GroupBy(r => r.menuID)
                .ToDictionary(
                    group => group.Key, // Key is the menuID
                    group => group
                        .GroupBy(roleMenu => roleMenu.roleName) // Group roles by roleName
                        .Select(roleGroup => roleGroup.First().ToRolePermissionDto( // Use the first roleMenu to create the DTO
                            roleGroup
                                .SelectMany(roleMenu =>
                                            permissionMap
                                                .Where(p => p.Key.rolemenuID == roleMenu.rolemenuID)
                                                .Select(p => p.Value) // Use existing permission DTOs
                                            )
                                                .Distinct()
                                                .ToList() // Consolidate permissions for the role
                        ))
                    .ToList()
                );

            // Recursive function to build the menu tree
            List<MenuResourceDto> BuildMenuTree(string menuName)
            {
                // Find the parent menu by the given name
                var parentMenu = menus.FirstOrDefault(m => m.menuName == menuName);

                if (parentMenu == null)
                {
                    return new List<MenuResourceDto>(); // Return an empty list if the menu name doesn't exist
                }

                // Create a recursive function to build the tree for a menu
                List<MenuResourceDto> BuildTree(int? parentId)
                {
                    return menus
                        .Where(menu => menu.menuParent == parentId)
                        .Select(menu => menu.ToMenuResourceDto(
                            BuildTree(menu.menuID), // Recursive call for children
                            rolePermissionMap.ContainsKey(menu.menuID)
                                ? rolePermissionMap[menu.menuID] // Roles for this menu
                                : new List<RolePermissionDto>() // No roles found
                        ))
                        .ToList();
                }

                // Build the tree starting from the parent menu
                return new List<MenuResourceDto>
                {
                    parentMenu.ToMenuResourceDto(
                        BuildTree(parentMenu.menuID), // Build children recursively
                        rolePermissionMap.ContainsKey(parentMenu.menuID)
                            ? rolePermissionMap[parentMenu.menuID] // Roles for the parent menu
                            : new List<RolePermissionDto>() // No roles found
                    )
                };
            }

            // Build menu tree starting from the root (where parentId is null)
            var menuDtos = BuildMenuTree(MenuName);

            // Serialize the object manually using JsonSerializer
            //var jsonOptions = new JsonSerializerOptions
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure camelCase naming for JSON
            //    WriteIndented = true // Optional: Pretty-print JSON
            //};
            //var jsonResult = JsonSerializer.Serialize(menuDtos, jsonOptions);

            //return Content(jsonResult, "application/json");

            return Ok(menuDtos);
        }
    }
}
