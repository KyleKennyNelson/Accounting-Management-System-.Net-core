Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_V_DetailDocumentType
as
BEGIN
    Select *
    From V_DetailDocumentTypes
END
go

Create or Alter Proc sp_GetByID_V_DetailDocumentType
@DocumentTypeID VARCHAR(255)
as
BEGIN
    Select *
    From V_DetailDocumentTypes
    Where DocumentTypeID = @DocumentTypeID
END
go

Create or Alter Proc sp_GetExcept_V_CustomerWithOutDetailDocumentType
@DocumentTypeID VARCHAR(255)
as
BEGIN
    Select distinct *
    From LKACSoft_Customer
    Where Code not in ( select CustomerCode
                        From LKACSoft_CustomerDocumentType
                        where DocumentTypeID = @DocumentTypeID)
END
go

-- exec sp_GetExcept_V_CustomerWithOutDetailDocumentType 'DT005'
-- go

Create or Alter Proc sp_GetByID_V_DetailDocumentType_CustomerCode_DocumentTypeID
@CustomerCode VARCHAR(255),
@DocumentTypeID VARCHAR(255)
as
BEGIN
    Select *
    From V_DetailDocumentTypes
    Where Code = @CustomerCode
        AND DocumentTypeID = @DocumentTypeID
END
go


CREATE Or Alter PROCEDURE sp_Delete_LKACSoft_DetailDocumentType
@DocumentTypeID VARCHAR(255),
@CustomerCode VARCHAR(255)
AS
BEGIN
    -- Check if the record exists
    IF EXISTS ( SELECT 1 FROM LKACSoft_CustomerDocumentType 
                WHERE DocumentTypeID = @DocumentTypeID
                    AND CustomerCode = @CustomerCode)
    BEGIN
        DELETE FROM LKACSoft_CustomerDocumentType 
        WHERE DocumentTypeID = @DocumentTypeID
            AND CustomerCode = @CustomerCode;
    END
END
GO

Create or Alter Proc sp_Insert_LKACSoft_DetailDocumentType
@CustomerCode                VARCHAR(255),
@DocumentTypeID              VARCHAR(255),
@DocumentReceivingMechanism  NVARCHAR(255) = NULL,
@AvgAmount                   INT           = NULL,
@RegisteredAmount            INT           = NULL,
@ResponseMessage             VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @CustomerCode IS NULL or @DocumentTypeID IS NULL
    BEGIN
        Set @ResponseMessage = '@CustomerCode nor @DocumentTypeID cannot be NULL.';
        RETURN;
    END

     -- Check for null values in all parameters
    IF @DocumentReceivingMechanism IS NULL AND @AvgAmount IS NULL AND @RegisteredAmount IS NULL
    BEGIN
        set @ResponseMessage = 'Atleast one parameter should be passed.';
        RETURN;
    END

    -- @DocumentReceivingMechanism
    IF @DocumentReceivingMechanism = ''
    BEGIN
        SET @DocumentReceivingMechanism = NULL;
    END

    -- @AvgAmount
    IF @AvgAmount = ''
    BEGIN
        SET @AvgAmount = NULL;
    END

    -- @RegisteredAmount
    IF @RegisteredAmount = ''
    BEGIN
        SET @RegisteredAmount = NULL;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_Customer
        WHERE Code = @CustomerCode
            --or @CustomerCode is null
    )
    BEGIN
        set @ResponseMessage = 'This Customer is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_DocumentType
        WHERE DocumentTypeID = @DocumentTypeID
            --or @DocumentTypeID is null
    )
    BEGIN
        set @ResponseMessage = 'This DocumentType is not exist';
        RETURN;
    END

    IF EXISTS (
        SELECT 1 
        FROM LKACSoft_CustomerDocumentType
        WHERE CustomerCode = @CustomerCode
            and DocumentTypeID = @DocumentTypeID
    )
    BEGIN
        set @ResponseMessage = 'This CustomerDocumentType is already exist';
        RETURN;
    END

    

    -- Step 4: Insert the new record with the generated ID
    INSERT INTO dbo.LKACSoft_CustomerDocumentType
    (
        CustomerCode,
        DocumentTypeID,
        DocumentReceivingMechanism,
        AvgAmount,
        RegisteredAmount
    )
    VALUES (
        @CustomerCode,
        @DocumentTypeID,
        @DocumentReceivingMechanism,
        @AvgAmount,
        @RegisteredAmount
        );
