Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAmountOfTaskStatusPerQuartersOfYear_LKACSoft_Task
--@UserID VARCHAR(255),
--@isLeader BIT,
--@IsManager BIT
@Quarter INT,
@Year INT,
@UserID varchar(255) = null,
@DepartmentCode varchar(10) = null,
@TeamID varchar(255) = null

as
BEGIN

    SET NOCOUNT ON;

    -- Calculate the start and end months of the quarter
    DECLARE @Month INT = 3 * (@Quarter - 1) + 1;

    DECLARE @StartDate DATE = DATEFROMPARTS(@Year, @Month, 1);
    DECLARE @EndDate DATE = EOMONTH(DATEADD(MONTH, 2, @StartDate));

    if (@UserID is null and @DepartmentCode is null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,

            SUM(CASE WHEN TS.TaskStatusName NOT IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) THEN 1 ELSE 0 END) as 'InComplete',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) = 0
                THEN 1 ELSE 0 END) as 'DoneOnTime',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) < 0
                THEN 1 ELSE 0 END) as 'DoneBeforeDL',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) > 0
                THEN 1 ELSE 0 END) as 'Late'

        From LKACSoft_Task T
        left join LKACSoft_TaskStatus TS
            on T.TaskStatusID = TS.TaskStatusID
        WHERE T.DateAssigned >= @StartDate
            AND T.DateAssigned <= @EndDate;

        return;
    END


    if (@UserID is not null and @DepartmentCode is null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,

            SUM(CASE WHEN TS.TaskStatusName NOT IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) THEN 1 ELSE 0 END) as 'InComplete',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) = 0
                THEN 1 ELSE 0 END) as 'DoneOnTime',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) < 0
                THEN 1 ELSE 0 END) as 'DoneBeforeDL',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) > 0
                THEN 1 ELSE 0 END) as 'Late'
        From LKACSoft_Task T
        left join LKACSoft_TaskStatus TS
            on T.TaskStatusID = TS.TaskStatusID
        Where T.AssignedTo = @UserID
            and T.DateAssigned >= @StartDate
            and T.DateAssigned <= @EndDate;

        return;
    END

    if (@UserID is null and @DepartmentCode is null and @TeamID is not null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,

            SUM(CASE WHEN TS.TaskStatusName NOT IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) THEN 1 ELSE 0 END) as 'InComplete',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) = 0
                THEN 1 ELSE 0 END) as 'DoneOnTime',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) < 0
                THEN 1 ELSE 0 END) as 'DoneBeforeDL',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) > 0
                THEN 1 ELSE 0 END) as 'Late'
        From LKACSoft_Task T
        left join LKACSoft_TaskStatus TS
            on T.TaskStatusID = TS.TaskStatusID
        left join LKACSoft_User
            on T.AssignedTo = ID
        Left join LKACSoft_AccountantTeam
            on Team = TeamID

        Where TeamID = @TeamID
            and T.DateAssigned >= @StartDate
            and T.DateAssigned <= @EndDate;

        return
    END


    if (@UserID is null and @DepartmentCode is not null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,

            SUM(CASE WHEN TS.TaskStatusName NOT IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) THEN 1 ELSE 0 END) as 'InComplete',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) = 0
                THEN 1 ELSE 0 END) as 'DoneOnTime',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) < 0
                THEN 1 ELSE 0 END) as 'DoneBeforeDL',

            SUM(CASE WHEN TS.TaskStatusName IN ( N'Đã hoàn thành', N'Xác nhận hoàn thành' ) and datediff(day, T.TaskDeadline, T.DateCompleted) > 0
                THEN 1 ELSE 0 END) as 'Late'
        From LKACSoft_Task T
        left join LKACSoft_TaskStatus TS
            on T.TaskStatusID = TS.TaskStatusID
        left join LKACSoft_User
            on T.AssignedTo = ID
        Left join LKACSoft_UserPosition UP
            on ID = UP.UserID
        Left join LKACSoft_RolePosition RP
            on UP.RoleID = RP.RoleID
        Left join LKACSoft_Department
            on RP.LKACSoft_DepartmentCode = Code

        Where Code = @DepartmentCode
            and T.DateAssigned >= @StartDate
            and T.DateAssigned <= @EndDate;

        return
    END
    
    return
END
go

-- select *
-- from AspNetUsers

-- exec sp_GetAmountOfTaskStatusPerQuartersOfYear_LKACSoft_Task 1, 2025;
-- go


Create or Alter Proc sp_GetAmountOfTaskPerQuartersOfYear_LKACSoft_Task
--@UserID VARCHAR(255),
--@isLeader BIT,
--@IsManager BIT
@Quarter INT,
@Year INT,
@UserID varchar(255) = null,
@DepartmentCode varchar(10) = null,
@TeamID varchar(255) = null

