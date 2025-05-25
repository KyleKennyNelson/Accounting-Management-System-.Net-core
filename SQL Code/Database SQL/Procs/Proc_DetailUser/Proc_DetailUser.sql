Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_LKACSoft_DetailUser
as
BEGIN
    Select *
    From V_DetailUsers
END
go

Create or Alter Proc sp_GetByID_LKACSoft_DetailUser
@UserID VARCHAR(255)
as
BEGIN
    Select *
    From V_DetailUsers
    Where UserID = @UserID
END
go

-- select *
-- from AspNetUsers u, AspNetRoles r, AspNetUserRoles ur
-- where ur.UserId = u.Id
--     and ur.RoleId = r.Id