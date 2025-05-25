using api.Identity;
using LKACSoftModel;

namespace api.Dtos.LK_Dtos.LKACSoft_DetailUsersDTO
{
    public class LKACSoft_DetailUserDto
    {
        public required LKACSoft_User User { get; set; }
        public string? Email { get; set; }

        public List<ApplicationRole>? Roles { get; set; }

        public LKACSoft_AccountantTeam? Team { get; set; }

        public int? InProgressTasksCount { get; set; }

        public int? DoneTasksCount { get; set; }
    }
}
