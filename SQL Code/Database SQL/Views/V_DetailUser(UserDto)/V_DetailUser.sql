use db_ab6e43_lkacsoftdb
go

--drop view V_DetailUsers

Create or Alter View V_DetailUsers
AS
    Select
        -- Columns from LKACSoft_User (Main Table)
        U.ID as UserID,
        U.Username,
        U.Firstname,
        U.LastName,
        U.Avatar,
        U.Address,
        U.District,
        U.Dob as DateOfBirth,
        U.IsQuitJob,
        U.DateCreate as UserDateCreated,

        -- Columns from AspNetUsers (Email)
        anu.Email,

        -- Columns from AspNetRoles (Roles)
        -- anr.Id as RoleID,
        -- anr.Name as RoleName,
        -- anr.NormalizedName as RoleNormalizedName,
        -- anr.ConcurrencyStamp as RoleConcurrencyStamp,

        -- Columns from LkacSoft_AccountantTeam (Team)
        LAT.TeamID,
        LAT.TeamName,
        LAT.TeamLeader,

        -- Count the number of tasks based on their status
        SUM(CASE WHEN TS.TaskStatusName = N'Đang thực hiện' THEN 1 ELSE 0 END) AS InProgressTasksCount,
        SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) THEN 1 ELSE 0 END) AS DoneTasksCount
        
    from LKACSoft_User U
        Left join AspNetUsers anu
            on U.ID = anu.Id
        -- Left join AspNetUserRoles anur
        --     on anu.Id = anur.UserId
        -- Left join AspNetRoles anr
        --     on anur.RoleId = anr.Id
        Left join LKACSoft_AccountantTeam LAT
            on U.Team = LAT.TeamID
        left join LKACSoft_Task T
            on U.ID = T.AssignedTo
        left join LKACSoft_TaskStatus TS
            on T.TaskStatusID = TS.TaskStatusID
    
    group by 
        U.ID,
        U.Username,
        U.Firstname,
        U.LastName,
        U.Avatar,
        U.Address,
        U.District,
        U.Dob,
        U.IsQuitJob,
        U.DateCreate,
        U.Team,
        anu.Email,
        -- anr.Id,
        -- anr.Name,
        -- anr.NormalizedName,
        -- anr.ConcurrencyStamp,
        LAT.TeamID,
        LAT.TeamName,
        LAT.TeamLeader;
go