Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_DetailJobTaskFile
as
BEGIN
    Select *
    From V_DetailJobTaskFiles
END
go

-- exec sp_GetByID_LKACSoft_DetailJobTaskFile 'F001'
-- go

Create or Alter Proc sp_GetByID_LKACSoft_DetailJobTaskFile
@code VARCHAR(255)
as
BEGIN
    Select *
    From V_DetailJobTaskFiles
    Where JobTaskFileCode = @code
END
go

Create or Alter Proc sp_GetByID_LKACSoft_JobTaskFile
@code VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_JobTaskFile
    Where code = @code
END
go



-- declare @NewCode                VARCHAR(255);
-- declare @ResponseMessage            VARCHAR(255);

-- exec sp_Insert_LKACSoft_JobTaskFile null, "U008", null, null, null, null, null, null, null, null, null, null, null, @NewCode output, @ResponseMessage output
-- go

Create or Alter Proc sp_Insert_LKACSoft_JobTaskFile
@FileName            NVARCHAR(255) = NULL,
@AccountantID        VARCHAR(255)  = NULL,
@AccountingStatus    INT = NULL,
@AccountantReceivedAt    DATE      = NULL,
@ReadyToBeReturnedAt     DATE      = NULL,
@ArchivingStatus     INT = NULL,
@PhysicalLocation    NVARCHAR(255) = NULL,
@ArchivedDate        DATE          = NULL,
@ReturnedDate        DATE          = NULL,
@CreatedBy           VARCHAR(255)  = NULL,
@RelatedToExecution  VARCHAR(255)  = NULL,
@DocumentType        VARCHAR(255)  = NULL,
@NewCode             VARCHAR(255) OUTPUT,
@ResponseMessage     VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values in the parameters
    IF --@FileName IS NULL AND @FileType IS NULL AND @FileS3Key IS NULL AND
       @AccountantID IS NULL AND --@AccountingStatus IS NULL AND
       --@AccountantReceivedAt IS NULL AND @ReadyToBeReturnedAt IS NULL AND
       --@ArchivingStatus IS NULL AND @PhysicalLocation IS NULL AND @ArchivedDate IS NULL AND
       --@ReturnedDate IS NULL AND @CreatedBy IS NULL AND 
       @RelatedToExecution IS NULL AND @DocumentType IS NULL
    BEGIN
        SET @ResponseMessage = 'At least one parameter should be passed.';
        RETURN;
    END

    -- Handle empty string cases
    IF @FileName = '' SET @FileName = NULL;
    IF @AccountingStatus = '' SET @AccountingStatus = NULL;
    IF @ArchivingStatus = '' SET @ArchivingStatus = NULL;
    IF @PhysicalLocation = '' SET @PhysicalLocation = NULL;
    IF @CreatedBy = '' SET @CreatedBy = NULL;
    IF @RelatedToExecution = '' SET @RelatedToExecution = NULL;
    IF @DocumentType = '' SET @DocumentType = NULL;

    -- Set default values for fields if necessary
    -- IF @AccountingStatus IS NULL
    -- BEGIN
    --     SET @AccountingStatus = 'Pending';
    -- END

    -- Check if the Accountant exists
    IF NOT EXISTS ( SELECT 1
                    FROM LKACSoft_User, LKACSoft_UserPosition u, LKACSoft_RolePosition r, LKACSoft_Department 
                    WHERE ID = @AccountantID
                        And ID = u.UserID
                        and u.RoleID = r.RoleID
                        And r.LKACSoft_DepartmentCode = Code
                        And Name = N'Phòng Kế toán'
                    OR @AccountantID IS NULL)
    BEGIN
        SET @ResponseMessage = 'This Accountant does not exist';
        RETURN;
    END

    -- Check if the AccountingStatus exists
    IF NOT EXISTS ( SELECT 1
                    FROM LKACSoft_AccountingStatus
                    WHERE ID = @AccountingStatus
                    OR @AccountingStatus IS NULL)
    BEGIN
        SET @ResponseMessage = 'This AccountingStatus does not exist';
        RETURN;
    END

    -- Check if the @ArchivingStatus exists
    IF NOT EXISTS ( SELECT 1
                    FROM LKACSoft_ArchivingStatus
                    WHERE ID = @ArchivingStatus
                    OR @ArchivingStatus IS NULL)
    BEGIN
        SET @ResponseMessage = 'This ArchivingStatus does not exist';
        RETURN;
    END

    -- Check if the User create file exist
    IF NOT EXISTS (SELECT 1 FROM LKACSoft_User WHERE ID = @CreatedBy OR @CreatedBy IS NULL)
    BEGIN
        SET @ResponseMessage = 'This User does not exist';
        RETURN;
    END

    -- Check if Related Process exists
    IF NOT EXISTS (SELECT 1 FROM LKACSoft_Execution WHERE ExecutionID = @RelatedToExecution OR @RelatedToExecution IS NULL)
    BEGIN
        SET @ResponseMessage = 'This Execution does not exist';
        RETURN;
    END

    -- Check if DocumentType exists
    IF NOT EXISTS (SELECT 1 FROM LKACSoft_DocumentType WHERE DocumentTypeID = @DocumentType OR @DocumentType IS NULL)
    BEGIN
        SET @ResponseMessage = 'This DocumentType does not exist';
        RETURN;
    END

    DECLARE @NewID VARCHAR(255);
    DECLARE @LastID VARCHAR(255);
    DECLARE @NextID INT;

    -- Get the latest Code (FileID)
    SELECT @LastID = MAX(Code) FROM LKACSoft_JobTaskFile;

    IF @LastID IS NULL
    BEGIN
        SET @NewID = 'F001';
    END
    ELSE
    BEGIN
        SET @NextID = CAST(SUBSTRING(@LastID, 2, 3) AS INT) + 1;
        SET @NewID = 'F' + RIGHT('000' + CAST(@NextID AS VARCHAR), 3);
    END

    -- Insert the new job task file record
    INSERT INTO dbo.LKACSoft_JobTaskFile
    (
        Code,
        FileName,
        AccountantID,
        AccountingStatus,
        AccountantReceivedAt,
        ReadyToBeReturnedAt,
        ArchivingStatus,
        PhysicalLocation,
        ArchivedDate,
        ReturnedDate,
        CreatedBy,
        RelatedToExecution,
        DocumentType,
        CustomerCode,
        CreatedAt
    )
    VALUES
    (
        @NewID,
        @FileName,
        @AccountantID,
        @AccountingStatus,
        @AccountantReceivedAt,
        @ReadyToBeReturnedAt,
        @ArchivingStatus,
        @PhysicalLocation,
        @ArchivedDate,
        @ReturnedDate,
        @CreatedBy,
        @RelatedToExecution,
        @DocumentType,
        (Select Top 1 C.Code
        From LKACSoft_JobTaskFile 
        left join LKACSoft_Execution
            on RelatedToExecution = ExecutionID
        left join LKACSoft_Customer C
            on RelatedToCustomer = C.Code
        where ExecutionID = @RelatedToExecution
        ),
        GETDATE()
    );

    -- Output the new code (primary key)
    SET @NewCode = @NewID;
