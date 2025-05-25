Use db_ab6e43_lkacsoftdb
go

-- Drop proc sp_GetAll_LKACSoft_ProcessSchemaStatus
-- Drop proc sp_GetByID_LKACSoft_ProcessSchemaStatus
-- go

Create or Alter Proc sp_GetAll_LKACSoft_ProcessStatus
as
BEGIN
    Select *
    From LKACSoft_ProcessStatus
END
go

Create or Alter Proc sp_GetByID_LKACSoft_ProcessStatus
@ProcessStatusID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_ProcessStatus
    Where ProcessStatusID = @ProcessStatusID
END
go