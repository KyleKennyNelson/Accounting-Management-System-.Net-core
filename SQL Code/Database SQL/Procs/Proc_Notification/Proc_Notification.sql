Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_Notification
@UserID VARCHAR(255) = NULL
as
BEGIN
    Select N.*
    From LKACSoft_Notification N
    Left Join LKACSoft_UserNotification UN
        on N.NotificationID = UN.NotificationID
    Where UN.UserID = @UserID
END
go

Create or Alter Proc sp_GetByID_LKACSoft_Notification
@UserID VARCHAR(255) = NULL
--@NotificationID VARCHAR(255)
as
BEGIN
    Declare @newestNoti varchar(255);

    select top 1 @newestNoti = NotificationID
    from LKACSoft_Notification
    order by DateCreated DESC
    --print @newestNoti

    Select N.*
    From LKACSoft_Notification N
    Left Join LKACSoft_UserNotification UN
        on N.NotificationID = UN.NotificationID
    Where UN.UserID = @UserID
        And N.NotificationID = @newestNoti
END
go

exec sp_GetByID_LKACSoft_Notification 'U006'
go


-- declare @NewCode                VARCHAR(255);
-- declare @ResponseMessage            VARCHAR(255);

-- exec sp_Insert_LKACSoft_JobTaskFile null, "U008", null, null, null, null, null, null, null, null, null, null, null, @NewCode output, @ResponseMessage output
-- go

Create or Alter Proc sp_Insert_LKACSoft_Notification_TasKInsert
@Detail            NVARCHAR(255) = NULL,
@UserCreate        VARCHAR(255)  = NULL,
@TaskIDRelated     VARCHAR(255)  = NULL
as
Begin

    if (@TaskIDRelated is null or @TaskIDRelated = '')
    BEGIN
        return
    END

    DECLARE @NewID VARCHAR(255);
    DECLARE @LastID VARCHAR(255);
    DECLARE @NextID INT;

    -- Get the latest Code (FileID)
    SELECT @LastID = MAX(NotificationID) FROM LKACSoft_Notification;

    IF @LastID IS NULL
    BEGIN
        SET @NewID = 'N001';
    END
    ELSE
    BEGIN
        SET @NextID = CAST(SUBSTRING(@LastID, 2, 3) AS INT) + 1;
        SET @NewID = 'N' + RIGHT('000' + CAST(@NextID AS VARCHAR), 3);
    END

    -- Insert the new Notification record
    INSERT INTO dbo.LKACSoft_Notification
    (
        NotificationID,
        Detail,
        UserCreate,
        DateCreated
    )
    VALUES
    (
        @NewID,
        @Detail,
        @UserCreate,
        GETDATE()
    );

    -- Insert the new Notification record

    INSERT INTO dbo.LKACSoft_UserNotification
    (
        UserID,
        NotificationID
    )
    Values
    (
        (Select AssignedTo from LKACSoft_Task where TaskID = @TaskIDRelated),
        @NewID
    );

    INSERT INTO dbo.LKACSoft_UserNotification
    (
        UserID,
        NotificationID
    )
    Select U.ID, @NewID
    From LKACSoft_User U
    Left join LKACSoft_UserPosition UP
        on U.ID = UP.UserID
    Left join LKACSoft_RolePosition RP
        on UP.RoleID = RP.RoleID
    where RP.LKACSoft_PositionCode = 'TP'
        and U.ID not in (select UserID from LKACSoft_UserNotification where NotificationID = @NewID)
        and RP.LKACSoft_DepartmentCode In (Select RP.LKACSoft_DepartmentCode
                                            From LKACSoft_Task T
                                            Left Join LKACSoft_User U
                                                on T.AssignedTo = U.ID
                                            Left join LKACSoft_UserPosition UP
                                                on U.ID = UP.UserID
                                            Left join LKACSoft_RolePosition RP
                                                on UP.RoleID = RP.RoleID
                                            Where T.TaskID = @TaskIDRelated);


    INSERT INTO dbo.LKACSoft_UserNotification
    (
        UserID,
        NotificationID
    )
    Select U.ID, @NewID
    From LKACSoft_AccountantTeam ACT
    Left join LKACSoft_User U
        on ACT.TeamLeader = U.ID
    where U.Team = (Select TOP 1 Team
                    From LKACSoft_User, LKACSoft_Task
                    Where TaskID = @TaskIDRelated
                        and ID = AssignedTo)
        and U.ID not in (select UserID from LKACSoft_UserNotification where NotificationID = @NewID)

    return;
END
GO

-- exec sp_Insert_LKACSoft_Notification_TasKStatusUpdate 'dasdsa', 'U004', 'E003'
-- go


Create or Alter Proc sp_Insert_LKACSoft_Notification_TasKStatusUpdate
@Detail            NVARCHAR(255) = NULL,
@UserCreate        VARCHAR(255)  = NULL,
@ExecutionIDRelated     VARCHAR(255)  = NULL
as
Begin

    DECLARE @NewID VARCHAR(255);
    DECLARE @LastID VARCHAR(255);
    DECLARE @NextID INT;

    -- Get the latest Code (FileID)
    SELECT @LastID = MAX(NotificationID) FROM LKACSoft_Notification;

    IF @LastID IS NULL
    BEGIN
        SET @NewID = 'N001';
    END
    ELSE
    BEGIN
        SET @NextID = CAST(SUBSTRING(@LastID, 2, 3) AS INT) + 1;
        SET @NewID = 'N' + RIGHT('000' + CAST(@NextID AS VARCHAR), 3);
    END

    -- Insert the new Notification record
    INSERT INTO dbo.LKACSoft_Notification
    (
        NotificationID,
        Detail,
        UserCreate,
        DateCreated
    )
    VALUES
    (
        @NewID,
        @Detail,
        @UserCreate,
        GETDATE()
    );

    -- Insert the new Notification record
    INSERT INTO dbo.LKACSoft_UserNotification
    (
        UserID,
        NotificationID
    )
    Select distinct U.ID, @NewID
    From LKACSoft_User U
    Left join LKACSoft_Task T
        on U.ID = T.AssignedTo
    where T.RelatedToExecution = @ExecutionIDRelated
        and U.ID not in (select UserID from LKACSoft_UserNotification where NotificationID = @NewID)
END
GO