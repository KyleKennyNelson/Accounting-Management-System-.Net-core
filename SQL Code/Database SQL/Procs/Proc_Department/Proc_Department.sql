Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_Department
as
BEGIN
    Select *
    From LKACSoft_Department
END
go

Create or Alter Proc sp_GetByID_LKACSoft_Department
@Code VARCHAR(255)
as
BEGIN
    Select *
    From LKACSoft_Department
    Where Code = @Code
END
go