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
    public class LKACSoft_ProcessSchema_repository : ILKACSoft_ProcessSchemaRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_ProcessSchema_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_ProcessSchema>> GetAllAsync()
        {
            var processSchemaList = await _context.LKACSoft_ProcessSchema
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_ProcessSchema")
                                .ToListAsync();
            return processSchemaList;
        }

        public async Task<LKACSoft_ProcessSchema?> GetByIdAsync(string processSchemaID)
        {
            var processSchemaIdParam = new SqlParameter("@ProcessSchemaID", processSchemaID);

            var processSchema = (await _context.LKACSoft_ProcessSchema
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_ProcessSchema @ProcessSchemaID", processSchemaIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return processSchema;
        }
    }
}
