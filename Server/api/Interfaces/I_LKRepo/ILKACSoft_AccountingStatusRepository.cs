
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_AccountingStatusRepository
    {
        Task<List<LKACSoft_AccountingStatus>> GetAllAsync();
        Task<LKACSoft_AccountingStatus?> GetByIdAsync(string ID);
    }
}
