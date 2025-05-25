using api.Models;
using LKACSoftModel;

namespace api.Mappers
{
    public static class UserMapper
    {
        public static User ToUserModel(this V_User User, List<Roles> roles)
        {
            return new User
            {
                Id = User.Id,
                Username = User.UserName,
                Email = User.Email,
                EmailVerified = User.EmailVerified,
                PhoneNumber = User.PhoneNumber,
                PhoneVerified = User.PhoneVerified,
                Roles = roles,
                Locked = User.Locked,
                Selected = false
            };
        }

        public static Roles ToRolesModel(this V_UserId_RoleId roles)
        {
            return new Roles
            {
                UserId = roles.UserId,
                RoleId = roles.RoleId,
                RoleName = roles.RoleName
            };
        }
    }
}
