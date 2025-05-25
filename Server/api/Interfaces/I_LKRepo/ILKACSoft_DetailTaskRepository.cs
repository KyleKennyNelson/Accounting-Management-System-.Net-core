
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_DetailTaskRepository
    {
        Task<List<V_DetailTasks>> GetAllAsync(string userID, bool isManager);
        Task<V_DetailTasks?> GetByIdAsync(string userID, string taskID, bool isManager);
    }
}
