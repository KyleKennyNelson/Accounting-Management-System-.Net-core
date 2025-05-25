Use db_ab6e43_lkacsoftdb
go

Create or Alter View V_ApiPermissionRole
as
    Select ana.API as API, p.Name as Permission, anr.Name as Role
    from ApiPermission rap
        left join AspNetRoleAPI anra
            on rap.RoleApiId = anra.Id
        left join AspNetRoles anr
            on anra.RoleId = anr.Id
        left join AspNetApi ana
            on anra.ApiId = ana.Id
        left join Permission p
            on rap.PermissionId = p.Id

go

-- exec sp_Get_Roles_InApiPermission_By_API_Perms '/api/detailusers/5242', 'GET';
-- go

-- drop proc sp_Get_Roles_InApiPermission_By_API_Perm
-- go

Create or Alter Proc sp_Get_Roles_InApiPermission_By_API_Perm
@ApiName             NVARCHAR(255) = NULL,
@PermissionName      NVARCHAR(50) = NULL
as
Begin

    --  -- Check for null values in all parameters
    -- IF @ApiName IS NULL or @PermissionName IS NULL
    -- BEGIN
    --     print 'ApiID or PermissionID should be passed.';
    --     RETURN;
    -- END

    -- IF NOT EXISTS (
    --     SELECT 1 
    --     FROM AspNetAPI
    --     WHERE Api = @ApiName
    -- )
    -- BEGIN
    --     print 'This Api does not exist';
    --     RETURN;
    -- END

    -- IF NOT EXISTS (
    --     SELECT 1 
    --     FROM Permission
    --     WHERE Name = @PermissionName
    -- )
    -- BEGIN
    --     print 'This Permission does not exist';
    --     RETURN;
    -- END

    -- Step 1: Find the best matching API
    DECLARE @BestMatchAPI NVARCHAR(255);

    SELECT TOP 1 @BestMatchAPI = API
    FROM V_ApiPermissionRole
    WHERE @ApiName LIKE API + '%'
    ORDER BY LEN(API) DESC;

    Declare @LeftString varchar(255);
    set @LeftString = RIGHT(@ApiName, LEN(@ApiName) - LEN(@BestMatchAPI))

    If not Exists ( SELECT 1 
                    WHERE LEFT(@LeftString, 1) = '/'
                            OR @LeftString = '')
    Begin
        Set @BestMatchAPI = null
    End

    -- Step 2: If found, return all matching roles

    SELECT *
    FROM V_ApiPermissionRole
    WHERE API = @BestMatchAPI
      AND Permission = @PermissionName;

END
GO
