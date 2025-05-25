Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_V_DetailCustomer
as
BEGIN
    Select *
    From V_DetailCustomers
END
go

Create or Alter Proc sp_GetByID_V_DetailCustomer
@customerCode VARCHAR(255)
as
BEGIN
    Select *
    From V_DetailCustomers
    Where code = @customerCode
END
go