as
BEGIN

    SET NOCOUNT ON;

    -- Calculate the start and end months of the quarter
    DECLARE @Month1 INT = 3 * (@Quarter - 1) + 1;
    DECLARE @Month2 INT = @Month1 + 1;
    DECLARE @Month3 INT = @Month1 + 2;

    if (@UserID is null and @DepartmentCode is null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,
            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth1',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth1',

            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth2',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth2',

            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth3',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth3'
        From LKACSoft_Task;

        return;
    END


    if (@UserID is not null and @DepartmentCode is null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,
            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth1',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth1',

            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth2',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth2',

            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth3',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth3'
        From LKACSoft_Task
        Where AssignedTo = @UserID;

        return;
    END

    if (@UserID is null and @DepartmentCode is null and @TeamID is not null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,
            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth1',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth1',

            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth2',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth2',

            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth3',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth3'
        From LKACSoft_Task
            left join LKACSoft_User
                on AssignedTo = ID
            Left join LKACSoft_AccountantTeam
                on Team = TeamID

        Where TeamID = @TeamID;

        return
    END


    if (@UserID is null and @DepartmentCode is not null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,
            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth1',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth1',

            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth2',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth2',

            SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'IsRetriedMonth3',
            SUM(CASE WHEN IsRetried = 0 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) as 'PerfectMonth3'
        From LKACSoft_Task
            left join LKACSoft_User
                on AssignedTo = ID
            Left join LKACSoft_UserPosition UP
                on ID = UP.UserID
            Left join LKACSoft_RolePosition RP
                on UP.RoleID = RP.RoleID
            Left join LKACSoft_Department
                on RP.LKACSoft_DepartmentCode = Code

        Where Code = @DepartmentCode;

        return
    END
    
    return
END
go

-- exec sp_GetAmountOfTaskPerQuartersOfYear_LKACSoft_Task 2, 2025, null, 'D01', null
-- go



Create or Alter Proc sp_TaskVisualize_LKACSoft_Task
--@UserID VARCHAR(255),
--@isLeader BIT,
--@IsManager BIT
@Quarter INT,
@Year INT,
@UserID varchar(255) = null,
@DepartmentCode varchar(10) = null,
@TeamID varchar(255) = null
as
BEGIN

    SET NOCOUNT ON;

    -- Calculate the start and end months of the quarter
    DECLARE @Month1 INT = 3 * (@Quarter - 1) + 1;
    DECLARE @Month2 INT = @Month1 + 1;
    DECLARE @Month3 INT = @Month1 + 2;

    DECLARE @StartDate DATE = DATEFROMPARTS(@Year, @Month1, 1);
    DECLARE @EndDate DATE = EOMONTH(DATEADD(MONTH, 2, @StartDate));

    -- Get number of users once
    DECLARE @UserCount INT = (SELECT COUNT(*) FROM LKACSoft_User U 
                            left join LKACSoft_UserPosition UP 
                                on U.ID = UP.UserID
                            left join LKACSoft_RolePosition RP 
                                on UP.RoleID = RP.RoleID
                            left join LKACSoft_Position P 
                                on RP.LKACSoft_PositionCode = P.Code
                            where P.Name != N'Trưởng phòng');
    
    if (@UserID is null and @DepartmentCode is null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,
            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth1],

            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth2],

            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth3],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth1],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 + 1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth2],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 + 2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth3]

        FROM LKACSoft_Task
        WHERE DateAssigned >= @StartDate
            AND DateAssigned <= @EndDate;
        return
    END

    if (@UserID is not null and @DepartmentCode is null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,
            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth1],

            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth2],

            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth3],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth1],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 + 1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth2],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 + 2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth3]

        FROM LKACSoft_Task
        WHERE AssignedTo = @UserID
            AND DateAssigned >= @StartDate
            AND DateAssigned <= @EndDate;
        return
    END

    if (@UserID is null and @DepartmentCode is null and @TeamID is not null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,
            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth1],

            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth2],

            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth3],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth1],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 + 1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth2],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 + 2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth3]

        From LKACSoft_Task
            left join LKACSoft_User
                on AssignedTo = ID
            Left join LKACSoft_AccountantTeam
                on Team = TeamID
        WHERE Team = @TeamID
            AND DateAssigned >= @StartDate
            AND DateAssigned <= @EndDate;
        return
    END

    if (@UserID is null and @DepartmentCode is not null and @TeamID is null)
    BEGIN
        Select
            @Year as Year,
            @Quarter as Quarter,
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,
            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth1],

            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth2],

            IsNull(CAST(SUM(CASE WHEN IsRetried = 1 AND MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) /
            NULLIF(SUM(CASE WHEN MONTH(DateAssigned) = @Month3 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END), 0) * 100 AS FLOAT), 0) AS [RedoMonth3],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth1],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 + 1 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth2],

            IsNull(CAST((SUM(CASE WHEN MONTH(DateAssigned) = @Month1 + 2 and Year(DateAssigned) = @Year THEN 1 ELSE 0 END) + 0.5) /
            NULLIF(@UserCount, 0) AS INT), 0) AS [AvgTaskAssignedPerUserMonth3]

        From LKACSoft_Task
            left join LKACSoft_User
                on AssignedTo = ID
            Left join LKACSoft_UserPosition UP
                on ID = UP.UserID
            Left join LKACSoft_RolePosition RP
                on UP.RoleID = RP.RoleID
            Left join LKACSoft_Department
                on RP.LKACSoft_DepartmentCode = Code
        WHERE Code = @DepartmentCode
            AND DateAssigned >= @StartDate
            AND DateAssigned <= @EndDate;
        return
    END

    return
END
go

-- exec sp_TaskVisualize_LKACSoft_Task 1, 2025, null, null, 'T001'
-- go


