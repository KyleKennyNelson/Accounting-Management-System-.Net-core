using api.Dtos.LK_Dtos.LKACSoft_AccountantTeamDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_AccountantTeamMapper
    {
        public static LKACSoft_AccountantTeamDto ToLKACSoft_AccountantTeamDto(this LKACSoft_AccountantTeam LKACSoft_AccountantTeam)
        {
            return new LKACSoft_AccountantTeamDto
            {
                TeamID = LKACSoft_AccountantTeam.TeamID,
                TeamName = LKACSoft_AccountantTeam.TeamName,
                TeamLeader = LKACSoft_AccountantTeam.TeamLeader
            };
        }
    }
}
