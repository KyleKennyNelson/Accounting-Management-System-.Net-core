
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_ArchivingStatusRepository
    {
        Task<List<LKACSoft_ArchivingStatus>> GetAllAsync();
        Task<LKACSoft_ArchivingStatus?> GetByIdAsync(string ID);
    }
}
