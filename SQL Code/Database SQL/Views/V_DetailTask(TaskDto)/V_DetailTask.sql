use db_ab6e43_lkacsoftdb
go

--drop view V_DetailTasks

Create or Alter View V_DetailTasks
AS
    Select
        -- Columns from LKACSoft_Task (Main Table)
        T.TaskID AS TaskID,
        T.DateAssigned AS DateAssigned,
        T.TaskDeadline AS TaskDeadline,
        T.AssignedTo AS AssignedTo,
        T.Title AS TaskTitle,
        T.Detail AS TaskDetail,
        T.TaskStatusID AS TaskStatus,
        T.DateAccepted AS DateAccepted,
        T.DateCompleted AS DateCompleted,
        T.ReviewedBy AS ReviewedBy,
        T.ReviewNote AS ReviewNote,
        T.DateReview AS DateReview,
        T.IsRetried AS IsRetried,
        T.RelatedToExecution AS RelatedToExecution,
        T.TaskType AS TaskType,
        T.DesignatedNumberOfDocument AS DesignatedNumberOfDocument,
        T.NumberOfCompletedDocument AS NumberOfCompletedDocument,
        T.Priority AS Priority,
        T.CreatedAt AS TaskCreatedAt,

        -- Columns from LkacSoft_User (Assigned User)
        assignedUser.ID AS AssignedUserID,
        assignedUser.Username AS AssignedUserUsername,
        assignedUser.Firstname AS AssignedUserFirstname,
        assignedUser.Lastname AS AssignedUserLastname,
        assignedUser.Avatar AS AssignedUserAvatar,
        assignedUser.Address AS AssignedUserAddress,
        assignedUser.District AS AssignedUserDistrict,
        assignedUser.Dob AS AssignedUserDob,
        assignedUser.IsQuitJob AS AssignedUserIsQuitJob,
        assignedUser.DateCreate AS AssignedUserDateCreate,
        assignedUser.Team AS AssignedUserTeam,

        -- Columns from LkacSoft_User (Reviewed User)
        ReviewedUser.ID AS ReviewedUserID,
        ReviewedUser.Username AS ReviewedUserUsername,
        ReviewedUser.Firstname AS ReviewedUserFirstname,
        ReviewedUser.Lastname AS ReviewedUserLastname,
        ReviewedUser.Avatar AS ReviewedUserAvatar,
        ReviewedUser.Address AS ReviewedUserAddress,
        ReviewedUser.District AS ReviewedUserDistrict,
        ReviewedUser.Dob AS ReviewedUserDob,
        ReviewedUser.IsQuitJob AS ReviewedUserIsQuitJob,
        ReviewedUser.DateCreate AS ReviewedUserDateCreate,
        ReviewedUser.Team AS ReviewedUserTeam,

        -- Columns from LKACSoft_Execution
        LKACSoft_Execution.*,

        -- Columns from LKACSoft_TaskStatus
        Ts.TaskStatusID,
        Ts.TaskStatusName,
        Ts.DesignatedColor AS TaskStatusDesignatedColor,

        -- Columns from LKACSoft_Priority
        LKACSoft_Priority.PriorityID,
        LKACSoft_Priority.PriorityName,
        LKACSoft_Priority.DesignatedColor AS PriorityDesignatedColor,

        -- Columns from LKACSoft_TaskType
        LKACSoft_TaskType.*
        
    from LKACSoft_Task T
        Left join LkacSoft_User assignedUser
            on T.AssignedTo = assignedUser.ID
        Left join LkacSoft_User ReviewedUser
            on T.ReviewedBy = ReviewedUser.ID
        Left join LKACSoft_Execution
            on RelatedToExecution = ExecutionID
        Left join LKACSoft_TaskStatus Ts
            on T.TaskStatusID = Ts.TaskStatusID
        Left join LKACSoft_Priority
            on Priority = PriorityID
        Left join LKACSoft_TaskType
            on T.TaskType = TaskTypeID
go

