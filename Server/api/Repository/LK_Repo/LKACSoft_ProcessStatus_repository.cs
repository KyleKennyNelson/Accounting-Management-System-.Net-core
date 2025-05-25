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
    public class LKACSoft_ProcessStatus_repository : ILKACSoft_ProcessStatusRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_ProcessStatus_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_ProcessStatus>> GetAllAsync()
        {
            var processStatusList = await _context.LKACSoft_ProcessStatus
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_ProcessStatus")
                                .ToListAsync();
            return processStatusList;
        }

        public async Task<LKACSoft_ProcessStatus?> GetByIdAsync(string processStatusID)
        {
            var processStatusIdParam = new SqlParameter("@ProcessStatusID", processStatusID);

            var processStatus = (await _context.LKACSoft_ProcessStatus
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_ProcessStatus @ProcessStatusID", processStatusIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return processStatus;
        }
    }
}
