Use db_ab6e43_lkacsoftdb
go

-- Drop proc sp_GetAll_LKACSoft_ProcessSchemaStatus
-- Drop proc sp_GetByID_LKACSoft_ProcessSchemaStatus
-- go

Create or Alter Proc sp_GetAll_LKACSoft_ProcessSchema
as
BEGIN
    Select *
    From LKACSoft_ProcessSchema
END
go

Create or Alter Proc sp_GetByID_LKACSoft_ProcessSchema
@ProcessSchemaID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_ProcessSchema
    Where ProcessSchemaID = @ProcessSchemaID
END
go