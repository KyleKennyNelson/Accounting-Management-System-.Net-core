
using api.Helpers;
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_DetailExecutionRepository
    {
        Task<List<V_DetailExecutions>> GetAllAsync(QueryObject_DetailCustomer query);
        Task<V_DetailExecutions?> GetByIdAsync(string executionID);
    }
}
