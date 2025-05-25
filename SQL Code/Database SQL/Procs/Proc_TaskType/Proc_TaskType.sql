Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_TaskType
as
BEGIN
    Select *
    From LKACSoft_TaskType
END
go

Create or Alter Proc sp_GetByID_LKACSoft_TaskType
@TaskTypeID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_TaskType
    Where TaskTypeID = @TaskTypeID
END
go