Create or Alter Proc sp_TaskAverageCompletionTimePerQuarter
--@UserID VARCHAR(255),
--@isLeader BIT,
--@IsManager BIT
@Quarter INT,
@Year INT,
@UserID varchar(255) = null,
@DepartmentCode varchar(10) = null,
@TeamID varchar(255) = null
as
BEGIN

    SET NOCOUNT ON;

    DECLARE @Month1 INT = 3 * (@Quarter - 1) + 1;
    DECLARE @Month2 INT = @Month1 + 1;
    DECLARE @Month3 INT = @Month1 + 2;

    if (@UserID is null and @DepartmentCode is null and @TeamID is null)
    BEGIN
        SELECT
            @Year AS [Year],
            @Quarter AS [Quarter],
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,

            /* -------- average completion time in hours.decimal -------- */
        /* month 1 */
        ISNULL( CAST(
                SUM( CASE WHEN MONTH(DateAccepted) = @Month1 AND YEAR(DateAccepted) = @Year
                                THEN 1.0 * DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month1 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0            -- minutes → hours
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth1 ,

        /* month 2 */
        ISNULL( CAST(
                SUM( CASE WHEN MONTH(DateAccepted) = @Month2 AND YEAR(DateAccepted) = @Year
                                THEN 1.0 * DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0.0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month2 AND YEAR(DateAccepted) = @Year
                                   THEN 1.0 ELSE 0.0 END ), 0.00 )
              / 60.0
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth2 ,

        /* month 3 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month3 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month3 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth3

        FROM LKACSoft_Task T
        LEFT JOIN LKACSoft_TaskStatus TS ON T.TaskStatusID = TS.TaskStatusID
        WHERE 
            T.DateAccepted IS NOT NULL
            AND T.DateCompleted IS NOT NULL
            AND (TS.TaskStatusName = N'Đã hoàn thành' OR TS.TaskStatusName = N'Xác nhận hoàn thành');

        return;
    END


    if (@UserID is not null and @DepartmentCode is null and @TeamID is null)
    BEGIN
        SELECT
            @Year AS [Year],
            @Quarter AS [Quarter],
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,

            /* -------- average completion time in hours.decimal -------- */
        /* month 1 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month1 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month1 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0            -- minutes → hours
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth1 ,

        /* month 2 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month2 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month2 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth2 ,

        /* month 3 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month3 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month3 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth3

        FROM LKACSoft_Task T
        LEFT JOIN LKACSoft_TaskStatus TS ON T.TaskStatusID = TS.TaskStatusID
        WHERE T.AssignedTo = @UserID
            AND T.DateAccepted IS NOT NULL
            AND T.DateCompleted IS NOT NULL
            AND (TS.TaskStatusName = N'Đã hoàn thành' OR TS.TaskStatusName = N'Xác nhận hoàn thành');

        return;
    END


    if (@UserID is null and @DepartmentCode is null and @TeamID is not null)
    BEGIN
        SELECT
            @Year AS [Year],
            @Quarter AS [Quarter],
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,

            /* -------- average completion time in hours.decimal -------- */
        /* month 1 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month1 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month1 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0            -- minutes → hours
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth1 ,

        /* month 2 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month2 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month2 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth2 ,

        /* month 3 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month3 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month3 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth3

        From LKACSoft_Task T
            left join LKACSoft_User
                on T.AssignedTo = ID
            Left join LKACSoft_AccountantTeam
                on Team = TeamID
            left join LKACSoft_TaskStatus TS ON T.TaskStatusID = TS.TaskStatusID
        WHERE Team = @TeamID
            AND T.DateAccepted IS NOT NULL
            AND T.DateCompleted IS NOT NULL
            AND (TS.TaskStatusName = N'Đã hoàn thành' OR TS.TaskStatusName = N'Xác nhận hoàn thành');

        return;
    END

    if (@UserID is null and @DepartmentCode is not null and @TeamID is null)
    BEGIN
        SELECT
            @Year AS [Year],
            @Quarter AS [Quarter],
            DateName( month , DateAdd( month , @Month1 , 0 ) - 1 ) as Month1,
            DateName( month , DateAdd( month , @Month2 , 0 ) - 1 ) as Month2,
            DateName( month , DateAdd( month , @Month3 , 0 ) - 1 ) as Month3,

            /* -------- average completion time in hours.decimal -------- */
        /* month 1 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month1 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month1 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0            -- minutes → hours
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth1 ,

        /* month 2 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month2 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month2 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth2 ,

        /* month 3 */
        ISNULL( CAST(
                1.0 * SUM( CASE WHEN MONTH(DateAccepted) = @Month3 AND YEAR(DateAccepted) = @Year
                                THEN DATEDIFF(MINUTE, DateAccepted, DateCompleted)
                                ELSE 0
                          END )
              / NULLIF( SUM( CASE WHEN MONTH(DateAccepted) = @Month3 AND YEAR(DateAccepted) = @Year
                                   THEN 1 ELSE 0 END ), 0 )
              / 60.0
              AS Float ), 0.00 ) AS AvgCompletionTimeInHoursMonth3

        From LKACSoft_Task T
            left join LKACSoft_User
                on AssignedTo = ID
            Left join LKACSoft_UserPosition UP
                on ID = UP.UserID
            Left join LKACSoft_RolePosition RP
                on UP.RoleID = RP.RoleID
            Left join LKACSoft_Department
                on RP.LKACSoft_DepartmentCode = Code
            left join LKACSoft_TaskStatus TS
                on T.TaskStatusID = TS.TaskStatusID
        WHERE Code = @DepartmentCode
            AND T.DateAccepted IS NOT NULL
            AND T.DateCompleted IS NOT NULL
            AND (TS.TaskStatusName = N'Đã hoàn thành' OR TS.TaskStatusName = N'Xác nhận hoàn thành');

        return;
    END

    return;
END
go

exec sp_TaskAverageCompletionTimePerQuarter 2, 2025, null, null , NULL
go

Create or Alter Proc sp_GetAll_LKACSoft_Task
@UserID VARCHAR(255),
@isLeader BIT,
@IsManager BIT
as
BEGIN
    if (@IsManager = 1)
    BEGIN
        Select *
        From LKACSoft_Task

        return
    END

    if (@isLeader = 1)
    BEGIN
        Select *
        From LKACSoft_Task
        Where AssignedTo in (Select ID
                            From LKACSoft_User
                            Where Team = (select top 1 Team from LKACSoft_User where ID = @UserID))

        return
    END

    Select *
    From LKACSoft_Task
    Where AssignedTo = @UserID
END
go

Create or Alter Proc sp_GetAll_LKACSoft_Task
@UserID VARCHAR(255),
@isLeader BIT,
@IsManager BIT
as
BEGIN
    if (@IsManager = 1)
    BEGIN
        Select *
        From LKACSoft_Task

        return
    END

    if (@isLeader = 1)
    BEGIN
        Select *
        From LKACSoft_Task
        Where AssignedTo in (Select ID
                            From LKACSoft_User
                            Where Team = (select top 1 Team from LKACSoft_User where ID = @UserID))

        return
    END

    Select *
    From LKACSoft_Task
    Where AssignedTo = @UserID
END
go

Create or Alter Proc sp_GetByID_LKACSoft_Task
@TaskID VARCHAR(255),
@UserID VARCHAR(255),
@isLeader BIT,
@IsManager BIT
as
BEGIN
    if (@IsManager = 1)
    BEGIN
        Select *
        From LKACSoft_Task
        Where TaskID = @TaskID
        return
    END

    if (@isLeader = 1)
    BEGIN
        Select *
        From LKACSoft_Task
        Where AssignedTo in (Select ID
                            From LKACSoft_User
                            Where Team = (select top 1 Team from LKACSoft_User where ID = @UserID))
            And TaskID = @TaskID

        return
    END

    Select *
    From LKACSoft_Task
    Where AssignedTo = @UserID
        And TaskID = @TaskID
END
go

-- declare @NewTaskID                  VARCHAR(255);
-- declare @ResponseMessage            VARCHAR(255);

-- exec sp_Insert_LKACSoft_Task null, 'U006', 'dfsdfsf', null, null, null, null, null, null, @NewTaskID output, @ResponseMessage output
-- print @ResponseMessage
-- go

-- select *
-- from LKACSoft_Notification
-- go

-- select *
-- from LKACSoft_UserNotification
-- go

-- select *
-- from LKACSoft_User
-- go

-- delete LKACSoft_UserNotification
-- delete LKACSoft_Notification
-- go

Create or Alter Proc sp_Insert_LKACSoft_Task
--@DateAssigned               DATE,
@TaskDeadline               DATE = NULL,
@AssignedTo                 VARCHAR(255) = NULL,
@Title                      NVARCHAR(255) = NULL,
--@Detail                     NVARCHAR(200),
@TaskStatusID               VARCHAR(255) = NULL,
--@TaskStatusId               VARCHAR(255),
--@DateAccepted               DATE,
--@DateCompleted              DATE,
--@ReviewedBy                 VARCHAR(255),
--@ReviewNote                 NVARCHAR(2000),
--@DateReview                 DATE,
--@IsRetried                  BIT,
@RelatedToExecution           VARCHAR(255) = NULL,
@TaskType                   VARCHAR(255) = NULL,
--@DesignatedNumberOfDocument INT,
--@NumberOfCompletedDocument  INT,
@Priority                   VARCHAR(255) = NULL,
@isLeader                   BIT,
@UserID                     VARCHAR(255),
@NewTaskID                  VARCHAR(255) OUTPUT,
@ResponseMessage            VARCHAR(255) OUTPUT
as
Begin

    IF @Title IS NULL
    BEGIN
        set @ResponseMessage = 'Task Title should not be null.';
        RETURN;
    END

     -- Check for null values in all parameters
    IF @TaskDeadline IS NULL AND @AssignedTo IS NULL AND
       @TaskStatusID IS NULL AND @RelatedToExecution IS NULL 
       AND @TaskType IS NULL AND @Priority IS NULL
    BEGIN
        set @ResponseMessage = 'Atleast one parameter should be passed.';
        RETURN;
    END

    -- @TaskDeadline
    IF @TaskDeadline = ''
    BEGIN
        SET @TaskDeadline = NULL;
    END

    -- @AssignedTo
    IF @AssignedTo = ''
    BEGIN
        SET @AssignedTo = NULL;
    END

    -- @Title
    IF @Title = ''
    BEGIN
        SET @Title = NULL;
    END

    -- @TaskStatusID
    IF @TaskStatusID = ''
    BEGIN
        SET @TaskStatusID = NULL;
    END

    -- @RelatedToExecution
    IF @RelatedToExecution = ''
    BEGIN
        SET @RelatedToExecution = NULL;
    END

    -- @TaskType
    IF @TaskType = ''
    BEGIN
        SET @TaskType = NULL;
    END

    -- @Priority
    IF @priority = ''
    BEGIN
        SET @Priority = NULL;
    END

    if (@isLeader = 1)
    BEGIN
        IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_User
        WHERE (ID = @AssignedTo
                and (Team = (select top 1 Team from LKACSoft_User where ID = @UserID)))
            or @AssignedTo is null
        )
        BEGIN
            set @ResponseMessage = 'This Assigned User is not exist';
            RETURN;
        END
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_TaskStatus
        WHERE TaskStatusID = @TaskStatusID
            or @TaskStatusID is null
    )
    BEGIN
        set @ResponseMessage = 'This TaskStatus is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_Execution
        WHERE ExecutionID = @RelatedToExecution
            or @RelatedToExecution is null
    )
    BEGIN
        set @ResponseMessage = 'This Execution is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_TaskType
        WHERE TaskTypeID = @TaskType
            or @TaskType is null
    )
    BEGIN
        set @ResponseMessage = 'This TaskType is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_Priority
        WHERE PriorityID = @Priority 
            or @Priority is null
    )
    BEGIN
        set @ResponseMessage = 'This Priority is not exist';
        RETURN;
    END

    
    DECLARE @NewID VARCHAR(255);
    DECLARE @LastID VARCHAR(255);
    DECLARE @NextID INT;

    -- Step 1: Get the latest ID_CANVA
    SELECT @LastID = MAX(TaskID) FROM LKACSoft_Task;

    -- Step 2: If there is no existing ID, start with "C001"
    IF @LastID IS NULL
    BEGIN
        SET @NewID = 'TK001';
    END
    ELSE
    BEGIN
        -- Step 3: Extract the numeric part, increment it, and format it with leading zeros
        SET @NextID = CAST(SUBSTRING(@LastID, 3, 3) AS INT) + 1;
        SET @NewID = 'TK' + RIGHT('000' + CAST(@NextID AS VARCHAR), 3);
    END

    -- Step 4: Insert the new record with the generated ID
    INSERT INTO dbo.LKACSoft_Task
    (
        TaskID,
        DateAssigned,
        TaskDeadline,
        AssignedTo,
        Title,
        --Detail,
        TaskStatusID,
        --TaskStatusId,
        --DateAccepted,
        --DateCompleted,
        --ReviewedBy,
        --ReviewNote,
        --DateReview,
        IsRetried,
        RelatedToExecution,
        TaskType,
        --DesignatedNumberOfDocument,
        --NumberOfCompletedDocument,
        Priority,
        CreatedAt
    )
    VALUES (@NewID,
            GetDate(),
            @TaskDeadline,
            @AssignedTo,
            @Title,
            --@Detail,
            @TaskStatusID,
            --@TaskStatusId,
            --@DateAccepted,
            --@DateCompleted,
            --@ReviewedBy,
            --@ReviewNote,
            --@DateReview,
            0,
            @RelatedToExecution,
            @TaskType,
            --@DesignatedNumberOfDocument,
            --@NumberOfCompletedDocument,
            @Priority,
            GETDATE()
        );

    -- Update DateAssigned

    IF @TaskStatusID = '' or @TaskStatusID is null
    BEGIN
        UPDATE LKACSoft_Task
        SET TaskStatusID = 'TS02'
        WHERE TaskID = @NewID;
    END
    
    IF @AssignedTo != '' and @AssignedTo is not null 
    BEGIN
        UPDATE LKACSoft_Task
        SET DateAssigned = GETDATE()
        WHERE TaskID = @NewID;

        declare @DetailForNoti NVARCHAR(255);

        set @DetailForNoti = N'Tác vụ ' + @Title + N' vừa được tạo.'

        exec sp_Insert_LKACSoft_Notification_TasKInsert @DetailForNoti, @UserID, @NewID
    END

    Set @NewTaskID = @NewID
