Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_ArchivingStatus
as
BEGIN
    Select *
    From LKACSoft_ArchivingStatus
END
go

Create or Alter Proc sp_GetByID_LKACSoft_ArchivingStatus
@ID INT
as
BEGIN
    Select *
    From LKACSoft_ArchivingStatus
    Where ID = @ID
END
go