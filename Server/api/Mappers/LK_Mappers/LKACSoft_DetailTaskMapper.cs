using api.Dtos.LK_Dtos.LKACSoft_DetailTasksDTO;
using LKACSoftModel;

namespace api.Mappers.LK_Mappers
{
    public static class LKACSoft_DetailTaskMapper
    {
        public static LKACSoft_DetailTaskDto ToLKACSoft_DetailTaskDto(this V_DetailTasks V_DetailTasks)
        {
            var res = new LKACSoft_DetailTaskDto
            {
                Task = new LKACSoft_Task
                {
                    TaskID = V_DetailTasks.TaskID,
                    DateAssigned = V_DetailTasks.DateAssigned,
                    TaskDeadline = V_DetailTasks.TaskDeadline,
                    AssignedTo = V_DetailTasks.AssignedTo,
                    Title = V_DetailTasks.TaskTitle,
                    Detail = V_DetailTasks.TaskDetail,
                    TaskStatusID = V_DetailTasks.TaskStatus,
                    DateAccepted = V_DetailTasks.DateAccepted,
                    DateCompleted = V_DetailTasks.DateCompleted,
                    ReviewedBy = V_DetailTasks.ReviewedBy,
                    ReviewNote = V_DetailTasks.ReviewNote,
                    DateReview = V_DetailTasks.DateReview,
                    IsRetried = V_DetailTasks.IsRetried,
                    RelatedToExecution = V_DetailTasks.RelatedToExecution,
                    TaskType = V_DetailTasks.TaskType,
                    DesignatedNumberOfDocument = V_DetailTasks.DesignatedNumberOfDocument,
                    NumberOfCompletedDocument = V_DetailTasks.NumberOfCompletedDocument,
                    Priority = V_DetailTasks.Priority,
                    CreatedAt = V_DetailTasks.TaskCreatedAt
                }
            };

            // ── Assigned‑to user ───────────────────────────────────────────
            if (V_DetailTasks.AssignedUserID != null)
            {
                res.AssignedTo = new LKACSoft_User
                {
                    ID = V_DetailTasks.AssignedUserID,
                    Username = V_DetailTasks.AssignedUserUsername,
                    Firstname = V_DetailTasks.AssignedUserFirstname,
                    LastName = V_DetailTasks.AssignedUserLastname,
                    Avatar = V_DetailTasks.AssignedUserAvatar,
                    Address = V_DetailTasks.AssignedUserAddress,
                    District = V_DetailTasks.AssignedUserDistrict,
                    Dob = V_DetailTasks.AssignedUserDob,
                    IsQuitJob = V_DetailTasks.AssignedUserIsQuitJob,
                    DateCreate = V_DetailTasks.AssignedUserDateCreate,
                    Team = V_DetailTasks.AssignedUserTeam
                };
            }

            // ── Assigned‑by / reviewer ─────────────────────────────────────
            if (V_DetailTasks.ReviewedUserID != null)
            {
                res.AssignedBy = new LKACSoft_User
                {
                    ID = V_DetailTasks.ReviewedUserID,
                    Username = V_DetailTasks.ReviewedUserUsername,
                    Firstname = V_DetailTasks.ReviewedUserFirstname,
                    LastName = V_DetailTasks.ReviewedUserLastname,
                    Avatar = V_DetailTasks.ReviewedUserAvatar,
                    Address = V_DetailTasks.ReviewedUserAddress,
                    District = V_DetailTasks.ReviewedUserDistrict,
                    Dob = V_DetailTasks.ReviewedUserDob,
                    IsQuitJob = V_DetailTasks.ReviewedUserIsQuitJob,
                    DateCreate = V_DetailTasks.ReviewedUserDateCreate,
                    Team = V_DetailTasks.ReviewedUserTeam
                };
            }

            // ── Execution header ───────────────────────────────────────────
            if (V_DetailTasks.ExecutionID != null)
            {
                res.Execution = new LKACSoft_Execution
                {
                    ExecutionID = V_DetailTasks.ExecutionID,
                    ExecutionName = V_DetailTasks.ExecutionName,
                    CreatedBy = V_DetailTasks.CreatedBy,
                    DateCreated = V_DetailTasks.DateCreated,
                    IsPeriodic = V_DetailTasks.IsPeriodic,
                    ProcessSchemaStatus = V_DetailTasks.ProcessSchemaStatus,
                    ProcessSchemaID = V_DetailTasks.ProcessSchemaID,
                    RelatedToCustomer = V_DetailTasks.RelatedToCustomer
                };
            }

            // ── Task status ────────────────────────────────────────────────
            if (V_DetailTasks.TaskStatusID != null)
            {
                res.TaskStatus = new LKACSoft_TaskStatus
                {
                    TaskStatusID = V_DetailTasks.TaskStatusID,
                    TaskStatusName = V_DetailTasks.TaskStatusName,
                    DesignatedColor = V_DetailTasks.TaskStatusDesignatedColor
                };
            }

            // ── Priority ───────────────────────────────────────────────────
            if (V_DetailTasks.PriorityID != null)
            {
                res.Priority = new LKACSoft_Priority
                {
                    PriorityID = V_DetailTasks.PriorityID,
                    PriorityName = V_DetailTasks.PriorityName,
                    DesignatedColor = V_DetailTasks.PriorityDesignatedColor
                };
            }

            // ── Task type ──────────────────────────────────────────────────
            if (V_DetailTasks.TaskTypeID != null)
            {
                res.TaskType = new LKACSoft_TaskType
                {
                    TaskTypeID = V_DetailTasks.TaskTypeID,
                    TaskTypeName = V_DetailTasks.TaskTypeName,
                    Description = V_DetailTasks.Description,
                    TaskTypeDesignatedColor = V_DetailTasks.TaskTypeDesignatedColor
                };
            }

            return res;
        }
    }
}
