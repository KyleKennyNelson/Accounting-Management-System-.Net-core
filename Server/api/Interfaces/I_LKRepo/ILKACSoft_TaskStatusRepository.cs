
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_TaskStatusRepository
    {
        Task<List<LKACSoft_TaskStatus>> GetAllAsync();
        Task<LKACSoft_TaskStatus?> GetByIdAsync(string taskstatusID);
    }
}
