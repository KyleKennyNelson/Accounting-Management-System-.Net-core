using api.Dtos.LK_Dtos.LKACSoft_DetailUsersDTO;
using api.Identity;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DetailUserMapper
    {
        public static LKACSoft_DetailUserKPIDto ToLKACSoft_DetailUserKPIDto(this V_DetailUsersKPI V_DetailUsersKPI)
        {
            return new LKACSoft_DetailUserKPIDto
            {
                UserID = V_DetailUsersKPI.UserID,
                InComplete = V_DetailUsersKPI.InComplete,
                DoneOnTime = V_DetailUsersKPI.DoneOnTime,
                DoneBeforeDL = V_DetailUsersKPI.DoneBeforeDL,
                Late = V_DetailUsersKPI.Late,
            };
        }
        public static LKACSoft_DetailUserDto ToLKACSoft_DetailUserDto(this V_DetailUsers V_DetailUsers, List<ApplicationRole> roles)
        {
            var res = new LKACSoft_DetailUserDto
            {
                User = new LKACSoft_User
                {
                    ID = V_DetailUsers.UserID,
                    Username = V_DetailUsers.Username,
                    Firstname = V_DetailUsers.Firstname,
                    LastName = V_DetailUsers.LastName,
                    Avatar = V_DetailUsers.Avatar,
                    Address = V_DetailUsers.Address,
                    District = V_DetailUsers.District,
                    Dob = V_DetailUsers.DateOfBirth,
                    IsQuitJob = V_DetailUsers.IsQuitJob,
                    DateCreate = V_DetailUsers.UserDateCreated,
                },
                Email = V_DetailUsers.Email,
            };


            if (roles != null || roles.Any())
            {
                res.Roles = roles;
            }

            if (V_DetailUsers.TeamID != null)
            {
                res.Team = new LKACSoft_AccountantTeam
                {
                    TeamID = V_DetailUsers.TeamID,
                    TeamName = V_DetailUsers.TeamName,
                    TeamLeader = V_DetailUsers.TeamLeader
                };
            }

            if (V_DetailUsers.InProgressTasksCount != null)
            {
                res.InProgressTasksCount = V_DetailUsers.InProgressTasksCount;
            }

            if (V_DetailUsers.DoneTasksCount != null)
            {
                res.DoneTasksCount = V_DetailUsers.DoneTasksCount;
            }

            return res;
        }
    }
}
