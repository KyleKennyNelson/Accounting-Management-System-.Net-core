use db_ab6e43_lkacsoftdb
go

--drop view V_DetailUsersKPI

Create or Alter View V_DetailUsersKPI
AS
    Select
        -- Columns from LKACSoft_User (Main Table)
        U.ID as UserID,

        -- Count the number of tasks based on their status
        SUM(CASE WHEN TS.TaskStatusName NOT IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) THEN 1 ELSE 0 END) AS InComplete,
        SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) = 0 THEN 1 ELSE 0 END) AS DoneOnTime,
        SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) < 0 THEN 1 ELSE 0 END) AS DoneBeforeDL,
        SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) > 0 THEN 1 ELSE 0 END) AS Late
        
    from LKACSoft_User U
        left join LKACSoft_Task T
            on U.ID = T.AssignedTo
        left join LKACSoft_TaskStatus TS
            on T.TaskStatusID = TS.TaskStatusID
    
    group by 
        U.ID;
go

-- select *
-- from LKACSoft_Task T
-- where TaskStatusID = 'TS04'
--     or TaskStatusID = 'TS05'

-- update LKACSoft_Task
-- set DateCompleted = '2025-02-11'