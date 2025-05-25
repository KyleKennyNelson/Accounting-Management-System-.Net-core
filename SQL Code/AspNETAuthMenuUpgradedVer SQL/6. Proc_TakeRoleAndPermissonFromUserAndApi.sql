use db_ab6e43_lkacsoftdb
go

--drop proc sp_TakeRole_Permission_From_UserAndApi

create or alter view V_TakeRole_From_UserAndApi
as
	SELECT Roles.Name AS RoleName, Users.UserName UserName, Apis.API API
    FROM AspNetUsers AS Users
    INNER JOIN AspNetUserRoles AS UserRoles ON Users.Id = UserRoles.UserId
    INNER JOIN AspNetRoles AS Roles ON UserRoles.RoleId = Roles.Id
    INNER JOIN AspNetRoleAPI AS RoleApis ON Roles.Id = RoleApis.RoleId
    INNER JOIN AspNetAPI AS Apis ON RoleApis.ApiId = Apis.Id
go

CREATE OR ALTER PROC sp_TakeRole_From_UserAndApi
    @UserName NVARCHAR(50), -- Use NVARCHAR to match the column type in the database
    @Api NVARCHAR(50)
AS
BEGIN

    SELECT distinct RoleName, UserName, API
    FROM V_TakeRole_From_UserAndApi
    WHERE UserName = @UserName -- Fixed alias and column reference
      AND API = @Api;
END

exec sp_TakeRole_From_UserAndApi 'deeznut2@example.com', '/api/MenuResources'
go

------------------------------------------

create or alter view V_TakePermission_From_UserAndApiAndRole
as
	SELECT Permissions.Name AS Permission, 
        Users.UserName AS UserName, Apis.API AS API, Roles.Name AS RoleName
    FROM AspNetUsers AS Users
    INNER JOIN AspNetUserRoles AS UserRoles ON Users.Id = UserRoles.UserId
    INNER JOIN AspNetRoles AS Roles ON UserRoles.RoleId = Roles.Id
    INNER JOIN AspNetRoleAPI AS RoleApis ON Roles.Id = RoleApis.RoleId
    INNER JOIN AspNetAPI AS Apis ON RoleApis.ApiId = Apis.Id
    INNER JOIN ApiPermission AS ApiPermissions ON RoleApis.Id = ApiPermissions.RoleApiId
    INNER JOIN Permission AS Permissions ON ApiPermissions.PermissionId = Permissions.Id
go

CREATE OR ALTER PROC sp_TakePermission_From_UserAndApiAndRole
    @UserName NVARCHAR(50), -- Use NVARCHAR to match the column type in the database
    @Api NVARCHAR(50)
AS
BEGIN
    SELECT distinct Permission, UserName, API, RoleName
    FROM V_TakePermission_From_UserAndApiAndRole
    WHERE UserName = @UserName -- Fixed alias and column reference
      AND API = @Api
END

EXEC sp_TakePermission_From_UserAndApiAndRole 'deeznut2@example.com', '/api/MenuResources';