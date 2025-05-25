
using LKACSoftModel;

namespace api.Interfaces.I_LKRepo
{
    public interface ILKACSoft_UserRepository
    {
        Task<List<LKACSoft_User>> GetAllAsync();
        Task<LKACSoft_User?> GetByIdAsync(string userID);
        Task<(LKACSoft_User, string)> AddAsync(LKACSoft_User user);
        Task<string> UpdateAvatarAsync(LKACSoft_User userAvatar);
        Task<string> AddAvatarAsync(LKACSoft_User userAvatar);
    }
}
