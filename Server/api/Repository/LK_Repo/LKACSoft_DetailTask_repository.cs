using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_DetailTask_repository : ILKACSoft_DetailTaskRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_DetailTask_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        
        public async Task<List<V_DetailTasks>> GetAllAsync(string userID, bool isManager)
        {
            var userIdParam = new SqlParameter("@UserID", userID);

            var isManagerParam = new SqlParameter("@IsManager", isManager);

            var detailtaskList = await _context.V_DetailTasks
                                .FromSqlRaw("EXEC DBO.sp_GetAll_V_DetailTask @UserID, @IsManager", userIdParam, isManagerParam)
                                .ToListAsync();

            return detailtaskList;
        }

        public async Task<V_DetailTasks> GetByIdAsync(string userID, string taskID, bool isManager)
        {
            var userIdParam = new SqlParameter("@UserID", userID);

            var taskIdParam = new SqlParameter("@TaskID", taskID);

            var isManagerParam = new SqlParameter("@IsManager", isManager);

            var detailtask = (await _context.V_DetailTasks
                                .FromSqlRaw("EXEC DBO.sp_GetByID_V_DetailTask @UserID, @TaskID, @IsManager", 
                                userIdParam, taskIdParam, isManagerParam)
                                .ToListAsync())
                                .FirstOrDefault();

            return detailtask;
        }
    }
}