END
GO

CREATE Or Alter PROCEDURE sp_Delete_LKACSoft_JobTaskFile
@Code VARCHAR(255)

AS
BEGIN
    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_JobTaskFile WHERE Code = @Code)
    BEGIN
        -- Delete the record
        DELETE FROM LKACSoft_JobTaskFile WHERE Code = @Code;
    END
END
GO

-- declare @ResponseMessage            VARCHAR(255);

-- exec sp_Update_LKACSoft_JobTaskFile 'F016', null, 0, null, null, null, null, null, null, null, null, null, null, @ResponseMessage output

-- print @ResponseMessage
-- go

-- select *
-- from LKACSoft_JobTaskFile
-- where Code = 'F016'
-- go

Create or Alter Proc sp_Update_LKACSoft_JobTaskFile
@Code                    VARCHAR(255),
@AccountantID            VARCHAR(255) = NULL,
@AccountingStatus        INT = NULL,
@AccountantReceivedAt    VARCHAR(50) = NULL,
@ReadyToBeReturnedAt    VARCHAR(50) = NULL,
@ArchivingStatus        INT = NULL,
@PhysicalLocation        NVARCHAR(255) = NULL,
@ArchivedDate            VARCHAR(50) = NULL,
@ReturnedDate            VARCHAR(50) = NULL,
@CreatedBy               VARCHAR(255) = NULL,
@RelatedToExecution      VARCHAR(255) = NULL,
@DocumentType            VARCHAR(255) = NULL,
@ResponseMessage         VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @Code IS NULL
    BEGIN
        SET @ResponseMessage = 'JobTaskFileCode cannot be NULL.';
        RETURN;
    END

    -- Set default values for fields if necessary
    -- IF @AccountingStatus IS NULL
    -- BEGIN
    --     SET @AccountingStatus = 'Pending';
    -- END

    -- Check if the Accountant exists
    IF NOT EXISTS ( SELECT 1
                    FROM LKACSoft_User, LKACSoft_UserPosition u, LKACSoft_RolePosition r, LKACSoft_Department 
                    WHERE ID = @AccountantID
                        And ID = u.UserID
                        and u.RoleID = r.RoleID
                        And r.LKACSoft_DepartmentCode = Code
                        And Name = N'Phòng Kế toán'
                    OR @AccountantID IS NULL
                    OR @AccountantID = '')
    BEGIN
        SET @ResponseMessage = 'This Accountant does not exist';
        RETURN;
    END

    -- Check if the AccountingStatus exists
    IF NOT EXISTS ( SELECT 1
                    FROM LKACSoft_AccountingStatus
                    WHERE ID = @AccountingStatus
                    OR @AccountingStatus IS NULL
                    OR @AccountingStatus = '')
    BEGIN
        SET @ResponseMessage = 'This AccountingStatus does not exist';
        RETURN;
    END

    -- Check if the @ArchivingStatus exists
    IF NOT EXISTS ( SELECT 1
                    FROM LKACSoft_ArchivingStatus
                    WHERE ID = @ArchivingStatus
                    OR @ArchivingStatus IS NULL
                    OR @ArchivingStatus = '')
    BEGIN
        SET @ResponseMessage = 'This ArchivingStatus does not exist';
        RETURN;
    END

    -- Check if the User create file exist
    IF NOT EXISTS (SELECT 1 FROM LKACSoft_User WHERE ID = @CreatedBy OR @CreatedBy IS NULL OR @CreatedBy = '')
    BEGIN
        SET @ResponseMessage = 'This User does not exist';
        RETURN;
    END

    -- Check if Related Process exists
    IF NOT EXISTS (SELECT 1 FROM LKACSoft_Execution WHERE ExecutionID = @RelatedToExecution OR @RelatedToExecution IS NULL OR @RelatedToExecution = '')
    BEGIN
        SET @ResponseMessage = 'This Process does not exist';
        RETURN;
    END

    -- Check if DocumentType exists
    IF NOT EXISTS (SELECT 1 FROM LKACSoft_DocumentType WHERE DocumentTypeID = @DocumentType OR @DocumentType IS NULL OR @DocumentType = '')
    BEGIN
        SET @ResponseMessage = 'This DocumentType does not exist';
        RETURN;
    END

    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_JobTaskFile WHERE Code = @Code)
    BEGIN

        -- Update the existing record
        -- Update AccountantID
        IF @AccountantID = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET AccountantID = NULL
            WHERE Code = @Code;
        END
        ELSE IF @AccountantID IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET AccountantID = @AccountantID
            WHERE Code = @Code;
        END

        -- Update AccountingStatus
        IF @AccountingStatus = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET AccountingStatus = NULL
            WHERE Code = @Code;
        END
        ELSE IF @AccountingStatus IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET AccountingStatus = @AccountingStatus
            WHERE Code = @Code;
        END

        -- Update AccountantReceivedAt
        IF @AccountantReceivedAt = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET AccountantReceivedAt = NULL
            WHERE Code = @Code;
        END
        ELSE IF @AccountantReceivedAt IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET AccountantReceivedAt = @AccountantReceivedAt
            WHERE Code = @Code;
        END

        -- Update ReadyToBeReturnedAt
        IF @ReadyToBeReturnedAt = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET ReadyToBeReturnedAt = NULL
            WHERE Code = @Code;
        END
        ELSE IF @ReadyToBeReturnedAt IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET ReadyToBeReturnedAt = @ReadyToBeReturnedAt
            WHERE Code = @Code;
        END

        -- Update ArchivingStatus
        IF @ArchivingStatus = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET ArchivingStatus = NULL
            WHERE Code = @Code;
        END
        ELSE IF @ArchivingStatus IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET ArchivingStatus = @ArchivingStatus
            WHERE Code = @Code;
        END

        -- Update PhysicalLocation
        IF @PhysicalLocation = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET PhysicalLocation = NULL
            WHERE Code = @Code;
        END
        ELSE IF @PhysicalLocation IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET PhysicalLocation = @PhysicalLocation
            WHERE Code = @Code;
        END

        -- Update ArchivedDate
        IF @ArchivedDate = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET ArchivedDate = NULL
            WHERE Code = @Code;
        END
        ELSE IF @ArchivedDate IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET ArchivedDate = @ArchivedDate
            WHERE Code = @Code;
        END

        -- Update ReturnedDate
        IF @ReturnedDate = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET ReturnedDate = NULL
            WHERE Code = @Code;
        END
        ELSE IF @ReturnedDate IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET ReturnedDate = @ReturnedDate
            WHERE Code = @Code;
        END

        -- Update CreatedBy
        IF @CreatedBy = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET CreatedBy = NULL
            WHERE Code = @Code;
        END
        ELSE IF @CreatedBy IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET CreatedBy = @CreatedBy
            WHERE Code = @Code;
        END

        -- Update RelatedToExecution
        IF @RelatedToExecution = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET RelatedToExecution = NULL
            WHERE Code = @Code;
        END
        ELSE IF @RelatedToExecution IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET RelatedToExecution = @RelatedToExecution
            WHERE Code = @Code;
        END

        -- Update DocumentType
        IF @DocumentType = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET DocumentType = NULL
            WHERE Code = @Code;
        END
        ELSE IF @DocumentType IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET DocumentType = @DocumentType
            WHERE Code = @Code;
        END

        -- Update the existing record
        UPDATE LKACSoft_JobTaskFile
        SET 
            CreatedAt = GETDATE()
            -- AccountantID = @AccountantID,
            -- AccountingStatus = @AccountingStatus,
            -- AccountantReceivedAt = @AccountantReceivedAt,
            -- ReadyToBeReturnedAt = @ReadyToBeReturnedAt,
            -- ArchivingStatus = @ArchivingStatus,
            -- PhysicalLocation = @PhysicalLocation,
            -- ArchivedDate = @ArchivedDate,
            -- ReturnedDate = @ReturnedDate,
            -- CreatedBy = @CreatedBy,
            -- RelatedToExecution = @RelatedToExecution,
            -- DocumentType = @DocumentType,
            -- CustomerCode = @CustomerCode
        WHERE Code = @Code;

        print 'Job task file updated successfully.';
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'Job task file with the given Code not found.';
    END
