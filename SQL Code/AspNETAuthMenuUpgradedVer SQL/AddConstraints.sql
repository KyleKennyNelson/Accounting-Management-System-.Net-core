use db_ab6e43_lkacsoftdb
go


/* =========================================
   ADD FOREIGN-KEY CONSTRAINTS
   ========================================= */

-----------------------------
-- AspNetUserRoles
-----------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetUserRoles_Users')
ALTER TABLE dbo.AspNetUserRoles  WITH CHECK
ADD CONSTRAINT FK_AspNetUserRoles_Users
    FOREIGN KEY (UserId)
    REFERENCES dbo.AspNetUsers (Id)
    ON DELETE CASCADE;   -- optional

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetUserRoles_Roles')
ALTER TABLE dbo.AspNetUserRoles  WITH CHECK
ADD CONSTRAINT FK_AspNetUserRoles_Roles
    FOREIGN KEY (RoleId)
    REFERENCES dbo.AspNetRoles (Id)
    ON DELETE CASCADE;   -- optional


-----------------------------
-- AspNetUserClaims
-----------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetUserClaims_Users')
ALTER TABLE dbo.AspNetUserClaims  WITH CHECK
ADD CONSTRAINT FK_AspNetUserClaims_Users
    FOREIGN KEY (UserId)
    REFERENCES dbo.AspNetUsers (Id)
    ON DELETE CASCADE;


-----------------------------
-- AspNetRoleClaims
-----------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetRoleClaims_Roles')
ALTER TABLE dbo.AspNetRoleClaims  WITH CHECK
ADD CONSTRAINT FK_AspNetRoleClaims_Roles
    FOREIGN KEY (RoleId)
    REFERENCES dbo.AspNetRoles (Id)
    ON DELETE CASCADE;


-----------------------------
-- AspNetUserLogins
-----------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetUserLogins_Users')
ALTER TABLE dbo.AspNetUserLogins  WITH CHECK
ADD CONSTRAINT FK_AspNetUserLogins_Users
    FOREIGN KEY (UserId)
    REFERENCES dbo.AspNetUsers (Id)
    ON DELETE CASCADE;


-----------------------------
-- AspNetUserTokens
-----------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetUserTokens_Users')
ALTER TABLE dbo.AspNetUserTokens  WITH CHECK
ADD CONSTRAINT FK_AspNetUserTokens_Users
    FOREIGN KEY (UserId)
    REFERENCES dbo.AspNetUsers (Id)
    ON DELETE CASCADE;


-----------------------------
-- AspNetRoleMenu   (custom)
-----------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetRoleMenu_Roles')
ALTER TABLE dbo.AspNetRoleMenu  WITH CHECK
ADD CONSTRAINT FK_AspNetRoleMenu_Roles
    FOREIGN KEY (RoleId)
    REFERENCES dbo.AspNetRoles (Id)
    ON DELETE CASCADE;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetRoleMenu_Menu')
ALTER TABLE dbo.AspNetRoleMenu  WITH CHECK
ADD CONSTRAINT FK_AspNetRoleMenu_Menu
    FOREIGN KEY (MenuId)
    REFERENCES dbo.AspNetMenu (Id)
    ON DELETE CASCADE;

-- select r.Name
-- from AspNetRoleMenu rm, AspNetRoles r
-- where rm.RoleId = r.Id
--     and MenuId = 2

-- select *
-- from AspNetRoleMenu

-- select *
-- from AspNetMenu

-----------------------------
-- MenuPermission    (custom)
-----------------------------
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_MenuPermission_RoleMenu')
ALTER TABLE dbo.MenuPermission  WITH CHECK
ADD CONSTRAINT FK_MenuPermission_RoleMenu
    FOREIGN KEY (RoleMenuId)
    REFERENCES dbo.AspNetRoleMenu (Id)
    ON DELETE CASCADE;


IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_MenuPermission_Permission')
ALTER TABLE dbo.MenuPermission  WITH CHECK
ADD CONSTRAINT FK_MenuPermission_Permission
    FOREIGN KEY (PermissionId)
    REFERENCES dbo.Permission (Id)
    ON DELETE CASCADE;

-----------------------------
-- ApiPermission    (custom)
-----------------------------

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ApiPermission_Permission')
ALTER TABLE dbo.ApiPermission  WITH CHECK
ADD CONSTRAINT FK_ApiPermission_Permission
    FOREIGN KEY (PermissionId)
    REFERENCES dbo.Permission (Id)
    ON DELETE CASCADE;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ApiPermission_AspNetRoleAPI')
ALTER TABLE dbo.ApiPermission  WITH CHECK
ADD CONSTRAINT FK_ApiPermission_AspNetRoleAPI
    FOREIGN KEY (RoleApiId)
    REFERENCES dbo.AspNetRoleAPI (Id)
    ON DELETE CASCADE;

-----------------------------
-- AspNetRoleAPI    (custom)
-----------------------------

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetRoleAPI_AspNetRoles')
ALTER TABLE dbo.AspNetRoleAPI  WITH CHECK
ADD CONSTRAINT FK_AspNetRoleAPI_AspNetRoles
    FOREIGN KEY (RoleId)
    REFERENCES dbo.AspNetRoles (Id)
    ON DELETE CASCADE;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_AspNetRoleAPI_AspNetAPI')
ALTER TABLE dbo.AspNetRoleAPI  WITH CHECK
ADD CONSTRAINT FK_AspNetRoleAPI_AspNetAPI
    FOREIGN KEY (ApiId)
    REFERENCES dbo.AspNetAPI (Id)
    ON DELETE CASCADE;

go