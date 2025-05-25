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
    public class LKACSoft_AccountantTeam_repository : ILKACSoft_AccountantTeamRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_AccountantTeam_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_AccountantTeam>> GetAllAsync()
        {

            var accountantTeamList =  await _context.LKACSoft_AccountantTeam
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_AccountantTeam")
                                .ToListAsync();
            return accountantTeamList;
        }

        public async Task<LKACSoft_AccountantTeam?> GetByIdAsync(string teamID)
        {
            var teamIdParam = new SqlParameter("@TeamID", teamID);

            var accountantTeam = (await _context.LKACSoft_AccountantTeam
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_AccountantTeam @TeamID", teamIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return accountantTeam;
        }

        public async Task<string> UpdateAsync(string TeamID, string LeaderID)
        {
            var teamID = new SqlParameter("@TeamID", TeamID);

            var leaderID = new SqlParameter("@LeaderID", string.IsNullOrWhiteSpace(LeaderID) ? (object)DBNull.Value : LeaderID);

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Update_LKACSoft_AccountantTeam
                        @TeamID, @LeaderID, @ResponseMessage OUTPUT
                """,
                teamID, leaderID, responseMessage
            );

            return responseMessage.Value.ToString();
        }
    }
}
