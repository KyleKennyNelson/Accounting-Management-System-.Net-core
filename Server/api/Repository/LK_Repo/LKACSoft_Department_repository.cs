using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_Department_repository : ILKACSoft_DepartmentRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_Department_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_Department>> GetAllAsync()
        {

            var userList =  await _context.LKACSoft_Department
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_Department")
                                .ToListAsync();
            return userList;
        }

        public async Task<LKACSoft_Department?> GetByIdAsync(string departmentCode)
        {
            var departmentCodeParam = new SqlParameter("@Code", departmentCode);

            var department = (await _context.LKACSoft_Department
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_Department @Code", departmentCodeParam)
                .ToListAsync())
                .FirstOrDefault();

            return department;
        }
    }
}
