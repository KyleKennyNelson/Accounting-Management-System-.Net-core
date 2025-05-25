
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_AccountantTeamRepository
    {
        Task<List<LKACSoft_AccountantTeam>> GetAllAsync();
        Task<LKACSoft_AccountantTeam?> GetByIdAsync(string teamID);
        Task<string> UpdateAsync(string TeamID, string LeaderID);
    }
}
