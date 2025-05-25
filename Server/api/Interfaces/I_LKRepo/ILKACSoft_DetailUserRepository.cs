
using api.Helpers;
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_DetailUserRepository
    {
        Task<List<V_DetailUsers>> GetAllAsync();
        Task<V_DetailUsers?> GetByIdAsync(string userID);
        Task<V_DetailUsersKPI?> GetUserKPIAsync(string userID);
    }
}
