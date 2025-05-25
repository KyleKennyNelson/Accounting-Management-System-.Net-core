
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_ExecutionRepository
    {
        Task<List<LKACSoft_Execution>> GetAllAsync();
        Task<LKACSoft_Execution?> GetByIdAsync(string executionID);
        Task<(LKACSoft_Execution, string)> AddAsync(LKACSoft_Execution execution);
    }
}
