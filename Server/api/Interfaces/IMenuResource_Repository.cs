using api.Models;
using LKACSoftModel;

namespace api.Interfaces
{
    public interface IMenuResource_Repository
    {
        public Task<List<V_Menu>> getAllMenus();
        public Task<List<V_Role>> getAllRoles();
        public Task<List<Identity.Permission>> getAllPermissions();
        public Task<List<RoleMenuPermAll>> getAllRolesPermAll();
        public Task InsertRoleMenuPerm(RoleMenuPermAll roleMenuPerm);
        public Task UpdateRoleMenuPerm(RoleMenuPermAll roleMenuPerm);
        public Task DeleteRoleMenuPerm(RoleMenuPermAll roleMenuPerm);
        public Task<(List<V_Menu>, List<V_Role_Menu>, List<V_Permission_RoleMenu>)> getAllMenuResource();
    }
}
