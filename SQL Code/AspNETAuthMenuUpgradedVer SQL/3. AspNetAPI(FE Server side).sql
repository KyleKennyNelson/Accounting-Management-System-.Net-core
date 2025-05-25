use db_ab6e43_lkacsoftdb
go

insert into AspNetAPI (API)
VALUES 
('/api/canva'),
('/api/FoodType'),
('/api/MenuResources'),
('/api/ShapeType'),
('/api/TableFoods');



insert into dbo.AspNetRoleAPI(RoleId, ApiId)
VALUES 
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetAPI where API = '/api/canva')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetAPI where API = '/api/FoodType')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetAPI where API = '/api/MenuResources')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetAPI where API = '/api/ShapeType')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetAPI where API = '/api/TableFoods'));

insert into dbo.AspNetRoleAPI(RoleId, ApiId)
VALUES 
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetAPI where API = '/api/canva')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetAPI where API = '/api/FoodType')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetAPI where API = '/api/MenuResources')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetAPI where API = '/api/ShapeType')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetAPI where API = '/api/TableFoods'));

insert into dbo.AspNetRoleAPI(RoleId, ApiId)
VALUES 
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetAPI where API = '/api/canva')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetAPI where API = '/api/FoodType')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetAPI where API = '/api/MenuResources')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetAPI where API = '/api/ShapeType')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetAPI where API = '/api/TableFoods'));


--delete from dbo.AspNetRoleMenu

insert into dbo.ApiPermission(RoleApiId,PermissionId)
values ((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/canva'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/canva'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/FoodType'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/FoodType'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/MenuResources'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/MenuResources'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/ShapeType'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/ShapeType'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/TableFoods'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Manager' and api.API = '/api/TableFoods'), (select Id from dbo.Permission where Name = 'ReadWrite'));

insert into dbo.ApiPermission(RoleApiId,PermissionId)
values ((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Staff' and api.API = '/api/canva'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Staff' and api.API = '/api/FoodType'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Staff' and api.API = '/api/MenuResources'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Staff' and api.API = '/api/ShapeType'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Staff' and api.API = '/api/TableFoods'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Staff' and api.API = '/api/TableFoods'), (select Id from dbo.Permission where Name = 'ReadWrite'));

insert into dbo.ApiPermission(RoleApiId,PermissionId)
values ((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Customer' and api.API = '/api/canva'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Customer' and api.API = '/api/FoodType'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Customer' and api.API = '/api/MenuResources'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Customer' and api.API = '/api/ShapeType'), (select Id from dbo.Permission where Name = 'Read')),
((select RAPI.Id from dbo.AspNetRoleAPI as RAPI
inner join dbo.AspNetAPI as api on RAPI.ApiId = api.Id
inner join dbo.AspNetRoles as R on R.Id = RAPI.RoleId
where R.Name = 'Customer' and api.API = '/api/TableFoods'), (select Id from dbo.Permission where Name = 'Read'));

--delete from dbo.MenuPermission;