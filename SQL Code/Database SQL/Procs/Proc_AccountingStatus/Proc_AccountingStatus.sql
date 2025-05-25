Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_AccountingStatus
as
BEGIN
    Select *
    From LKACSoft_AccountingStatus
END
go

Create or Alter Proc sp_GetByID_LKACSoft_AccountingStatus
@ID INT
as
BEGIN
    Select *
    From LKACSoft_AccountingStatus
    Where ID = @ID
END
go