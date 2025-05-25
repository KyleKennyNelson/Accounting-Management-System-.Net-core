
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_ProcessSchemaRepository
    {
        Task<List<LKACSoft_ProcessSchema>> GetAllAsync();
        Task<LKACSoft_ProcessSchema?> GetByIdAsync(string processSchemaID);
    }
}