END
GO

Create or Alter Proc sp_Update_LKACSoft_JobTaskFile_File
@Code                    VARCHAR(255),
@FileName                NVARCHAR(255) = NULL,
@FileType                NVARCHAR(255) = NULL,
@FileS3Key                 VARCHAR(255) = NULL,
@ResponseMessage         VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @Code IS NULL
    BEGIN
        SET @ResponseMessage = 'Code cannot be NULL.';
        RETURN;
    END

    

    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_JobTaskFile WHERE Code = @Code)
    BEGIN

    -- Update the existing record

        -- Update FileName
        IF @FileName = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET FileName = NULL
            WHERE Code = @Code;
        END
        ELSE IF @FileName IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET FileName = @FileName + cast(NEWID() as varchar(36))
            WHERE Code = @Code;
        END

        -- Update FileType
        IF @FileType = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET FileType = NULL
            WHERE Code = @Code;
        END
        ELSE IF @FileType IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET FileType = @FileType
            WHERE Code = @Code;
        END

        -- Update FileS3Key
        IF @FileS3Key = '' 
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET FileS3Key = NULL
            WHERE Code = @Code;
        END
        ELSE IF @FileS3Key IS NOT NULL
        BEGIN
            UPDATE LKACSoft_JobTaskFile
            SET FileS3Key = @FileS3Key
            WHERE Code = @Code;
        END

        -- UPDATE LKACSoft_JobTaskFile
        -- SET
        --     FileName = @FileName + cast(NEWID() as varchar(36)),
        --     FileType = @FileType,
        --     FileS3Key = @FileS3Key
        -- WHERE Code = @Code;

        print 'Update file successfully.';
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'Update file fail.';
    END
