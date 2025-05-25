Use db_ab6e43_lkacsoftdb
go

-- drop proc sp_GetAll_LKACSoft_Execution
-- drop proc sp_GetByID_LKACSoft_Execution
-- drop proc sp_Insert_LKACSoft_Execution
-- go

Create or Alter Proc sp_GetAll_LKACSoft_Execution
as
BEGIN
    Select *
    From LKACSoft_Execution
END
go

Create or Alter Proc sp_GetByID_LKACSoft_Execution
@ExecutionID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_Execution
    Where ExecutionID = @ExecutionID
END
go

-- select *
-- from LKACSoft_ProcessSchemaStatus


Create or Alter Proc sp_Insert_LKACSoft_Execution
@ExecutionName              NVARCHAR(255),
@CreatedBy                  VARCHAR(255),
@IsPeriodic                 BIT,
@ProcessSchemaStatus        VARCHAR(255),
@ProcessSchemaID            VARCHAR(255),
@RelatedToCustomer          VARCHAR(255),
@NewExecutionID             VARCHAR(255) OUTPUT,
@ResponseMessage            VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values in all parameters
    IF @CreatedBy IS NULL AND @RelatedToCustomer IS NULL
       AND @ProcessSchemaStatus IS NULL AND @ProcessSchemaID IS NULL
    BEGIN
        set @ResponseMessage = 'Atleast one parameter should be passed.';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_ProcessSchemaStatus
        WHERE ProcessSchemaStatusID = @ProcessSchemaStatus
            or @ProcessSchemaStatus is null
    )
    BEGIN
        set @ResponseMessage = 'This ProcessSchemaStatus does not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_ProcessSchema
        WHERE ProcessSchemaID = @ProcessSchemaID
    )
    BEGIN
        set @ResponseMessage = 'This ProcessSchema does not exist or you have not passed it';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_User
        WHERE ID = @CreatedBy
            or @CreatedBy is null
    )
    BEGIN
        set @ResponseMessage = 'This User does not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_Customer
        WHERE Code = @RelatedToCustomer
    )
    BEGIN
        set @ResponseMessage = 'This Customer does not exist or you have not passed it';
        RETURN;
    END

    IF (@ProcessSchemaStatus is null)
    BEGIN
        set @ProcessSchemaStatus = (Select ProcessSchemaStatusID
                                    From LKACSoft_ProcessSchemaStatus
                                    Where ProcessSchema = @ProcessSchemaID
                                        and OrderIndex = 1)
    END

    DECLARE @NewID VARCHAR(255);
    DECLARE @LastID VARCHAR(255);
    DECLARE @NextID INT;

    -- Step 1: Get the latest ID_Process
    SELECT @LastID = MAX(ExecutionID) FROM LKACSoft_Execution;

    -- Step 2: If there is no existing ID, start with ...
    IF @LastID IS NULL
    BEGIN
        SET @NewID = 'E001';
    END
    ELSE
    BEGIN
        -- Step 3: Extract the numeric part, increment it, and format it with leading zeros
        SET @NextID = CAST(SUBSTRING(@LastID, 2, 3) AS INT) + 1;
        SET @NewID = 'E' + RIGHT('000' + CAST(@NextID AS VARCHAR), 3);
    END

    -- Step 4: Insert the new record with the generated ID
    INSERT INTO dbo.LKACSoft_Execution
    (
        ExecutionID,
        ExecutionName,
        CreatedBy,
        DateCreated,
        IsPeriodic,
        ProcessSchemaStatus,
        ProcessSchemaID,
        RelatedToCustomer 
    )
    VALUES (
        @NewID,
        @ExecutionName,
        @CreatedBy,
        GETDATE(),
        @IsPeriodic,
        @ProcessSchemaStatus,
        @ProcessSchemaID,
        @RelatedToCustomer 
        );

    IF exists (select 1 from LKACSoft_ProcessSchema where ProcessSchemaID = @ProcessSchemaID and Name = N'Quy trình nhận và nhập liệu chứng từ')
    BEGIN

        DECLARE @GetDocsDate INT;

        Select @GetDocsDate = GetDocsDate
        From LKACSoft_Customer
        Where Code = @RelatedToCustomer

        -- Build a date with current year and month, and your chosen day
        DECLARE @GetDocsDay DATE = DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), @GetDocsDate);


        INSERT INTO [dbo].[LKACSoft_ExecutionAttributesValue]
            (ExecutionID
            ,FieldID
            ,FieldValue)
        VALUES 
        (@NewID, (select FieldID from LKACSoft_ExecutionAttributesDefinition where ProcessSchemaID = @ProcessSchemaID), @GetDocsDay);
    END

    Set @NewExecutionID = @NewID
END
GO

-- declare @ResponseMessage varchar(255);
-- EXEC sp_Insert_LKACSoft_Execution 'ADAD', 'U007', 1, 'PSS02', 'SC001', 'C002', null, @ResponseMessage output
-- print @ResponseMessage
-- GO