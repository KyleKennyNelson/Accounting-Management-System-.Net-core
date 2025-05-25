use db_ab6e43_lkacsoftdb
go

create or alter view V_Menu
as
	select id menuID, Title menuName, ParentId menuParent
    from AspNetMenu
go

create or alter view V_Role_Menu
as
	select distinct anr.Name roleName, anrm.MenuId menuID, anrm.Id rolemenuID, p.Id permissionID
    from AspNetRoles anr, AspNetRoleMenu anrm, Permission p, MenuPermission mp
    where anr.id = anrm.RoleId
        and p.Id = mp.PermissionId
        and mp.RoleMenuId = anrm.Id
go

create or alter view V_MenuRolePerm
as
	select distinct anr.Name roleName, anrm.MenuId menuID, anrm.Id rolemenuID, p.Id permissionID, anm.Title menuName
    from AspNetRoles anr, AspNetRoleMenu anrm, Permission p, MenuPermission mp, AspNetMenu anm
    where anr.id = anrm.RoleId
        and p.Id = mp.PermissionId
        and mp.RoleMenuId = anrm.Id
        and anm.Id = anrm.MenuId
go

create or alter view V_Role
as
    SELECT 
        anr.Id AS RoleID, 
        anr.Name AS RoleName, 
        COUNT(anu.Id) AS UserCount
    FROM 
        AspNetRoles anr
    LEFT JOIN 
        AspNetUserRoles anur ON anr.Id = anur.RoleId
    LEFT JOIN 
        AspNetUsers anu ON anur.UserId = anu.Id
    GROUP BY 
        anr.Id, anr.Name;
go

create or alter view V_Permission_RoleMenu
as
	select distinct mp.RoleMenuId rolemenuID, p.Name permissionName, p.id permissionID
    from MenuPermission mp, Permission p
    where mp.PermissionId = p.Id
go

create or alter view V_RoleMenuPerm
as
	select distinct 
        vrole.RoleID, vrole.RoleName, UserCount, anm.Id MenuID, anm.Title MenuName, p.Id PermID, p.Name PermName
    from 
        V_Role vrole inner join AspNetRoleMenu anrm on vrole.RoleID = anrm.RoleId
                    inner join AspNetMenu anm on anrm.MenuId = anm.Id
                    inner join MenuPermission mp on anrm.Id = mp.RoleMenuId
                    inner join Permission p on mp.PermissionId = p.Id
go

create or alter view V_User_Role
as
	select distinct users.UserName UserName, roles.Name RoleName
    from AspNetUsers users, AspNetUserRoles userRoles, AspNetRoles roles
    where users.Id = userRoles.UserId
        and userRoles.RoleId = roles.Id
go

create or alter view V_User
as
	select distinct Id, UserName, Email, EmailConfirmed as EmailVerified, PhoneNumber, PhoneNumberConfirmed as PhoneVerified, LockoutEnabled as Locked
    from AspNetUsers
go

create or alter view V_UserId_RoleId
as

	select distinct anu.Id UserId, anur.RoleId, anr.Name RoleName
    from AspNetUsers anu, AspNetUserRoles anur, AspnetRoles anr
    where anu.Id = anur.UserId
        and anr.Id = anur.RoleId
go

Create Or Alter Proc sp_TakeRoleFromUser
    @UserName nvarchar(50)
AS
BEGIN
    Select UserName, RoleName Role
    From V_User_Role
    Where UserName = @UserName
END

exec sp_TakeRoleFromUser 'deeznut2@example.com';
go

Create Or Alter Proc sp_InsertRoleMenuPerms
    @RoleID nvarchar(50),
    @MenuID int,
    @PermID int
AS
BEGIN

    -- Check if the record already exists
    IF EXISTS (
        SELECT 1
        FROM AspNetRoleMenu anrm, MenuPermission mp
        WHERE anrm.MenuId = @MenuID
          AND anrm.RoleId = @RoleID
          AND anrm.Id = mp.RoleMenuId
          AND mp.PermissionId = @PermID
    ) 
    BEGIN
        -- Return a message if the record exists
        PRINT 'This role already exists'
        RETURN
    END

    -- Delete 'No' row
    delete from MenuPermission 
    where RoleMenuId = (select Id
                        from AspNetRoleMenu 
                        where MenuId = @MenuID
                            and RoleId = @RoleID)
        and PermissionId = (select Id from Permission where Name = 'No')

    Insert into MenuPermission (RoleMenuId, PermissionId)
    values ((select Id 
            from AspNetRoleMenu 
            where MenuId = @MenuID
                and RoleId = @RoleID), @PermID)
END
go

Create Or Alter Proc sp_DeleteRoleMenuPerms
    @RoleID nvarchar(50),
    @MenuID int,
    @PermID int
AS
BEGIN

    IF EXISTS (
        SELECT 1
        FROM AspNetRoleMenu
        WHERE RoleId = @RoleID AND MenuId = @MenuID
    )
    begin
        delete from MenuPermission 
        where RoleMenuId = (select Id
                            from AspNetRoleMenu 
                            where MenuId = @MenuID
                                and RoleId = @RoleID)
            and PermissionId = @PermID
    end

    IF not EXISTS (
        SELECT 1
        FROM AspNetRoleMenu anrm, MenuPermission mp
        WHERE anrm.MenuId = @MenuID
          AND anrm.RoleId = @RoleID
          AND anrm.Id = mp.RoleMenuId
    ) 
    BEGIN
        Insert into MenuPermission (RoleMenuId, PermissionId)
        values ((select Id 
                from AspNetRoleMenu 
                where MenuId = @MenuID
                    and RoleId = @RoleID),
                (select Id
                from Permission
                where Name = 'No'))
    END
END
go

Create Or Alter Proc sp_UpdateRoleMenuPerms
    @RoleID nvarchar(50),
    @MenuID int,
    @PermID int
AS
BEGIN
    Update MenuPermission
    Set PermissionId = @PermID
    From AspNetRoleMenu anrm
    Where anrm.MenuId = @MenuID
        and anrm.RoleId = @RoleID
        and anrm.Id = RoleMenuId
END

--exec sp_UpdateRoleMenuPerms '2077F726-A824-4785-A394-010BB886CE69', 1, 2;