END
GO

-- select *
-- from LKACSoft_User

CREATE Or Alter PROCEDURE sp_Delete_LKACSoft_Task
@TaskID VARCHAR(255),
@isLeader BIT,
@UserID VARCHAR(255)

AS
BEGIN
    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_Task WHERE TaskID = @TaskID)
    BEGIN
        if (@isLeader = 1)
        BEGIN
            if not exists (
                Select 1
                From LKACSoft_Task
                Where AssignedTo in (Select ID
                                    From LKACSoft_User
                                    Where Team = (select top 1 Team from LKACSoft_User where ID = @UserID))
                    And TaskID = @TaskID)
            BEGIN
                print 'Cant delete task that does not belong to user in team';
                return
            END
        END
        
        -- Delete the record
        DELETE FROM LKACSoft_Task WHERE TaskID = @TaskID;
    END
END
GO

-- declare @ResponseMessage            VARCHAR(255);

-- exec sp_Update_LKACSoft_Task 'TK001', null, null, null, null, 'dasdasdasd', null, null, null, null, null, null, null, null, null, null, null, null, null, @ResponseMessage output

-- print @ResponseMessage
-- go


Create or Alter Proc sp_Update_LKACSoft_Task
@TaskID                     VARCHAR(255),
--@DateAssigned               DATE = NULL,
@TaskDeadline               DATE = NULL,
@AssignedTo                 VARCHAR(255) = NULL,
@Title                      NVARCHAR(255) = NULL,
@Detail                     NVARCHAR(200) = NULL,
@TaskStatusID               VARCHAR(255) = NULL,
@DateAccepted               DATE = NULL,
@DateCompleted              DATE = NULL,
@ReviewedBy                 VARCHAR(255) = NULL,
@ReviewNote                 NVARCHAR(2000) = NULL,
@DateReview                 DATE = NULL,
@IsRetried                  BIT = NULL,
@RelatedToExecution         VARCHAR(255) = NULL,
@TaskType                   VARCHAR(255) = NULL,
@DesignatedNumberOfDocument INT = NULL,
@NumberOfCompletedDocument  INT = NULL,
@Priority                   VARCHAR(255) = NULL,
@isLeader                   BIT,
@UserID                     VARCHAR(255),
@ResponseMessage            VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @TaskID IS NULL
    BEGIN
        Set @ResponseMessage = 'TaskID cannot be NULL.';
        RETURN;
    END

    if (@isLeader = 1)
    BEGIN
        IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_User
        WHERE (ID = @AssignedTo
                and (Team = (select top 1 Team from LKACSoft_User where ID = @UserID)))
            or @AssignedTo is null
            or @AssignedTo = ''
        )
        BEGIN
            set @ResponseMessage = 'This Assigned User is not exist';
            RETURN;
        END
    END
    else
    Begin
        IF NOT EXISTS (
            SELECT 1 
            FROM LKACSoft_User
            WHERE ID = @AssignedTo
                or @AssignedTo is null
                or @AssignedTo = ''
            )
            BEGIN
                set @ResponseMessage = 'This Assigned User is not exist';
                RETURN;
            END
    End

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_TaskStatus
        WHERE TaskStatusID = @TaskStatusID
            or @TaskStatusID is null
            or @TaskStatusID = ''
    )
    BEGIN
        set @ResponseMessage = 'This TaskStatus is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_User
        WHERE ID = @ReviewedBy
            or @ReviewedBy is null
            or @ReviewedBy = ''
    )
    BEGIN
        set @ResponseMessage = 'This User is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_Execution
        WHERE ExecutionID = @RelatedToExecution
            or @RelatedToExecution is null
            or @RelatedToExecution = ''
    )
    BEGIN
        set @ResponseMessage = 'This Execution is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_TaskType
        WHERE TaskTypeID = @TaskType
            or @TaskType is null
            or @TaskType = ''
    )
    BEGIN
        set @ResponseMessage = 'This TaskType is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_Priority
        WHERE PriorityID = @Priority 
            or @Priority is null
            or @Priority = ''
    )
    BEGIN
        set @ResponseMessage = 'This Priority is not exist';
        RETURN;
    END

    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_Task WHERE TaskID = @TaskID)
    BEGIN

        -- Update DateAssigned
        IF @AssignedTo != '' and @AssignedTo is not null 
        BEGIN
            UPDATE LKACSoft_Task
            SET DateAssigned = GETDATE()
            WHERE TaskID = @TaskID;
        END

        -- Update TaskDeadline
        IF @TaskDeadline = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET TaskDeadline = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @TaskDeadline IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET TaskDeadline = @TaskDeadline
            WHERE TaskID = @TaskID;
        END

        -- Update AssignedTo
        IF @AssignedTo = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET AssignedTo = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @AssignedTo IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET AssignedTo = @AssignedTo
            WHERE TaskID = @TaskID;
        END

        -- Update Title
        IF @Title = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET Title = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @Title IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET Title = @Title
            WHERE TaskID = @TaskID;
        END

        -- Update Detail
        IF @Detail = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET Detail = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @Detail IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET Detail = @Detail
            WHERE TaskID = @TaskID;
        END

        -- Update TaskStatusID
        IF @TaskStatusID = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET TaskStatusID = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @TaskStatusID IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET TaskStatusID = @TaskStatusID
            WHERE TaskID = @TaskID;
        END

        -- Update DateAccepted
        IF @DateAccepted = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET DateAccepted = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @DateAccepted IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET DateAccepted = @DateAccepted
            WHERE TaskID = @TaskID;
        END

        -- Update DateCompleted
        IF @DateCompleted = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET DateCompleted = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @DateCompleted IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET DateCompleted = @DateCompleted
            WHERE TaskID = @TaskID;
        END

        -- Update ReviewedBy
        IF @ReviewedBy = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET ReviewedBy = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @ReviewedBy IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET ReviewedBy = @ReviewedBy
            WHERE TaskID = @TaskID;
        END

        -- Update ReviewNote
        IF @ReviewNote = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET ReviewNote = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @ReviewNote IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET ReviewNote = @ReviewNote
            WHERE TaskID = @TaskID;
        END

        -- Update DateReview
        IF @DateReview = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET DateReview = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @DateReview IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET DateReview = @DateReview
            WHERE TaskID = @TaskID;
        END

        -- Update IsRetried
        IF @IsRetried IS NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET IsRetried = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @IsRetried IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET IsRetried = @IsRetried
            WHERE TaskID = @TaskID;
        END

        -- Update RelatedToExecution
        IF @RelatedToExecution = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET RelatedToExecution = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @RelatedToExecution IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET RelatedToExecution = @RelatedToExecution
            WHERE TaskID = @TaskID;
        END

        -- Update TaskType
        IF @TaskType = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET TaskType = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @TaskType IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET TaskType = @TaskType
            WHERE TaskID = @TaskID;
        END

        -- Update DesignatedNumberOfDocument
        IF @DesignatedNumberOfDocument IS NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET DesignatedNumberOfDocument = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @DesignatedNumberOfDocument IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET DesignatedNumberOfDocument = @DesignatedNumberOfDocument
            WHERE TaskID = @TaskID;
        END

        -- Update NumberOfCompletedDocument
        IF @NumberOfCompletedDocument IS NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET NumberOfCompletedDocument = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @NumberOfCompletedDocument IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET NumberOfCompletedDocument = @NumberOfCompletedDocument
            WHERE TaskID = @TaskID;
        END

        -- Update Priority
        IF @Priority = ''
        BEGIN
            UPDATE LKACSoft_Task
            SET Priority = NULL
            WHERE TaskID = @TaskID;
        END
        ELSE IF @Priority IS NOT NULL
        BEGIN
            UPDATE LKACSoft_Task
            SET Priority = @Priority
            WHERE TaskID = @TaskID;
        END

        -- Update the existing record
        -- UPDATE LKACSoft_Task
        -- SET 
        --     DateAssigned = @DateAssigned,
        --     TaskDeadline = @TaskDeadline,
        --     DateAccepted = @DateAccepted,
        --     DateCompleted = @DateCompleted,
        --     DateReview = @DateReview,
        --     AssignedTo = @AssignedTo,
        --     Title = @Title,
        --     Detail = @Detail,
        --     TaskStatusID = @TaskStatusID,
        --     ReviewedBy = @ReviewedBy,
        --     ReviewNote = @ReviewNote,
        --     IsRetried = @IsRetried,
        --     RelatedToExecution = @RelatedToExecution,
        --     TaskType = @TaskType,
        --     DesignatedNumberOfDocument = @DesignatedNumberOfDocument,
        --     NumberOfCompletedDocument = @NumberOfCompletedDocument,
        --     Priority = @Priority
        -- WHERE TaskID = @TaskID;

        print 'Task updated successfully.';

        IF @AssignedTo != '' and @AssignedTo is not null 
        BEGIN

            declare @DetailForNoti NVARCHAR(255);

            set @DetailForNoti = N'Tác vụ ' + @Title + N' vừa được cập nhật.'

            exec sp_Insert_LKACSoft_Notification_TasKInsert @DetailForNoti, @UserID, @TaskID
        END
    END
    ELSE
    BEGIN
        set @ResponseMessage = 'Task ID not found.';
    END