END
GO

Create or Alter Proc sp_Insert_LKACSoft_JobTaskFile_File
@Code                    VARCHAR(255),
@FileName                NVARCHAR(255) = NULL,
@FileType                NVARCHAR(255) = NULL,
@FileS3Key                 VARCHAR(255) = NULL,
@ResponseMessage         VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @Code IS NULL
    BEGIN
        SET @ResponseMessage = 'Code cannot be NULL.';
        RETURN;
    END

    -- Handle empty string cases
    IF @FileName = ''
    BEGIN
        SET @ResponseMessage = 'There is no filename passed.'
        return
    END

    IF @FileType = ''
    BEGIN
        SET @ResponseMessage = 'There is no filetype passed.'
        return
    END

    IF @FileS3Key = ''
    BEGIN
        SET @ResponseMessage = 'There is no fileKey exist.'
        return
    END;

    Declare @CodeCheckExist varchar(255);

    Declare @FileS3KeyCheckExist varchar(50);

    SELECT @CodeCheckExist = Code, @FileS3KeyCheckExist = FileS3Key 
    FROM LKACSoft_JobTaskFile 
    WHERE Code = @Code
    

    -- Check if the record exists
    IF (@CodeCheckExist is not null)
    BEGIN

        IF (@FileS3KeyCheckExist is not null)
        BEGIN
            SET @ResponseMessage = 'Can not upload file into a jobtaskfile which already had a file.'
            return
        END

        -- Update the existing record
        UPDATE LKACSoft_JobTaskFile
        SET 
            FileName = @FileName + cast(NEWID() as varchar(36)),
            FileType = @FileType,
            FileS3Key = @FileS3Key
        WHERE Code = @Code;

        print 'Upload file successfully.';
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'There is no JobTaskFile exist';
    END
END
GO

-- exec sp_Update_LKACSoft_Task 'TK003', '2023-03-06', '2023-03-15', 'U008',
--  N'Kế toán nhập liệu Q1/2023',
--  N'Nhập chi tiết sổ sách cho Cty XYZ',
--  'TS01', '2023-03-07', NULL, 'U007',
--  N'Chưa hoàn thành', NULL, 0,
--  'P002', 'TT03', 'DT002', 20, 11;