Use db_ab6e43_lkacsoftdb
go

Create or Alter Proc sp_GetAll_V_DetailTask
@UserID VARCHAR(255),
@IsManager BIT
as
BEGIN
    if (@IsManager = 1)
    BEGIN
        Select *
        From V_DetailTasks

        return
    END

    Select *
    From V_DetailTasks
    Where AssignedTo = @UserID
END
go

Create or Alter Proc sp_GetByID_V_DetailTask
@UserID VARCHAR(255),
@TaskID VARCHAR(255),
@IsManager BIT
as
BEGIN
    if (@IsManager = 1)
    BEGIN
        Select *
        From V_DetailTasks
        Where TaskID = @TaskID

        return
    END

    Select *
    From V_DetailTasks
    Where AssignedTo = @UserID
        And TaskID = @TaskID
END
go