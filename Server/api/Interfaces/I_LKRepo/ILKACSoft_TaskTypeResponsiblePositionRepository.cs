
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_TaskTypeResponsiblePositionRepository
    {
        Task<List<LKACSoft_TaskTypeResponsiblePosition>> GetAllAsync();
        Task<LKACSoft_TaskTypeResponsiblePosition?> GetByIdAsync(string tasktypeID);
    }
}
