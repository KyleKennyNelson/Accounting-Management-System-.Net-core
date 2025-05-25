using api.Identity;
using api.Interfaces;
using api.Models;
using LKACSoftModel;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class User_Roles_repository : IUser_Roles_repository
    {
        private readonly ApplicationDBContext _context;
        public User_Roles_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<UserRolesModel>> getRolesFromUser(string UserName)
        {
            var UserRoles = await _context.userRolesModels
                .FromSqlRaw("EXEC DBO.sp_TakeRoleFromUser @UserName={0}", UserName)
                .ToListAsync();
            return UserRoles;
        }

        public async Task<(List<V_User>, List<V_UserId_RoleId>)> getUserContainRoles()
        {
            var Users = await _context.V_Users
                .FromSqlRaw("Select * From DBO.V_User")
                .ToListAsync();

            var Roles = await _context.V_UserId_RoleIds
                .FromSqlRaw("Select * From DBO.V_UserId_RoleId")
                .ToListAsync();

            return (Users, Roles);
        }
    }
}
