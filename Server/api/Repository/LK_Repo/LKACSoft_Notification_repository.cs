using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_Notification_repository : ILKACSoft_NotificationRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_Notification_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_Notification>> GetAllAsync(string? userID)
        {
            var userIdParam = new SqlParameter("@UserID", userID ?? (object)DBNull.Value);

            var notificationList =  await _context.LKACSoft_Notification
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_Notification @UserID", userIdParam)
                                .ToListAsync();
            return notificationList;
        }

        public async Task<LKACSoft_Notification?> GetByIdAsync(string? userID)
        {
            var userIdParam = new SqlParameter("@UserID", userID ?? (object)DBNull.Value);

            var notification = (await _context.LKACSoft_Notification
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_Notification @UserID", userIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return notification;
        }
    }
}
