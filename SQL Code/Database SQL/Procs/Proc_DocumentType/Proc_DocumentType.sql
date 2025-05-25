Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_V_DocumentTypeDto
as
BEGIN
    Select *
    From V_DocumentTypeDtos
END
go

Create or Alter Proc sp_GetByID_V_DocumentTypeDto
@DocumentTypeID VARCHAR(255)
as
BEGIN
    Select *
    From V_DocumentTypeDtos
    Where DocumentTypeID = @DocumentTypeID
END
go

CREATE Or Alter PROCEDURE sp_Delete_LKACSoft_DocumentType
@DocumentTypeID VARCHAR(255)
AS
BEGIN
    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM LKACSoft_DocumentType WHERE DocumentTypeID = @DocumentTypeID)
    BEGIN
        IF EXISTS (SELECT 1 FROM LKACSoft_CustomerDocumentType WHERE DocumentTypeID = @DocumentTypeID)
        BEGIN
            -- Delete the record
            DELETE FROM LKACSoft_CustomerDocumentType WHERE DocumentTypeID = @DocumentTypeID;
        END
        -- Delete the record
        DELETE FROM LKACSoft_DocumentType WHERE DocumentTypeID = @DocumentTypeID;
    END
END
GO