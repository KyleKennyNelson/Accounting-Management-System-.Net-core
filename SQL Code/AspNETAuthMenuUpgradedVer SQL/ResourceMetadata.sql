INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES
  (NEWID(), 'NVGIAONHAN', 'NVGIAONHAN', NEWID()),
  (NEWID(), 'NVHANHCHINH', 'NVHANHCHINH', NEWID()),
  (NEWID(), 'NVKETOAN', 'NVKETOAN', NEWID()),
  (NEWID(), 'TRUONGNHOMKT', 'TRUONGNHOMKT', NEWID()),
  (NEWID(), 'TRUONGPHONGKT', 'TRUONGPHONGKT', NEWID());

INSERT INTO [dbo].[AspNetMenu] ([Title], [Description], [ParentId], [Icon], [Url]) VALUES
(N'Tác vụ', 'Task management section', NULL, 'tasks-icon', '/Tasks'),
(N'Khách hàng', 'List of customers', NULL, 'customers-icon', '/Customers'),
(N'Luồng công việc', 'Process execution tracking', NULL, 'process-icon', '/Processes');

INSERT INTO [dbo].[AspNetRoleMenu] ([RoleId], [MenuId])
SELECT
    r.[Id],
    m.[Id]
FROM
    [dbo].[AspNetRoles] r
CROSS JOIN
    [dbo].[AspNetMenu] m;

INSERT INTO [dbo].[Permission]
VALUES  ('R'),
        ('RW');

INSERT INTO [dbo].[MenuPermission] ([RoleMenuId], [PermissionId])
SELECT
    rm.[Id] AS RoleMenuId,
    p.[Id] AS PermissionId
FROM
    [dbo].[AspNetRoleMenu] rm
CROSS JOIN
    [dbo].[Permission] p;