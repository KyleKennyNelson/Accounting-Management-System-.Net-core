use db_ab6e43_lkacsoftdb
go

insert into dbo.MenuPermission(RoleMenuId,PermissionId)
values ((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Home'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Home'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Header'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Header'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Table Button'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Table Button'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Order Button'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Order Button'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Navbar'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'Navbar'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'TableCollaspsibleList'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'TableCollaspsibleList'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'FoodCollaspsibleList'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'FoodCollaspsibleList'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'MainCanvas'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'MainCanvas'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'PropertiesSideBar'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Manager' and M.Title = 'PropertiesSideBar'), (select Id from dbo.Permission where Name = 'ReadWrite'));

insert into dbo.MenuPermission(RoleMenuId,PermissionId)
values ((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'Home'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'Header'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'Table Button'), (select Id from dbo.Permission where Name = 'No')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'Order Button'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'Navbar'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'FoodCollaspsibleList'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'FoodCollaspsibleList'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'MainCanvas'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'MainCanvas'), (select Id from dbo.Permission where Name = 'ReadWrite')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'PropertiesSideBar'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Staff' and M.Title = 'PropertiesSideBar'), (select Id from dbo.Permission where Name = 'ReadWrite'));

insert into dbo.MenuPermission(RoleMenuId,PermissionId)
values ((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'Home'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'Header'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'Table Button'), (select Id from dbo.Permission where Name = 'No')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'Order Button'), (select Id from dbo.Permission where Name = 'No')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'Navbar'), (select Id from dbo.Permission where Name = 'No')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'TableCollaspsibleList'), (select Id from dbo.Permission where Name = 'No')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'FoodCollaspsibleList'), (select Id from dbo.Permission where Name = 'No')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'MainCanvas'), (select Id from dbo.Permission where Name = 'Read')),
((select RM.Id from dbo.AspNetRoleMenu as RM
inner join dbo.AspNetMenu as M on RM.MenuId = M.Id
inner join dbo.AspNetRoles as R on R.Id = RM.RoleId
where R.Name = 'Customer' and M.Title = 'PropertiesSideBar'), (select Id from dbo.Permission where Name = 'Read'));

--delete from dbo.MenuPermission;