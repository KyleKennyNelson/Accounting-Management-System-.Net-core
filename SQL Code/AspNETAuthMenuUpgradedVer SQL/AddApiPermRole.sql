Use db_ab6e43_lkacsoftdb
go

-- DELETE FROM Permission;
-- DBCC CHECKIDENT ('Permission', RESEED, 0);

-- DELETE FROM AspNetAPI;
-- DBCC CHECKIDENT ('AspNetAPI', RESEED, 0);

-- DELETE FROM AspNetRoleAPI;
-- DBCC CHECKIDENT ('AspNetRoleAPI', RESEED, 0);

-- DELETE FROM ApiPermission;
-- go


INSERT INTO AspNetAPI (API) 
VALUES
('/api/accountantteams'),
('/api/accountingstatuses'),
('/api/archivingstatuses'),
('/api/detailcustomers'),
('/api/detaildocumenttypes'),
('/api/documenttypes'),
('/api/detailexecutions'),
('/api/detailtasks'),
('/api/executions'),
('/api/jobtaskfiles'),
('/api/priorities'),
('/api/departments'),
('/api/processstatuses'),
('/api/processschemas'),
('/api/processschemastatuses'),
('/api/s3service'),
('/api/s3service/DowloadFile'),
('/api/tasks'),
('/api/tasks/UpdateTaskStatus'),
('/api/tasks/GetAmountOfRetriedTasks'),
('/api/tasks/GetTaskVisualization'),
('/api/tasks/GetTaskAverageCompletionTimePerQuarter'),
('/api/taskstatuses'),
('/api/tasktypes'),
('/api/users'),
('/api/users/GetUserByTeamID'),
('/api/users/Avatar'),
('/api/detailusers'),
('/api/detailusers/GetUserInfor'),
('/api/Menu'),
('/api/MenuResources'),
('/api/Permission'),
('/api/Role'),
('/api/RoleMenuPerm'),
('/api/User');
go

-- Insert into Permission (Name)
-- values
-- ('R'),
-- ('RW'),
-- ('GET'),
-- ('POST'),
-- ('PUT'),
-- ('DELETE');
-- GO

-- select C.Name, B.API, count(B.API)
-- from AspNetRoleAPI A, AspNetAPI B, AspNetRoles C
--     where A.ApiId = B.Id
--         AND C.Id = A.RoleId
--         --and b.API = '/api/jobtaskfiles'
--         and C.Name = 'TRUONGPHONGKT'
--         group by C.Name, B.API
--         having count(B.API) > 1
-- go

-------------------------
--Insert into RoleAPI
-------------------------


INSERT INTO AspNetRoleAPI (RoleId, ApiId)
Select anr.Id, ana.Id 
from AspNetRoles anr
    join AspNetAPI ana on 1=1
where ana.API IN (
      '/api/users/Avatar',
      '/api/tasks',
      '/api/detailtasks',
      '/api/priorities',
      '/api/departments',
      '/api/tasks/UpdateTaskStatus',
      '/api/taskstatuses',
      '/api/tasktypes',
      '/api/detailcustomers',
      '/api/detailusers/GetUserInfor',
      '/api/MenuResources'
  );

INSERT INTO AspNetRoleAPI (RoleId, ApiId)
Select anr.Id, ana.Id 
from AspNetRoles anr
    join AspNetAPI ana on 1=1
where anr.Name != 'NVGIAONHAN'
    AND ana.API IN (
      '/api/jobtaskfiles',
      '/api/s3service',
      '/api/s3service/DowloadFile',
      '/api/accountingstatuses',
      '/api/archivingstatuses',
      '/api/detaildocumenttypes',
      '/api/documenttypes',
      '/api/processschemas',
      '/api/processschemastatuses'
  );

INSERT INTO AspNetRoleAPI (RoleId, ApiId)
Select anr.Id, ana.Id 
from AspNetRoles anr
    join AspNetAPI ana on 1=1
where (anr.Name = 'TRUONGPHONGKT' 
    OR anr.Name = 'TRUONGNHOMKT')
    AND ana.API IN (
      '/api/users/GetUserByTeamID',
      '/api/accountantteams',
      '/api/tasks/GetAmountOfRetriedTasks',
      '/api/tasks/GetTaskVisualization',
      '/api/tasks/GetTaskAverageCompletionTimePerQuarter'
  );

INSERT INTO AspNetRoleAPI (RoleId, ApiId)
Select anr.Id, ana.Id 
from AspNetRoles anr
    join AspNetAPI ana on 1=1
where anr.Name = 'TRUONGPHONGKT'
    AND ana.API IN (
      '/api/detailexecutions',
      '/api/executions',
      '/api/processstatuses',
      '/api/users',
      '/api/detailusers',
      '/api/users',
      '/api/detailusers'
  );

INSERT INTO AspNetRoleAPI (RoleId, ApiId)
Select anr.Id, ana.Id 
from AspNetRoles anr
    join AspNetAPI ana on 1=1
where anr.Name = 'NVHANHCHINH'
    AND ana.API IN (
        '/api/Menu',
        '/api/Permission',
        '/api/Role',
        '/api/RoleMenuPerm',
        '/api/User',
        '/api/users',
        '/api/detailusers'
  );

