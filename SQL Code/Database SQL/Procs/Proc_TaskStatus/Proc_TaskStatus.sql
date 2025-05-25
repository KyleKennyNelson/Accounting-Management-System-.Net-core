Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_TaskStatus
as
BEGIN
    Select *
    From LKACSoft_TaskStatus
END
go

Create or Alter Proc sp_GetByID_LKACSoft_TaskStatus
@TaskStatusID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_TaskStatus
    Where TaskStatusID = @TaskStatusID
END
go