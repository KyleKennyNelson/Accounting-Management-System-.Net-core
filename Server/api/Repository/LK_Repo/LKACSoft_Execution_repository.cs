using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_Execution_repository : ILKACSoft_ExecutionRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_Execution_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<(LKACSoft_Execution, string)> AddAsync(LKACSoft_Execution execution)
        {
            var executionName = new SqlParameter("@ExecutionName", execution.ExecutionName ?? (object)DBNull.Value);
            var createdBy = new SqlParameter("@CreatedBy", execution.CreatedBy ?? (object)DBNull.Value);
            //var getDocsDate = new SqlParameter("@GetDocsDate", Execution.GetDocsDate ?? (object)DBNull.Value);
            var isPeriodic = new SqlParameter("@IsPeriodic", execution.IsPeriodic ?? (object)DBNull.Value);
            var processSchemaStatus = new SqlParameter("@ProcessSchemaStatus", execution.ProcessSchemaStatus ?? (object)DBNull.Value);
            var processSchemaID = new SqlParameter("@ProcessSchemaID", execution.ProcessSchemaID ?? (object)DBNull.Value);
            var relatedToCustomer = new SqlParameter("@RelatedToCustomer", execution.RelatedToCustomer ?? (object)DBNull.Value);


            var NewExecutionID = new SqlParameter("@NewExecutionID", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Insert_LKACSoft_Execution
                        @ExecutionName, @CreatedBy, @IsPeriodic,
                        @ProcessSchemaStatus, @ProcessSchemaID, @RelatedToCustomer, 
                        @NewExecutionID OUTPUT, @ResponseMessage OUTPUT
                """,
                executionName, createdBy, isPeriodic,
                processSchemaStatus, processSchemaID, relatedToCustomer, NewExecutionID, responseMessage
            );

            execution.ExecutionID = NewExecutionID.Value.ToString();

            return (execution, responseMessage.Value.ToString());
        }

        public async Task<List<LKACSoft_Execution>> GetAllAsync()
        {

            var executionList =  await _context.LKACSoft_Execution
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_Execution")
                                .ToListAsync();
            return executionList;
        }

        public async Task<LKACSoft_Execution> GetByIdAsync(string executionID)
        {
            var executionIdParam = new SqlParameter("@ExecutionID", executionID);

            var execution = (await _context.LKACSoft_Execution
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_Execution @ExecutionID", executionIdParam)
                .ToListAsync())
                .FirstOrDefault();

            return execution;
        }
    }
}
