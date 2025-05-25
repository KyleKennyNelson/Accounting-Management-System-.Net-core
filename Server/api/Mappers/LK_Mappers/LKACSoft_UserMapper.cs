using api.Dtos.LK_Dtos.LKACSoft_JobTaskFileDTO;
using api.Dtos.LK_Dtos.LKACSoft_UserDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_UserMapper
    {
        public static LKACSoft_DetailUserKPIDto ToLKACSoft_UserDto(this LKACSoft_User LKACSoft_User)
        {
            return new LKACSoft_DetailUserKPIDto
            {
                ID = LKACSoft_User.ID,
                Username = LKACSoft_User.Username,
                Firstname = LKACSoft_User.Firstname,
                LastName = LKACSoft_User.LastName,
                Avatar = LKACSoft_User.Avatar,
                Address = LKACSoft_User.Address,
                District = LKACSoft_User.District,
                Dob = LKACSoft_User.Dob,
                IsQuitJob = LKACSoft_User.IsQuitJob,
                DateCreate = LKACSoft_User.DateCreate,
                Team = LKACSoft_User.Team
            };
        }

        public static LKACSoft_User CreateLKACSoft_UserDto(this CreateLKACSoft_UserDto CreateLKACSoft_UserDto)
        {
            return new LKACSoft_User
            {
                Username = CreateLKACSoft_UserDto.Username,
                Firstname = CreateLKACSoft_UserDto.Firstname,
                LastName = CreateLKACSoft_UserDto.LastName,
                Address = CreateLKACSoft_UserDto.Address,
                District = CreateLKACSoft_UserDto.District,
                Dob = CreateLKACSoft_UserDto.Dob,
                IsQuitJob = CreateLKACSoft_UserDto.IsQuitJob,
                DateCreate = CreateLKACSoft_UserDto.DateCreate,
                Team = CreateLKACSoft_UserDto.Team
            };
        }

        public static LKACSoft_User TestCreateLKACSoft_UserDto(this TestCreateLKACSoft_UserDto CreateLKACSoft_UserDto)
        {
            return new LKACSoft_User
            {
                ID = CreateLKACSoft_UserDto.UserID,
                Username = CreateLKACSoft_UserDto.Username
            };
        }

        public static LKACSoft_User UpdateLKACSoft_User_AvatarDto(this UpdateLKACSoft_User_AvatarDto UpdateLKACSoft_User_AvatarDto, string ID)
        {
            return new LKACSoft_User
            {
                ID = ID,
                Avatar = UpdateLKACSoft_User_AvatarDto.Avatar
            };
        }
    }
}
