
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_JobTaskFileRepository
    {
        Task<List<V_DetailJobTaskFiles>> GetAllAsync();
        Task<V_DetailJobTaskFiles?> GetByIdAsync(string jobTaskFileCode);
        Task<LKACSoft_JobTaskFile?> GetByCodeJTFAsync(string jobTaskFileCode);
        Task<(LKACSoft_JobTaskFile, string)> AddAsync(LKACSoft_JobTaskFile jobTaskFile);
        Task<(LKACSoft_JobTaskFile, string)> UpdateAsync(LKACSoft_JobTaskFile jobTaskFile);
        Task<string> UpdateFileAsync(LKACSoft_JobTaskFile jobTaskFile);
        Task<string> AddFileAsync(LKACSoft_JobTaskFile jobTaskFile);
        Task<bool> DeleteAsync(string jobTaskFileCode);
    }
}
