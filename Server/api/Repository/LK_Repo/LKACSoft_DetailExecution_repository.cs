using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_DetailExecution_repository : ILKACSoft_DetailExecutionRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_DetailExecution_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        
        public async Task<List<V_DetailExecutions>> GetAllAsync(QueryObject_DetailCustomer query)
        {
            var detailexecutionList =  await _context.V_DetailExecutions
                                .FromSqlRaw("EXEC DBO.sp_GetAll_V_DetailExecution")
                                .AsQueryable()
                                .ToListAsync();
            if (!string.IsNullOrEmpty(query.CustomerCode))
            {
                detailexecutionList = detailexecutionList.Where(de => de.Code.Contains(query.CustomerCode)).ToList();
            }
            return detailexecutionList;
        }

        public async Task<V_DetailExecutions> GetByIdAsync(string executionID)
        {
            var executionIDParam = new SqlParameter("@executionID", executionID);

            var detailexecution = (await _context.V_DetailExecutions
                .FromSqlRaw("EXEC DBO.sp_GetByID_V_DetailExecution @executionID", executionIDParam)
                .ToListAsync())
                .FirstOrDefault();

            return detailexecution;
        }
    }
}
