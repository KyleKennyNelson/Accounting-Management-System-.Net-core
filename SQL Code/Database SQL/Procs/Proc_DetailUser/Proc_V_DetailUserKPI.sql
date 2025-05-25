Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetByID_V_DetailUserKPI
@UserID VARCHAR(255)
as
BEGIN
    Select *
    From V_DetailUsersKPI
    Where UserID = @UserID
END
go

-- select *
-- from AspNetUsers u, AspNetRoles r, AspNetUserRoles ur
-- where ur.UserId = u.Id
--     and ur.RoleId = r.Id