END
GO

Create or Alter Proc sp_Update_LKACSoft_DetailDocumentType
@CustomerCode                VARCHAR(255),
@DocumentTypeID              VARCHAR(255),
@DocumentReceivingMechanism  NVARCHAR(255) = NULL,
@AvgAmount                   INT           = NULL,
@RegisteredAmount            INT           = NULL,
@ResponseMessage             VARCHAR(255) OUTPUT
as
Begin

    -- Check for null values
    IF @CustomerCode IS NULL or @DocumentTypeID IS NULL
    BEGIN
        Set @ResponseMessage = '@CustomerCode nor @DocumentTypeID cannot be NULL.';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_Customer
        WHERE Code = @CustomerCode
            -- or @CustomerCode is null
            -- or @CustomerCode = ''
    )
    BEGIN
        set @ResponseMessage = 'This Customer is not exist';
        RETURN;
    END

    IF NOT EXISTS (
        SELECT 1 
        FROM LKACSoft_DocumentType
        WHERE DocumentTypeID = @DocumentTypeID
            -- or @DocumentTypeID is null
            -- or @DocumentTypeID = ''
    )
    BEGIN
        set @ResponseMessage = 'This DocumentType is not exist';
        RETURN;
    END

    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_CustomerDocumentType WHERE CustomerCode = @CustomerCode AND DocumentTypeID = @DocumentTypeID)
    BEGIN

        -- Update DocumentReceivingMechanism
        IF @DocumentReceivingMechanism = ''
        Begin
            UPDATE LKACSoft_CustomerDocumentType
            SET DocumentReceivingMechanism = null
            WHERE CustomerCode = @CustomerCode AND DocumentTypeID = @DocumentTypeID;
        END
        else IF @DocumentReceivingMechanism IS NOT NULL
        BEGIN
            UPDATE LKACSoft_CustomerDocumentType
            SET DocumentReceivingMechanism = @DocumentReceivingMechanism
            WHERE CustomerCode = @CustomerCode AND DocumentTypeID = @DocumentTypeID;
        END

        -- Update AvgAmount
        IF @AvgAmount = ''
        Begin
            UPDATE LKACSoft_CustomerDocumentType
            SET @AvgAmount = null
            WHERE CustomerCode = @CustomerCode AND DocumentTypeID = @DocumentTypeID;
        END
        else IF @AvgAmount IS NOT NULL
        BEGIN
            UPDATE LKACSoft_CustomerDocumentType
            SET AvgAmount = @AvgAmount
            WHERE CustomerCode = @CustomerCode AND DocumentTypeID = @DocumentTypeID;
        END

        -- Update RegisteredAmount
        IF @RegisteredAmount = ''
        Begin
            UPDATE LKACSoft_CustomerDocumentType
            SET @RegisteredAmount = null
            WHERE CustomerCode = @CustomerCode AND DocumentTypeID = @DocumentTypeID;
        END
        else IF @RegisteredAmount IS NOT NULL
        BEGIN
            UPDATE LKACSoft_CustomerDocumentType
            SET RegisteredAmount = @RegisteredAmount
            WHERE CustomerCode = @CustomerCode AND DocumentTypeID = @DocumentTypeID;
        END

        --SET @ResponseMessage = 'Detail DocumentType updated successfully.';
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'The specified CustomerCode and DocumentTypeID record does not exist.';
    END

    

    -- Update the existing record
    -- UPDATE LKACSoft_CustomerDocumentType
    -- SET 
    --     DocumentReceivingMechanism = @DocumentReceivingMechanism,
    --     AvgAmount = @AvgAmount,
    --     RegisteredAmount = @RegisteredAmount,
    --     CurrentTotalAmount = @CurrentTotalAmount
    -- WHERE CustomerCode = @CustomerCode
    --     and DocumentTypeID = @DocumentTypeID;
END
GO

-- declare @responsemessage varchar(255);

-- exec sp_Update_LKACSoft_DetailDocumentType 'C001', 'DT001', null, 533545, null, null, @responsemessage output

-- select *
-- from LKACSoft_CustomerDocumentType