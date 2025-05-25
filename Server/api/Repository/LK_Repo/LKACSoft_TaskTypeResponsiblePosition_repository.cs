using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_TaskTypeResponsiblePosition_repository : ILKACSoft_TaskTypeResponsiblePositionRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_TaskTypeResponsiblePosition_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        
        public async Task<List<LKACSoft_TaskTypeResponsiblePosition>> GetAllAsync()
        {
            var tasktyperesponsiblepositionList =  await _context.LKACSoft_TaskTypeResponsiblePosition
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_TaskTypeResponsiblePosition")
                                .ToListAsync();
            return tasktyperesponsiblepositionList;
        }

        public async Task<LKACSoft_TaskTypeResponsiblePosition> GetByIdAsync(string tasktypeID)
        {
            var tasktypeIdParam = new SqlParameter("@TaskTypeID", tasktypeID);

            var tasktyperesponsibleposition = (await _context.LKACSoft_TaskTypeResponsiblePosition
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_TaskTypeResponsiblePosition @TaskTypeID", tasktypeIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return tasktyperesponsibleposition;
        }
    }
}
