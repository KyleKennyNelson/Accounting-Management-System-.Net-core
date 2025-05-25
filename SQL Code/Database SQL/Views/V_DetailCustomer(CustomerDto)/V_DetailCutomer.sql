use db_ab6e43_lkacsoftdb
go

--drop table V_DetailCustomers

Create or Alter View V_DetailCustomers
AS
    Select
        -- Columns from LKACSoft_Customer (Main Table)
        C.*,

        -- Columns from LkacSoft_User (Main Accountant)
        mainAccountant.ID AS MainAccountantID,
        mainAccountant.Username AS MainAccountantUsername,
        mainAccountant.Firstname AS MainAccountantFirstname,
        mainAccountant.Lastname AS MainAccountantLastname,
        mainAccountant.Avatar AS MainAccountantAvatar,
        mainAccountant.Address AS MainAccountantAddress,
        mainAccountant.District AS MainAccountantDistrict,
        mainAccountant.Dob AS MainAccountantDob,
        mainAccountant.IsQuitJob AS MainAccountantIsQuitJob,
        mainAccountant.DateCreate AS MainAccountantDateCreate,
        mainAccountant.Team AS MainAccountantTeam,

        -- Columns from LkacSoft_User (Created By)
        createdBy.ID AS CreatedByID,
        createdBy.Username AS CreatedByUsername,
        createdBy.Firstname AS CreatedByFirstname,
        createdBy.Lastname AS CreatedByLastname,
        createdBy.Avatar AS CreatedByAvatar,
        createdBy.Address AS CreatedByAddress,
        createdBy.District AS CreatedByDistrict,
        createdBy.Dob AS CreatedByDob,
        createdBy.IsQuitJob AS CreatedByIsQuitJob,
        createdBy.DateCreate AS CreatedByDateCreate,
        createdBy.Team AS CreatedByTeam,

        -- Columns from LKACSoft_User (Assigned To Customer Support)
        assignedToCustomerSupport.ID AS AssignedSupportID,
        assignedToCustomerSupport.Username AS AssignedSupportUsername,
        assignedToCustomerSupport.Firstname AS AssignedSupportFirstname,
        assignedToCustomerSupport.Lastname AS AssignedSupportLastname,
        assignedToCustomerSupport.Avatar AS AssignedSupportAvatar,
        assignedToCustomerSupport.Address AS AssignedSupportAddress,
        assignedToCustomerSupport.District AS AssignedSupportDistrict,
        assignedToCustomerSupport.Dob AS AssignedSupportDob,
        assignedToCustomerSupport.IsQuitJob AS AssignedSupportIsQuitJob,
        assignedToCustomerSupport.DateCreate AS AssignedSupportDateCreate,
        assignedToCustomerSupport.Team AS AssignedSupportTeam,

        -- Columns from LKACSoft_User (Responsible AccountantTeam)
        responsibleAccountantTeam.TeamID AS AccountantTeamID,
        responsibleAccountantTeam.TeamName AS AccountantTeamName,
        responsibleAccountantTeam.TeamLeader AS AccountantTeamLeader,

        -- Columns from LKACSoft_Department
        D.Code AS DepartmentCode,
        D.Name AS DepartmentName,
        D.DisplayOrder AS DepartmentDisplayOrder,
        D.Closed AS DepartmentClosed


    from LKACSoft_Customer C
        Left join LkacSoft_User mainAccountant
            on C.MainAccountant = mainAccountant.ID
        Left join LkacSoft_User createdBy
            on C.CreatedBy = createdBy.ID
        Left join LKACSoft_User assignedToCustomerSupport
            on C.AssignedToCustomerSupport = assignedToCustomerSupport.ID
        Left join LKACSoft_AccountantTeam responsibleAccountantTeam
            on C.ResponsibleAccountantTeam = responsibleAccountantTeam.TeamID
        Left join LKACSoft_Department D
            on C.LKACSoft_DepartmentCode = D.Code
go

-- select *
-- from V_DetailCustomers

-- SELECT OBJECT_TYPE = o.type_desc
-- FROM sys.objects o
-- WHERE o.name = 'V_DetailCustomers';