
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_ProcessStatusRepository
    {
        Task<List<LKACSoft_ProcessStatus>> GetAllAsync();
        Task<LKACSoft_ProcessStatus?> GetByIdAsync(string processStatusID);
        //Task<(LKACSoft_ProcessStatus, string)> AddAsync(LKACSoft_ProcessStatus processStatus);
    }
}
