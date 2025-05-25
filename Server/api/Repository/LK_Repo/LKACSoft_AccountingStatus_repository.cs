using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_AccountingStatus_repository : ILKACSoft_AccountingStatusRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_AccountingStatus_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_AccountingStatus>> GetAllAsync()
        {

            var AccountingStatusList =  await _context.LKACSoft_AccountingStatus
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_AccountingStatus")
                                .ToListAsync();
            return AccountingStatusList;
        }

        public async Task<LKACSoft_AccountingStatus?> GetByIdAsync(string ID)
        {
            var IdParam = new SqlParameter("@ID", ID);

            var AccountingStatus = (await _context.LKACSoft_AccountingStatus
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_AccountingStatus @ID", IdParam)
                .ToListAsync())
                .FirstOrDefault();

            return AccountingStatus;
        }
    }
}
