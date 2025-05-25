Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_TaskTypeResponsiblePosition
as
BEGIN
    Select *
    From LKACSoft_TaskTypeResponsiblePosition
END
go

Create or Alter Proc sp_GetByID_LKACSoft_TaskTypeResponsiblePosition
@TaskTypeID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_TaskTypeResponsiblePosition
    Where TaskTypeID = @TaskTypeID
END
go