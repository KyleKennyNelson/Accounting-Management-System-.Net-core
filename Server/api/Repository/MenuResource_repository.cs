using api.Dtos.MenuResource;
using api.Identity;
using api.Interfaces;
using api.Models;
using LKACSoftModel;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class MenuResource_repository : IMenuResource_Repository
    {
        private readonly ApplicationDBContext _context;
        public MenuResource_repository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Identity.Permission>> getAllPermissions()
        {
            var resultTablesOfPermission = await _context.Permissions
                .FromSqlRaw("""
                                select Id, Name
                                from dbo.Permission
                            """)
                .ToListAsync();

            return resultTablesOfPermission;
        }
        public async Task<List<V_Menu>> getAllMenus()
        {
            var resultTablesOfMenu = await _context.VMenus
                .FromSqlRaw("""
                                select *
                                From DBO.V_Menu
                            """)
                .ToListAsync();

            return resultTablesOfMenu;
        }
        public async Task<(List<V_Menu>, List<V_Role_Menu>, List<V_Permission_RoleMenu>)> getAllMenuResource()
        {
            var resultTablesOfMenu = await _context.VMenus
                .FromSqlRaw("""
                                select *
                                From DBO.V_Menu
                            """)
                .ToListAsync();

            var resultTablesOfRoleMenu = await _context.VRole_Menus
                .FromSqlRaw("""
                                select *
                                From DBO.V_Role_Menu
                            """)
                .ToListAsync();

            var resultTablesOfPermission = await _context.VPermission_Roles
                .FromSqlRaw("""
                                select *
                                From DBO.V_Permission_RoleMenu
                            """)
                .ToListAsync();

            return (resultTablesOfMenu, resultTablesOfRoleMenu, resultTablesOfPermission);
        }

        public async Task<List<V_Role>> getAllRoles()
        {
            var resultTablesOfRoles = await _context.RoleModels
                .FromSqlRaw("""
                                select *
                                From DBO.V_Role
                            """)
                .ToListAsync();

            return resultTablesOfRoles;
        }

        public async Task<List<RoleMenuPermAll>> getAllRolesPermAll()
        {
            var resultTablesOfRoleMenuPerms = await _context.rolemenupermAll
                .FromSqlRaw("""
                                select *
                                From DBO.V_RoleMenuPerm
                            """)
                .ToListAsync();

            return resultTablesOfRoleMenuPerms;
        }

        public async Task InsertRoleMenuPerm(RoleMenuPermAll roleMenuPerm)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    """
                        EXEC DBO.sp_InsertRoleMenuPerms
                        @RoleID = {0}, 
                        @MenuID = {1},
                        @PermID = {2}
                    """,
                    roleMenuPerm.RoleId,
                    roleMenuPerm.MenuId,
                    roleMenuPerm.PermId);
            }
            catch (Exception ex)
            {
                // Log the error and rethrow or handle it based on your application needs
                throw new Exception("Failed to insert RoleMenuPerm.", ex);
            }
        }

        public async Task UpdateRoleMenuPerm(RoleMenuPermAll roleMenuPerm)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    """
                        EXEC DBO.sp_UpdateRoleMenuPerms
                        @RoleID = {0}, 
                        @MenuID = {1},
                        @PermID = {2}
                    """,
                    roleMenuPerm.RoleId,
                    roleMenuPerm.MenuId,
                    roleMenuPerm.PermId);
            }
            catch (Exception ex)
            {
                // Log the error and rethrow or handle it based on your application needs
                throw new Exception("Failed to update RoleMenuPerm.", ex);
            }
        }

        public async Task DeleteRoleMenuPerm(RoleMenuPermAll roleMenuPerm)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync(
                    """
                        EXEC DBO.sp_DeleteRoleMenuPerms
                        @RoleID = {0}, 
                        @MenuID = {1},
                        @PermID = {2}
                    """,
                    roleMenuPerm.RoleId,
                    roleMenuPerm.MenuId,
                    roleMenuPerm.PermId);
            }
            catch (Exception ex)
            {
                // Log the error and rethrow or handle it based on your application needs
                throw new Exception("Failed to delete RoleMenuPerm.", ex);
            }
        }
    }
}
