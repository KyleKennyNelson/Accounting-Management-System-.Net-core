
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_TaskTypeRepository
    {
        Task<List<LKACSoft_TaskType>> GetAllAsync();
        Task<LKACSoft_TaskType?> GetByIdAsync(string tasktypeID);
    }
}
