using Amazon.Runtime.Documents;
using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_DetailUser_repository : ILKACSoft_DetailUserRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_DetailUser_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<V_DetailUsers>> GetAllAsync()
        {
            var detailUserList = await _context.V_DetailUsers
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_DetailUser")
                                .ToListAsync();

            return detailUserList;
        }

        public async Task<V_DetailUsers?> GetByIdAsync(string userID)
        {
            var userIDParam = new SqlParameter("@UserID", userID);

            var detailUser = (await _context.V_DetailUsers
                            .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_DetailUser @UserID",
                                userIDParam)
                            .ToListAsync())
                            .FirstOrDefault();

            return detailUser;
        }

        public async Task<V_DetailUsersKPI?> GetUserKPIAsync(string userID)
        {
            var userIDParam = new SqlParameter("@UserID", userID);

            var detailUserKPI = (await _context.V_DetailUsersKPI
                            .FromSqlRaw("EXEC DBO.sp_GetByID_V_DetailUserKPI @UserID",
                                userIDParam)
                            .ToListAsync())
                            .FirstOrDefault();

            return detailUserKPI;
        }
    }
}
