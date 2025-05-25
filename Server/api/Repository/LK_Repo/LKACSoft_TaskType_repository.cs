using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_TaskType_repository : ILKACSoft_TaskTypeRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_TaskType_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        
        public async Task<List<LKACSoft_TaskType>> GetAllAsync()
        {
            var tasktypeList =  await _context.LKACSoft_TaskType
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_TaskType")
                                .ToListAsync();
            return tasktypeList;
        }

        public async Task<LKACSoft_TaskType> GetByIdAsync(string tasktypeID)
        {
            var tasktypeIdParam = new SqlParameter("@TaskTypeID", tasktypeID);

            var tasktype = (await _context.LKACSoft_TaskType
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_TaskType @TaskTypeID", tasktypeIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return tasktype;
        }
    }
}
