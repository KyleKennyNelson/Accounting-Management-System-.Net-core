using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Threading.Tasks;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_User_repository : ILKACSoft_UserRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_User_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<LKACSoft_User>> GetAllAsync()
        {

            var userList =  await _context.LKACSoft_User
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_User")
                                .ToListAsync();
            return userList;
        }

        public async Task<LKACSoft_User?> GetByIdAsync(string userID)
        {
            var userIdParam = new SqlParameter("@ID", userID);

            var user = (await _context.LKACSoft_User
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_User @ID", userIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return user;
        }

        public async Task<string> AddAvatarAsync(LKACSoft_User userAvatar)
        {
            var id = new SqlParameter("@ID", userAvatar.ID);
            var avatar = new SqlParameter("@Avatar", string.IsNullOrWhiteSpace(userAvatar.Avatar) ? (object)DBNull.Value : userAvatar.Avatar);


            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Insert_LKACSoft_User_Avatar
                        @ID, @Avatar,
                        @ResponseMessage OUTPUT
                """,
                id, avatar, responseMessage
            );

            return responseMessage.Value.ToString();
        }

        public async Task<string> UpdateAvatarAsync(LKACSoft_User userAvatar)
        {
            var id = new SqlParameter("@ID", userAvatar.ID);
            var avatar = new SqlParameter("@Avatar", string.IsNullOrWhiteSpace(userAvatar.Avatar) ? (object)DBNull.Value : userAvatar.Avatar);


            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Insert_LKACSoft_JobTaskFile_File
                        @ID, @Avatar,
                        @ResponseMessage OUTPUT
                """,
                id, avatar, responseMessage
            );

            return responseMessage.Value.ToString();
        }

        public async Task<(LKACSoft_User, string)> AddAsync(LKACSoft_User user)
        {
            var userId = new SqlParameter("@UserID", user.ID ?? (object)DBNull.Value);
            var userName = new SqlParameter("@UserName", user.Username ?? (object)DBNull.Value);
            //var firstname = new SqlParameter("@Firstname", user.Firstname ?? (object)DBNull.Value);
            //var lastname = new SqlParameter("@LastName", user.LastName ?? (object)DBNull.Value);
            //var address = new SqlParameter("@Address", user.Address ?? (object)DBNull.Value);
            //var district = new SqlParameter("@District", user.District ?? (object)DBNull.Value);
            //var dob = new SqlParameter("@Dob", user.Dob ?? (object)DBNull.Value);
            //var isQuitJob = new SqlParameter("@IsQuitJob", user.IsQuitJob ?? (object)DBNull.Value);
            //var team = new SqlParameter("@Team", user.Team ?? (object)DBNull.Value);

            //var NewUserID = new SqlParameter("@NewUserID", SqlDbType.VarChar, 255)
            //{
            //    Direction = ParameterDirection.Output
            //};

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            //await _context.Database.ExecuteSqlRawAsync(
            //    """
            //        EXEC DBO.sp_Insert_LKACSoft_User
            //            @Username, @Firstname, @LastName, @Address, @District,
            //            @Dob, @IsQuitJob, @Team, @NewUserID OUTPUT, @ResponseMessage OUTPUT
            //    """,
            //    username, firstname, lastname, address, district,
            //    dob, isQuitJob, team, NewUserID, responseMessage
            //);

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Insert_LKACSoft_User
                        @UserID, @Username, @ResponseMessage OUTPUT
                """
            ,
                userId, userName, responseMessage
            );

            // Return the inserted user object and the response message
            return (user, responseMessage.Value.ToString());
        }
    }
}
