Use db_ab6e43_lkacsoftdb
go

-- select *
-- from LKACSoft_User
-- go

------- Load Team List
Create or Alter Proc sp_GetAll_LKACSoft_AccountantTeam
as
BEGIN
    Select *
    From LKACSoft_AccountantTeam
END
go

Create or Alter Proc sp_GetByID_LKACSoft_AccountantTeam
@TeamID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_AccountantTeam
    Where TeamID = @TeamID
END
go


-- -- drop proc sp_GetUser_ByTeamID_LKACSoft_User
-- -- go

-- Create or Alter Proc sp_GetUser_ByTeamID_LKACSoft_User
-- @TeamID VARCHAR(255)
-- as
-- BEGIN
--     Select *
--     From LKACSoft_User
--     Where Team = @TeamID
-- END
-- go


Create or Alter Proc sp_Update_LKACSoft_AccountantTeam
@TeamID                     VARCHAR(255),
@LeaderID                     VARCHAR(255) = NULL,
@ResponseMessage            VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @TeamID IS NULL or @TeamID = ''
    BEGIN
        Set @ResponseMessage = 'TeamID cannot be NULL.';
        RETURN;
    END

    -- @UserID
    IF @LeaderID = ''
    BEGIN
        SET @LeaderID = NULL;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_User
        WHERE Team = @TeamID
            and ID = @LeaderID
            or @LeaderID is null
    )
    BEGIN
        set @ResponseMessage = 'This Assigned User may not exist in this Team';
        RETURN;
    END

    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_AccountantTeam WHERE TeamID = @TeamID)
    BEGIN
        
        -- Fetch existing values and apply ISNULL logic to prevent overwriting with nulls
        SELECT 
            @LeaderID = ISNULL(@LeaderID, ID)
        FROM LKACSoft_User
        WHERE Team = @TeamID;

        -- Update the existing record
        UPDATE LKACSoft_AccountantTeam
        SET 
            TeamLeader = @LeaderID
        WHERE TeamID = @TeamID;

        print 'Assign Leader to this team successfully';
    END
    ELSE
    BEGIN
        set @ResponseMessage = 'Team ID not found.';
    END
END
GO

-- declare @ResponseMessage            VARCHAR(255);

-- exec sp_Update_LKACSoft_AccountantTeam 'T001', 'U010', @ResponseMessage output

-- print @ResponseMessage;