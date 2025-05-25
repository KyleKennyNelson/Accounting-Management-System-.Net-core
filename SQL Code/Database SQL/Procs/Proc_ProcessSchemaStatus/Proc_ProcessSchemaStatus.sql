Use db_ab6e43_lkacsoftdb
go

-- Drop proc sp_GetAll_LKACSoft_ProcessSchemaStatus
-- Drop proc sp_GetByID_LKACSoft_ProcessSchemaStatus
-- go

Create or Alter Proc sp_GetAll_V_DetailProcessSchemaStatuses
as
BEGIN
    Select *
    From V_DetailProcessSchemaStatuses
END
go

Create or Alter Proc sp_GetByID_V_DetailProcessSchemaStatus
@ProcessSchemaStatusID VARCHAR(255)
as
BEGIN
    Select *
    From V_DetailProcessSchemaStatuses
    Where ProcessSchemaStatusID = @ProcessSchemaStatusID
END
go