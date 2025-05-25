Use db_ab6e43_lkacsoftdb
go

insert into dbo.AspNetRoleMenu(RoleId, MenuId)
VALUES 
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'Home')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'Header')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'Table Button')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'Order Button')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'Navbar')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'TableCollaspsibleList')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'FoodCollaspsibleList')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'MainCanvas')),
((select Id from dbo.AspNetRoles where Name = 'Manager'),(select Id from dbo.AspNetMenu where Title = 'PropertiesSideBar'));

insert into dbo.AspNetRoleMenu(RoleId, MenuId)
VALUES 
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'Home')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'Header')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'Table Button')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'Order Button')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'Navbar')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'TableCollaspsibleList')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'FoodCollaspsibleList')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'MainCanvas')),
((select Id from dbo.AspNetRoles where Name = 'Staff'),(select Id from dbo.AspNetMenu where Title = 'PropertiesSideBar'));

insert into dbo.AspNetRoleMenu(RoleId, MenuId)
VALUES 
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'Home')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'Header')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'Table Button')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'Order Button')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'Navbar')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'TableCollaspsibleList')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'FoodCollaspsibleList')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'MainCanvas')),
((select Id from dbo.AspNetRoles where Name = 'Customer'),(select Id from dbo.AspNetMenu where Title = 'PropertiesSideBar'));

--delete from dbo.AspNetRoleMenu