END
GO


Create or Alter Proc sp_Update_LKACSoft_Task_TaskStatus
@TaskID                     VARCHAR(255),
--@DateAssigned               DATE,
--@TaskDeadline               DATE,
--@AssignedTo                 VARCHAR(255),
--@Title                      NVARCHAR(255),
--@Detail                     NVARCHAR(200),
--@DateAccepted               DATE,
--@DateCompleted              DATE,
--@ReviewedBy                 VARCHAR(255),
--@ReviewNote                 NVARCHAR(2000),
--@DateReview                 DATE,
--@IsRetried                  BIT,
--@RelatedToProcess           VARCHAR(255),
--@TaskType                   VARCHAR(255),
--@DocumentType               VARCHAR(255),
--@DesignatedNumberOfDocument INT,
--@NumberOfCompletedDocument  INT
@TaskStatusID      VARCHAR(255),
@UserID            VARCHAR(255),
@IsManager         BIT,
@ResponseMessage   VARCHAR(255) OUTPUT

as
Begin

    -- Not manager, not allowed
    IF (@IsManager = 0 and (@TaskStatusID = 'TS06' or @TaskStatusID = 'TS05'))
        BEGIN
            SET @ResponseMessage = 'Staff can not change to this task status, only Manager allowed';
            return
        END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_TaskStatus
        WHERE TaskStatusID = @TaskStatusID
    )
    BEGIN
        set @ResponseMessage = 'This TaskStatus is not exist';
        RETURN;
    END

    -- Check if the record exists
    -- If TaskStatus already in status 'Xoa' or 'Xac Nhan hoan thanh', can not change it anymore
    IF EXISTS (SELECT 1 FROM LKACSoft_Task WHERE TaskID = @TaskID AND (@TaskStatusID != 'TS05'))
    BEGIN

        -- Update the existing record
        UPDATE LKACSoft_Task
        SET 
            TaskStatusID = @TaskStatusID
        WHERE TaskID = @TaskID;


        declare @RelatedToExecution  VARCHAR(255);
        declare @TaskType  VARCHAR(255);
        declare @TaskStatus  VARCHAR(255);

        SELECT 
            @RelatedToExecution = RelatedToExecution, 
            @TaskType = TaskType, 
            @TaskStatus = TaskStatusID
        FROM LKACSoft_Task
        WHERE TaskID = @TaskID;

        -------PS_ID for the upcomming Updated Task


        declare @UpdatedProcessSchemaStatusID VARCHAR(255);

        Select @UpdatedProcessSchemaStatusID = AssociatedProcessSchemaStatus
        From LKACSoft_TaskTypeStatus
        Where TaskTypeID = @TaskType
            And TaskStatusID = @TaskStatus


        declare @UpdatedINDEXProcessSchemaStatusID INT;

        SET @UpdatedINDEXProcessSchemaStatusID = CAST(SUBSTRING(@UpdatedProcessSchemaStatusID, 4, 2) AS INT);

        -----PS_ID for the actual Process 

        declare @CurrProcessSchemaStatusID VARCHAR(255);

        select @CurrProcessSchemaStatusID = ProcessSchemaStatus
        from LKACSoft_Execution
        where ExecutionID = @RelatedToExecution

        declare @CurrINDEXProcessSchemaStatusID INT;

        SET @CurrINDEXProcessSchemaStatusID = CAST(SUBSTRING(@CurrProcessSchemaStatusID, 4, 2) AS INT);



        IF (@UpdatedINDEXProcessSchemaStatusID > @CurrINDEXProcessSchemaStatusID)
        BEGIN
            IF NOT EXISTS (
                SELECT 1
                From LKACSoft_Task
                WHERE TaskType = @TaskType
                
                    -- AND
                    
                    -- ((TaskType = 'TT01'
                    --     AND TaskID != 'TS00'
                    --     AND TaskID != 'TS01')
                    -- OR 
                    -- (TaskID != 'TS00'
                    -- AND TaskID != 'TS01'
                    -- AND TaskID != 'TS02'
                    -- ))

                    AND RelatedToExecution = @RelatedToExecution
                    AND TaskStatusID != @TaskStatus
                    AND TaskID != @TaskID)
            BEGIN

                -- declare @ProcessStatus VARCHAR(255);

                -- select @ProcessStatus = AssociatedProcessStatus 
                -- from LKACSoft_TaskTypeStatus 
                -- where TaskStatusID = @TaskStatus 
                --     and TaskTypeID = @TaskType

                Update LKACSoft_Execution
                Set ProcessSchemaStatus = @UpdatedProcessSchemaStatusID
                Where ExecutionID = @RelatedToExecution


                ---------------------Update Task based on Task

                IF NOT EXISTS ( Select 1
                                From LKACSoft_Task
                                Where TaskType = @TaskType
                                    AND TaskStatusID != 'TS05'
                                    AND RelatedToExecution = @RelatedToExecution)
                BEGIN
                    declare @NextID INT;

                
                    SET @NextID = CAST(SUBSTRING(@TaskType, 3, 2) AS INT) + 1;

                    IF (@NextID < 5 and @NextID > 0)
                    BEGIN
                        declare @NextTaskTypeID VARCHAR(255);  

                        SET @NextTaskTypeID = 'TT' + RIGHT('00' + CAST(@NextID AS VARCHAR), 2);


                        Update LKACSoft_Task
                        SET
                            TaskStatusID = (select TaskStatusID
                                            from LKACSoft_TaskTypeStatus
                                            where AssociatedProcessSchemaStatus = @UpdatedProcessSchemaStatusID
                                                And TaskTypeID = @NextTaskTypeID)
                        WHERE RelatedToExecution = @RelatedToExecution
                            AND TaskType = @NextTaskTypeID;
                    END
                END
                
                declare @ExecutionName NVARCHAR(255);
                declare @ProcessStatusName NVARCHAR(255);
                declare @DetailForNoti NVARCHAR(255);

                select @ExecutionName = ExecutionName
                From LKACSoft_Execution
                Where ExecutionID = @RelatedToExecution

                select @ProcessStatusName = PS.StatusName
                From LKACSoft_ProcessSchemaStatus PSS
                Left Join LKACSoft_ProcessStatus PS
                    on PSS.ProcessStatus = PS.ProcessStatusID
                Where PSS.ProcessSchemaStatusID = @UpdatedProcessSchemaStatusID

                set @DetailForNoti = N'Chứng từ ' + @ExecutionName + N' ' + @ProcessStatusName

                exec sp_Insert_LKACSoft_Notification_TasKStatusUpdate @DetailForNoti, @UserID, @RelatedToExecution

            END
        END

        Update LKACSoft_Task
        Set DateAccepted = GetDate()
        Where TaskID = @TaskID
            and TaskStatusID = (Select top 1 TaskStatusID From LKACSoft_TaskStatus Where TaskStatusName = N'Đang thực hiện');

        Update LKACSoft_Task
        Set DateCompleted = GetDate()
        Where TaskID = @TaskID
            and TaskStatusID = (Select top 1 TaskStatusID From LKACSoft_TaskStatus Where TaskStatusName = N'Đã hoàn thành');

        Update LKACSoft_Task
        Set IsRetried = 1
        Where TaskID = @TaskID
            and TaskStatusID = (Select top 1 TaskStatusID From LKACSoft_TaskStatus Where TaskStatusName = N'Làm lại');

        PRINT 'Taskstatus updated successfully.';

        return
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'Task ID not found.';
        return
    END
