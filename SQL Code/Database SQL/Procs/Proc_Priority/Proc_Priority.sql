Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_Priority
as
BEGIN
    Select *
    From LKACSoft_Priority
END
go

Create or Alter Proc sp_GetByID_LKACSoft_Priority
@PriorityID VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_Priority
    Where PriorityID = @PriorityID
END
go