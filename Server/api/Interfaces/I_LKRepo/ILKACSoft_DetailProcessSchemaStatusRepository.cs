
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_DetailProcessSchemaStatusRepository
    {
        Task<List<V_DetailProcessSchemaStatuses>> GetAllAsync();
        Task<V_DetailProcessSchemaStatuses?> GetByIdAsync(string processSchemaStatusID);
    }
}
