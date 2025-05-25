use db_ab6e43_lkacsoftdb
go

insert into dbo.AspNetRoles(Id,Name,NormalizedName)
values(NEWID(),'Admin','ADMIN'),
(NEWID(),'Truong phong ke toan','TRUONG PHONG KE TOAN'),
(NEWID(),'Nhan vien ke toan','NHAN VIEN KE TOAN'),
(NEWID(),'Truong phong hanh chinh-giao nhan','TRUONG PHONG HANH CHINH-GIAO NHAN'),
(NEWID(),'Nhan vien hanh chinh','NHAN VIEN HANH CHINH'),
(NEWID(),'Nhan vien giao nhan','NHAN VIEN GIAO NHAN'),
(NEWID(),'Truong phong nhan su','TRUONG PHONG NHAN SU'),
(NEWID(),'Nhan vien nhan su','NHAN VIEN NHAN SU'),
(NEWID(),'Truong phong cskh','TRUONG PHONG CSKH'),
(NEWID(),'Nhan vien cskh','NHAN VIEN CSKH');
