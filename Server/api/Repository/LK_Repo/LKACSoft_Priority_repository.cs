using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_Priority_repository : ILKACSoft_PriorityRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_Priority_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_Priority>> GetAllAsync()
        {

            var userList =  await _context.LKACSoft_Priority
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_Priority")
                                .ToListAsync();
            return userList;
        }

        public async Task<LKACSoft_Priority?> GetByIdAsync(string priorityID)
        {
            var priorityIdParam = new SqlParameter("@ID", priorityID);

            var priority = (await _context.LKACSoft_Priority
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_Priority @ID", priorityIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return priority;
        }
    }
}