go


-------------------------
--Insert into RoleAPIPermission
-------------------------

----------
--ADMIN SECTION

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'GET'
    and anr.Name = 'NVHANHCHINH'
    AND ana.API IN (
        '/api/Menu',
        '/api/Permission',
        '/api/Role',
        '/api/RoleMenuPerm',
        '/api/User'
  );

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'GET'
    and anr.Name IN (
        'NVHANHCHINH',
        'TRUONGPHONGKT'
    )
    AND ana.API IN (
        '/api/users',
        '/api/detailusers'
  );

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where (p.Name = 'POST'
    or p.Name = 'PUT'
    or p.Name = 'DELETE')
    and anr.Name = 'NVHANHCHINH'
    AND ana.API = '/api/RoleMenuPerm';

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where (p.Name = 'POST'
    or p.Name = 'DELETE')
    and anr.Name = 'NVHANHCHINH'
    AND ana.API = '/api/User';
go




--USER SECTION

------------ Permission: get

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'GET'
    AND ana.API NOT IN (
        '/api/Menu',
        '/api/Permission',
        '/api/Role',
        '/api/RoleMenuPerm',
        '/api/User',
        '/api/users',
        '/api/detailusers'
  );
go

------------ Permission: PUT

--------api: accountantteams

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'PUT'
    and ana.API = '/api/accountantteams'
    and anr.Name = 'TRUONGPHONGKT'


--------api: detaildocumenttypes

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'PUT'
    and ana.API = '/api/detaildocumenttypes'
    and anr.Name = 'TRUONGPHONGKT'


--------api: jobtaskfiles

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'PUT'
    and ana.API = '/api/jobtaskfiles'
    and anr.Name IN 
        (
        'NVKETOAN',
        'TRUONGPHONGKT',
        'TRUONGNHOMKT',
        'NVHANHCHINH'
    )


--------api: s3service

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'PUT'
    and ana.API = '/api/s3service'
    and anr.Name IN 
        (
        'NVKETOAN',
        'TRUONGPHONGKT',
        'TRUONGNHOMKT',
        'NVHANHCHINH'
    )


--------api: tasks

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'PUT'
    and ana.API = '/api/tasks'
    and anr.Name IN 
        (
        'TRUONGPHONGKT',
        'TRUONGNHOMKT'
    )


--------api: tasks/UpdateTaskStatus

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'PUT'
    and ana.API = '/api/tasks/UpdateTaskStatus'


--------api: users/Avatar

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'PUT'
    and ana.API = '/api/users/Avatar'

go


------------ Permission: POST


--------api: detaildocumenttypes

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'POST'
    and ana.API = '/api/detaildocumenttypes'
    and anr.Name = 'TRUONGPHONGKT'


--------api: executions

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'POST'
    and ana.API = '/api/executions'
    and anr.Name = 'TRUONGPHONGKT'


--------api: jobtaskfiles

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'POST'
    and ana.API = '/api/jobtaskfiles'
    and anr.Name IN 
        (
        'NVKETOAN',
        'TRUONGPHONGKT',
        'TRUONGNHOMKT',
        'NVHANHCHINH'
    );


--------api: s3service

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'POST'
    and ana.API = '/api/s3service'
    and anr.Name IN 
        (
        'NVKETOAN',
        'TRUONGPHONGKT',
        'TRUONGNHOMKT',
        'NVHANHCHINH'
    );


--------api: tasks

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'POST'
    and ana.API = '/api/tasks'
    and anr.Name IN 
        (
        'TRUONGPHONGKT',
        'TRUONGNHOMKT'
        );

--------api: users

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'POST'
    and ana.API = '/api/users'
    and anr.name IN (
        'TRUONGPHONGKT',
        'NVHANHCHINH'
    )


--------api: users/Avatar

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'POST'
    and ana.API = '/api/users/Avatar'

go



------------ Permission: DELETE


--------api: detaildocumenttypes

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'DELETE'
    and ana.API = '/api/detaildocumenttypes'
    and anr.Name = 'TRUONGPHONGKT'


--------api: jobtaskfiles

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'DELETE'
    and ana.API = '/api/jobtaskfiles'
    and anr.Name IN 
        (
        'NVKETOAN',
        'TRUONGPHONGKT',
        'TRUONGNHOMKT',
        'NVHANHCHINH'
    );


--------api: s3service

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'DELETE'
    and ana.API = '/api/s3service'
    and anr.Name IN 
        (
        'NVKETOAN',
        'TRUONGPHONGKT',
        'TRUONGNHOMKT',
        'NVHANHCHINH'
    );


--------api: tasks

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'DELETE'
    and ana.API = '/api/tasks'
    and anr.Name IN 
        (
        'TRUONGPHONGKT',
        'TRUONGNHOMKT'
    );


--------api: users/Avatar

INSERT INTO ApiPermission (RoleApiId, PermissionId)
Select anra.Id, p.Id
from AspNetRoleAPI anra
    left join AspNetRoles anr
        on anra.RoleId = anr.Id
    left join AspNetApi ana
        on anra.ApiId = ana.Id
    join Permission p 
        on 1=1
where p.Name = 'DELETE'
    and ana.API = '/api/users/Avatar'

go