END
GO

-- declare @response varchar(255);

-- exec sp_Update_LKACSoft_Task_TaskStatus 'TK012', 'TS03', 'U004', 1, @response output

-- print @response
-- go




Create or Alter Proc sp_GetbyLatestCreatedID_LKACSoft_Task
as
begin
    SELECT TOP 1 *
    FROM LKACSoft_Task
    ORDER BY CAST(SUBSTRING(TaskID, 3, LEN(TaskID) - 2) AS INT) DESC;
end

-- exec sp_Update_LKACSoft_Task "TK038", "2026-02-05T00:00:00", null, null, null, null, null, null, null, null, null, "2026-02-05T00:00:00", null, null, null, null, null, 1
-- go

-- Create or Alter Proc sp_FilterBy_LKACSoft_Task
-- @option INT
-- as
-- BEGIN

--     If exists (select 1 from @option = 1)
--     --group by TaskStatus
--         Select *
--         From LKACSoft_Task
--         Where TaskID = @TaskID
-- END
-- go

-- exec sp_Update_LKACSoft_Task 'TK003', '2023-03-06', '2023-03-15', 'U008',
--  N'Kế toán nhập liệu Q1/2023',
--  N'Nhập chi tiết sổ sách cho Cty XYZ',
--  'TS01', '2023-03-07', NULL, 'U007',
--  N'Chưa hoàn thành', NULL, 0,
--  'P002', 'TT03', 'DT002', 20, 11;