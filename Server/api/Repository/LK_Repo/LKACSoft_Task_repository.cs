using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;
using api.Helpers;
using api.Identity;
using api.Interfaces.I_LKRepo;
using LKACSoftModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace api.Repository.LK_Repo
{
    public class LKACSoft_Task_repository : ILKACSoft_TaskRepository
    {
        private readonly ApplicationDBContext _context;

        public LKACSoft_Task_repository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<LKACSoft_AmountTaskStatusDto?> GetAmountOfTaskStatusPerYearAsync(int Quarter, int Year, [FromQuery] QueryObject_TaskVisualization queryIndex)
        {

            var yearParam = new SqlParameter("@Year", Year);

            var quarterParam = new SqlParameter("@Quarter", Quarter);

            var userIDParam = new SqlParameter("@UserID", queryIndex.UserID ?? (object)DBNull.Value);

            var departmentCodeParam = new SqlParameter("@DepartmentCode", queryIndex.DepartmentCode ?? (object)DBNull.Value);

            var teamIDParam = new SqlParameter("@TeamID", queryIndex.TeamID ?? (object)DBNull.Value);

            var amountRetriedTask = (await _context.LKACSoft_AmountTaskStatusDto
                .FromSqlRaw("EXEC DBO.sp_GetAmountOfTaskStatusPerQuartersOfYear_LKACSoft_Task @Quarter, @Year, @UserID, @DepartmentCode, @TeamID",
                quarterParam, yearParam, userIDParam, departmentCodeParam, teamIDParam)
                .ToListAsync())
                .FirstOrDefault();

            return amountRetriedTask;
        }

        public async Task<LKACSoft_AmountRetriedTaskDto?> GetAmountOfTasksPerYearAsync(int Quarter, int Year, [FromQuery] QueryObject_TaskVisualization queryIndex)
        {

            var yearParam = new SqlParameter("@Year", Year);

            var quarterParam = new SqlParameter("@Quarter", Quarter);

            var userIDParam = new SqlParameter("@UserID", queryIndex.UserID ?? (object)DBNull.Value);

            var departmentCodeParam = new SqlParameter("@DepartmentCode", queryIndex.DepartmentCode ?? (object)DBNull.Value);

            var teamIDParam = new SqlParameter("@TeamID", queryIndex.TeamID ?? (object)DBNull.Value);

            var amountRetriedTask = (await _context.LKACSoft_AmountRetriedTaskDto
                .FromSqlRaw("EXEC DBO.sp_GetAmountOfTaskPerQuartersOfYear_LKACSoft_Task @Quarter, @Year, @UserID, @DepartmentCode, @TeamID",
                quarterParam, yearParam, userIDParam, departmentCodeParam, teamIDParam)
                .ToListAsync())
                .FirstOrDefault();

            return amountRetriedTask;
        }

        public async Task<LKACSoft_TaskVisualizationDto?> GetTaskVisualizationAsync(int Quarter, int Year, [FromQuery] QueryObject_TaskVisualization queryIndex)
        {
            var yearParam = new SqlParameter("@Year", Year);

            var quarterParam = new SqlParameter("@Quarter", Quarter);

            var userIDParam = new SqlParameter("@UserID", queryIndex.UserID ?? (object)DBNull.Value);

            var departmentCodeParam = new SqlParameter("@DepartmentCode", queryIndex.DepartmentCode ?? (object)DBNull.Value);

            var teamIDParam = new SqlParameter("@TeamID", queryIndex.TeamID ?? (object)DBNull.Value);

            var amountRetriedTask = (await _context.LKACSoft_TaskVisualizationDto
                .FromSqlRaw("EXEC DBO.sp_TaskVisualize_LKACSoft_Task @Quarter, @Year, @UserID, @DepartmentCode, @TeamID",
                quarterParam, yearParam, userIDParam, departmentCodeParam, teamIDParam)
                .ToListAsync())
                .FirstOrDefault();

            return amountRetriedTask;
        }

        public async Task<LKACSoft_TaskAverageCompletionTimePerQuarterDto?> GetTaskAverageCompletionTimePerQuarterAsync(int Quarter, int Year, 
            [FromQuery] QueryObject_TaskVisualization queryIndex)
        {
            var yearParam = new SqlParameter("@Year", Year);

            var quarterParam = new SqlParameter("@Quarter", Quarter);

            var userIDParam = new SqlParameter("@UserID", queryIndex.UserID ?? (object)DBNull.Value);

            var departmentCodeParam = new SqlParameter("@DepartmentCode", queryIndex.DepartmentCode ?? (object)DBNull.Value);

            var teamIDParam = new SqlParameter("@TeamID", queryIndex.TeamID ?? (object)DBNull.Value);

            var amountRetriedTask = (await _context.LKACSoft_TaskAverageCompletionTimePerQuarterDto
                .FromSqlRaw("EXEC DBO.sp_TaskAverageCompletionTimePerQuarter @Quarter, @Year, @UserID, @DepartmentCode, @TeamID",
                quarterParam, yearParam, userIDParam, departmentCodeParam, teamIDParam)
                .ToListAsync())
                .FirstOrDefault();

            return amountRetriedTask;
        }

        public async Task<(LKACSoft_Task?, string?)> AddAsync(LKACSoft_Task task, bool isLeader, string userID)
        {
            //var dateAssigned = new SqlParameter("@DateAssigned", task.DateAssigned ?? (object)DBNull.Value);
            var taskDeadline = new SqlParameter("@TaskDeadline", task.TaskDeadline ?? (object)DBNull.Value);
            var assignedTo = new SqlParameter("@AssignedTo", task.AssignedTo ?? (object)DBNull.Value);
            var title = new SqlParameter("@Title", task.Title ?? (object)DBNull.Value);
            //var detail = new SqlParameter("@Detail", task.Detail ?? (object)DBNull.Value);

            var taskStatusID = new SqlParameter("@TaskStatusID", task.TaskStatusID ?? (object)DBNull.Value);

            //var taskStatusId = new SqlParameter("@TaskStatusId", task.TaskStatusId ?? (object)DBNull.Value);
            //var dateAccepted = new SqlParameter("@DateAccepted", task.DateAccepted ?? (object)DBNull.Value);
            //var dateCompleted = new SqlParameter("@DateCompleted", task.DateCompleted ?? (object)DBNull.Value);
            //var reviewedBy = new SqlParameter("@ReviewedBy", task.ReviewedBy ?? (object)DBNull.Value);
            //var reviewNote = new SqlParameter("@ReviewNote", task.ReviewNote ?? (object)DBNull.Value);
            //var dateReview = new SqlParameter("@DateReview", task.DateReview ?? (object)DBNull.Value);
            //var IsRetried = new SqlParameter("@IsRetried", task.IsRetried);
            var relatedToExecution = new SqlParameter("@RelatedToExecution", task.RelatedToExecution ?? (object)DBNull.Value);
            var taskType = new SqlParameter("@TaskType", task.TaskType ?? (object)DBNull.Value);
            //var documentType = new SqlParameter("@DocumentType", task.DocumentType ?? (object)DBNull.Value);
            //var designatedNumberOfDocument = new SqlParameter("@DesignatedNumberOfDocument", task.DesignatedNumberOfDocument);
            //var numberOfCompletedDocument = new SqlParameter("@NumberOfCompletedDocument", task.NumberOfCompletedDocument);
            var priority = new SqlParameter("@Priority", task.Priority ?? (object)DBNull.Value);

            var isLeaderParam = new SqlParameter("@IsLeader", isLeader);

            var UserID = new SqlParameter("@UserID", userID);

            var NewTaskID = new SqlParameter("@NewTaskID", SqlDbType.VarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Insert_LKACSoft_Task
                        @TaskDeadline, @AssignedTo, @Title, @TaskStatusID,
                        @RelatedToExecution, @TaskType, @Priority, @isLeader, @UserID,
                        @NewTaskID OUTPUT, @ResponseMessage OUTPUT
                """,
                taskDeadline, assignedTo, title, taskStatusID,
                relatedToExecution, taskType, priority, isLeaderParam, UserID, NewTaskID, responseMessage
            );

            task.TaskID = NewTaskID.Value.ToString();

            return (task, responseMessage.Value.ToString());
        }


        public async Task<bool> DeleteAsync(string taskID, bool isLeader, string userID)
        {
            var taskIdParam = new SqlParameter("@TaskID", taskID);
            var isLeaderParam = new SqlParameter("@IsLeader", isLeader);

            var UserID = new SqlParameter("@UserID", userID);

            var rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC DBO.sp_Delete_LKACSoft_Task @TaskID, @IsLeader, @UserID", taskIdParam, isLeaderParam, UserID);
            return rowsAffected > 0;
        }


        public async Task<List<LKACSoft_Task>> GetAllAsync(string userID, bool isManager, bool isLeader)
        {
            var userIdParam = new SqlParameter("@UserID", userID);

            var isManagerParam = new SqlParameter("@IsManager", isManager);

            var isLeaderParam = new SqlParameter("@IsLeader", isLeader);

            var taskList = await _context.LKACSoft_Task
                                .FromSqlRaw("EXEC DBO.sp_GetAll_LKACSoft_Task @UserID, @IsLeader, @IsManager", userIdParam, isLeaderParam, isManagerParam)
                                .ToListAsync();
            return taskList;
        }

        public async Task<LKACSoft_Task?> GetByIdAsync(string userID, string taskID, bool isManager, bool isLeader)
        {
            var userIdParam = new SqlParameter("@UserID", userID ?? (object)DBNull.Value);

            var taskIdParam = new SqlParameter("@TaskID", taskID);

            var isManagerParam = new SqlParameter("@IsManager", isManager);

            var isLeaderParam = new SqlParameter("@IsLeader", isLeader);

            var task = (await _context.LKACSoft_Task
                .FromSqlRaw("EXEC DBO.sp_GetByID_LKACSoft_Task @TaskID, @UserID, @IsLeader, @IsManager",
                taskIdParam, userIdParam, isLeaderParam, isManagerParam)
                .ToListAsync())
                .FirstOrDefault();

            return task;
        }

        public async Task<LKACSoft_Task?> GetByLastestCreatedIdAsync()
        {
            var task = (await _context.LKACSoft_Task
                .FromSqlRaw("EXEC DBO.sp_GetbyLatestCreatedID_LKACSoft_Task")
                .ToListAsync())
                .FirstOrDefault();

            return task;
        }

        public async Task<(LKACSoft_Task?, string?)> UpdateAsync(LKACSoft_Task task, bool isLeader, string userID)
        {
            var taskId = new SqlParameter("@TaskID", task.TaskID);
            //var dateAssigned = new SqlParameter("@DateAssigned", task.DateAssigned ?? (object)DBNull.Value);
            var taskDeadline = new SqlParameter("@TaskDeadline", task.TaskDeadline ?? (object)DBNull.Value);
            var assignedTo = new SqlParameter("@AssignedTo", string.IsNullOrWhiteSpace(task.AssignedTo) ? (object)DBNull.Value : task.AssignedTo);
            var title = new SqlParameter("@Title", string.IsNullOrWhiteSpace(task.Title) ? (object)DBNull.Value : task.Title);
            var detail = new SqlParameter("@Detail", string.IsNullOrWhiteSpace(task.Detail) ? (object)DBNull.Value : task.Detail);
            var taskStatusId = new SqlParameter("@TaskStatusID", string.IsNullOrWhiteSpace(task.TaskStatusID) ? (object)DBNull.Value : task.TaskStatusID);
            var dateAccepted = new SqlParameter("@DateAccepted", task.DateAccepted ?? (object)DBNull.Value);
            var dateCompleted = new SqlParameter("@DateCompleted", task.DateCompleted ?? (object)DBNull.Value);
            var reviewedBy = new SqlParameter("@ReviewedBy", string.IsNullOrWhiteSpace(task.ReviewedBy) ? (object)DBNull.Value : task.ReviewedBy);
            var reviewNote = new SqlParameter("@ReviewNote", string.IsNullOrWhiteSpace(task.ReviewNote) ? (object)DBNull.Value : task.ReviewNote);
            var dateReview = new SqlParameter("@DateReview", task.DateReview ?? (object)DBNull.Value);
            var IsRetried = new SqlParameter("@IsRetried", task.IsRetried ?? (object)DBNull.Value);
            var relatedToExecution = new SqlParameter("@RelatedToExecution", string.IsNullOrWhiteSpace(task.RelatedToExecution) ? (object)DBNull.Value : task.RelatedToExecution);
            var taskType = new SqlParameter("@TaskType", string.IsNullOrWhiteSpace(task.TaskType) ? (object)DBNull.Value : task.TaskType);
            //var documentType = new SqlParameter("@DocumentType", string.IsNullOrWhiteSpace(task.DocumentType) ? (object)DBNull.Value : task.DocumentType);
            var designatedNumberOfDocument = new SqlParameter("@DesignatedNumberOfDocument", task.DesignatedNumberOfDocument ?? (object)DBNull.Value);
            var numberOfCompletedDocument = new SqlParameter("@NumberOfCompletedDocument", task.NumberOfCompletedDocument ?? (object)DBNull.Value);
            var priority = new SqlParameter("@Priority", string.IsNullOrWhiteSpace(task.Priority) ? (object)DBNull.Value : task.Priority);

            var isLeaderParam = new SqlParameter("@IsLeader", isLeader);

            var UserID = new SqlParameter("@UserID", userID);

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Update_LKACSoft_Task
                        @TaskID, @TaskDeadline, @AssignedTo, @Title, @Detail, @TaskStatusID,
                        @DateAccepted, @DateCompleted, @ReviewedBy, @ReviewNote, @DateReview, @IsRetried,
                        @RelatedToExecution, @TaskType, @DesignatedNumberOfDocument,
                        @NumberOfCompletedDocument, @Priority, @isLeader, @UserID, @ResponseMessage OUTPUT
                """,
                taskId, taskDeadline, assignedTo, title, detail, taskStatusId,
                dateAccepted, dateCompleted, reviewedBy, reviewNote, dateReview, IsRetried,
                relatedToExecution, taskType, designatedNumberOfDocument,
                numberOfCompletedDocument, priority, isLeaderParam, UserID, responseMessage
            );

            return (task, responseMessage.Value.ToString());
        }

        public async Task<(LKACSoft_Task?, string?)> UpdateTaskStatusAsync(LKACSoft_Task task, string userID, bool isManager)
        {
            var taskId = new SqlParameter("@TaskID", task.TaskID);
            var taskStatusId = new SqlParameter("@TaskStatusID", task.TaskStatusID ?? (object)DBNull.Value);

            var UserID = new SqlParameter("@UserID", userID);
            var isManagerParam = new SqlParameter("@IsManager", isManager);

            var responseMessage = new SqlParameter("@ResponseMessage", SqlDbType.NVarChar, 255)
            {
                Direction = ParameterDirection.Output
            };

            await _context.Database.ExecuteSqlRawAsync(
                """
                    EXEC DBO.sp_Update_LKACSoft_Task_TaskStatus
                        @TaskID, @TaskStatusID, @UserID, @IsManager, @ResponseMessage OUTPUT
                """,
                taskId, taskStatusId, UserID, isManagerParam, responseMessage
            );

            return (task, responseMessage.Value.ToString());
        }
    }
}
