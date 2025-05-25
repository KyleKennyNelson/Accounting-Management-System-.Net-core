Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_V_DetailExecution
as
BEGIN
    Select *
    From V_DetailExecutions
END
go

-- DROP proc sp_GetAll_V_DetailExecution
-- go

Create or Alter Proc sp_GetByID_V_DetailExecution
@executionID VARCHAR(255)
as
BEGIN
    Select *
    From V_DetailExecutions
    Where executionID = @executionID
END
go

-- drop proc sp_GetByID_V_DetailDetailExecution
-- go