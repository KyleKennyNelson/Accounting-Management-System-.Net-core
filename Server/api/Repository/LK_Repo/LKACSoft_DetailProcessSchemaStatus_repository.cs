using api.Identity;
using api.Interfaces.I_LKRepo;
using api.Models;
using LKACSoftModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_DetailProcessSchemaStatus_repository : ILKACSoft_DetailProcessSchemaStatusRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_DetailProcessSchemaStatus_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<V_DetailProcessSchemaStatuses>> GetAllAsync()
        {
            var processSchemaStatusList = await _context.V_DetailProcessSchemaStatuses
                                .FromSqlRaw("EXEC DBO.sp_GetAll_V_DetailProcessSchemaStatuses")
                                .ToListAsync();
            return processSchemaStatusList;
        }

        public async Task<V_DetailProcessSchemaStatuses?> GetByIdAsync(string processSchemaStatusID)
        {
            var processSchemaStatusIdParam = new SqlParameter("@ProcessSchemaStatusID", processSchemaStatusID);

            var processSchemaStatus = (await _context.V_DetailProcessSchemaStatuses
                .FromSqlRaw("EXEC DBO.sp_GetByID_V_DetailProcessSchemaStatus @ProcessSchemaStatusID", processSchemaStatusIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return processSchemaStatus;
        }
    }
}
