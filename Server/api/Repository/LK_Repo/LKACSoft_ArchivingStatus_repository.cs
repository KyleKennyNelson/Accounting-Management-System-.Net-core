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
    public class LKACSoft_ArchivingStatus_repository : ILKACSoft_ArchivingStatusRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_ArchivingStatus_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_ArchivingStatus>> GetAllAsync()
        {

            var ArchivingStatusList =  await _context.LKACSoft_ArchivingStatus
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_ArchivingStatus")
                                .ToListAsync();
            return ArchivingStatusList;
        }

        public async Task<LKACSoft_ArchivingStatus?> GetByIdAsync(string ID)
        {
            var IdParam = new SqlParameter("@ID", ID);

            var ArchivingStatus = (await _context.LKACSoft_ArchivingStatus
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_ArchivingStatus @ID", IdParam)
                .ToListAsync())
                .FirstOrDefault();

            return ArchivingStatus;
        }
    }
}
