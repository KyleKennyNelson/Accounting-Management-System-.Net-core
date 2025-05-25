
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_NotificationRepository
    {
        Task<List<LKACSoft_Notification>> GetAllAsync(string? userID);
        Task<LKACSoft_Notification?> GetByIdAsync(string? userID);
    }
}
