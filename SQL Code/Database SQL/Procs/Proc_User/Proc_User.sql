Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_User
as
BEGIN
    Select *
    From LKACSoft_User
END
go

Create or Alter Proc sp_GetByID_LKACSoft_User
@ID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_User
    Where ID = @ID
END
go

CREATE OR ALTER PROCEDURE sp_Insert_LKACSoft_User
    @UserID         VARCHAR(450),
    @UserName       VARCHAR(255),
    @ResponseMessage VARCHAR(255) OUTPUT
AS
BEGIN
    -- Check for null values in required parameters
    IF @UserID IS NULL or @UserName IS NULL
    BEGIN
        SET @ResponseMessage = 'There are still field required to fill-in.';
        RETURN;
    END

    -- Validate that the Team doesn't already exist by checking the TeamID
    IF EXISTS (
        SELECT 1 
        FROM LKACSoft_User
        WHERE ID = @UserID
    )
    BEGIN
        set @ResponseMessage = 'This UserID is already exist';
        RETURN;
    END

    -- Insert the new user record into the table
    INSERT INTO dbo.LKACSoft_User
    (
        ID,
        Username
    )
    VALUES
    (
        @UserID,
        @Username
    );
    print 'User inserted successfully.';
END
GO

-- CREATE OR ALTER PROCEDURE sp_Insert_LKACSoft_User
--     @Username       NVARCHAR(255),
--     @Firstname      NVARCHAR(100),
--     @LastName       NVARCHAR(100),
--     @Address        NVARCHAR(255),
--     @District       NVARCHAR(255),
--     @Dob            DATE,
--     @IsQuitJob      BIT,
--     @Team           VARCHAR(255),
--     @NewUserID      VARCHAR(255) OUTPUT,
--     @ResponseMessage VARCHAR(255) OUTPUT
-- AS
-- BEGIN
--     -- Check for null values in required parameters
--     IF @Username IS NULL OR @Firstname IS NULL OR @LastName IS NULL
--         OR @Address IS NULL OR @District IS NULL OR @Dob IS NULL
--     BEGIN
--         SET @ResponseMessage = 'There are still field required to fill-in.';
--         RETURN;
--     END

--     -- Validate that the Team doesn't already exist by checking the TeamID
--     IF NOT EXISTS (
--         SELECT 1 
--         FROM LKACSoft_AccountantTeam
--         WHERE TeamID = @Team
--             or @Team is null
--     )
--     BEGIN
--         set @ResponseMessage = 'This Team is not exist';
--         RETURN;
--     END

--     DECLARE @NewID VARCHAR(255);
--     DECLARE @LastID VARCHAR(255);
--     DECLARE @NextID INT;

--     -- Step 1: Get the latest ID_Process
--     SELECT @LastID = MAX(ExecutionID) FROM LKACSoft_Execution;

--     -- Step 2: If there is no existing ID, start with ...
--     IF @LastID IS NULL
--     BEGIN
--         SET @NewID = 'U001';
--     END
--     ELSE
--     BEGIN
--         -- Step 3: Extract the numeric part, increment it, and format it with leading zeros
--         SET @NextID = CAST(SUBSTRING(@LastID, 2, 3) AS INT) + 1;
--         SET @NewID = 'U' + RIGHT('000' + CAST(@NextID AS VARCHAR), 3);
--     END

--     -- Insert the new user record into the table
--     INSERT INTO dbo.LKACSoft_User
--     (
--         ID,
--         Username,
--         Firstname,
--         LastName,
--         Address,
--         District,
--         Dob,
--         IsQuitJob,
--         DateCreate,
--         Team
--     )
--     VALUES
--     (
--         @NewID,
--         @Username,
--         @Firstname,
--         @LastName,
--         @Address,
--         @District,
--         @Dob,
--         @IsQuitJob,
--         GETDATE(),
--         @Team
--     );

--     Set @NewUserID = @NewID;
--     print 'User inserted successfully.';
-- END
-- GO



Create or Alter Proc sp_Update_LKACSoft_User_Avatar
@ID                    VARCHAR(255),
@Avatar                NVARCHAR(255) = NULL,
@ResponseMessage         VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @ID IS NULL
    BEGIN
        SET @ResponseMessage = 'ID cannot be NULL.';
        RETURN;
    END

    -- Handle empty string cases
    IF @Avatar = '' SET @Avatar = NULL;

    

    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_User WHERE ID = @ID and Avatar is not null)
    BEGIN

        -- Update the existing record
        UPDATE LKACSoft_User
        SET
            Avatar = @Avatar
        WHERE ID = @ID;

        print 'Update user avatar successfully.';
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'Update user avatar fail.';
    END
END
GO

Create or Alter Proc sp_Insert_LKACSoft_User_Avatar
@ID                    VARCHAR(255),
@Avatar                NVARCHAR(255) = NULL,
@ResponseMessage         VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @ID IS NULL
    BEGIN
        SET @ResponseMessage = 'ID cannot be NULL.';
        RETURN;
    END

    -- Handle empty string cases
    IF @Avatar = ''
    BEGIN
        SET @ResponseMessage = 'There is no Avatar passed.'
        return
    END;

    Declare @IDCheckExist varchar(255);

    Declare @AvatarCheckExist varchar(50);

    SELECT @IDCheckExist = ID, @AvatarCheckExist = Avatar 
    FROM LKACSoft_User 
    WHERE ID = @ID
    

    -- Check if the record exists
    IF (@IDCheckExist is not null)
    BEGIN

        IF (@AvatarCheckExist is not null)
        BEGIN
            SET @ResponseMessage = 'Can not upload avatar image into a user who already had an avatar.'
            return
        END

        -- Update the existing record
        UPDATE LKACSoft_User
        SET
            Avatar = @Avatar
        WHERE ID = @ID;

        print 'Upload user avatar successfully.';
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'There is no user exist';
    END
END
GO