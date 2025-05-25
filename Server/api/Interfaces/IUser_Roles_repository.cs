using api.Models;
using LKACSoftModel;

namespace api.Interfaces
{
    public interface IUser_Roles_repository
    {
        public Task<List<UserRolesModel>> getRolesFromUser(string UserName);
        public Task<(List<V_User>, List<V_UserId_RoleId>)> getUserContainRoles();
    }
}
