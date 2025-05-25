
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_PriorityRepository
    {
        Task<List<LKACSoft_Priority>> GetAllAsync();
        Task<LKACSoft_Priority?> GetByIdAsync(string priorityID);
    }
}
