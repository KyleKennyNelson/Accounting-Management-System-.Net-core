using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_TaskStatus_repository : ILKACSoft_TaskStatusRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_TaskStatus_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        
        public async Task<List<LKACSoft_TaskStatus>> GetAllAsync()
        {
            var taskList =  await _context.LKACSoft_TaskStatus
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_TaskStatus")
                                .ToListAsync();
            return taskList;
        }

        public async Task<LKACSoft_TaskStatus> GetByIdAsync(string taskstatusID)
        {
            var taskstatusIdParam = new SqlParameter("@TaskStatusID", taskstatusID);

            var taskstatus = (await _context.LKACSoft_TaskStatus
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_TaskStatus @TaskStatusID", taskstatusIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return taskstatus;
        }
    }
}
