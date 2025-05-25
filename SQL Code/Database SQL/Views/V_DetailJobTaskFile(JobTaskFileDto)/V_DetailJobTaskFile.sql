use db_ab6e43_lkacsoftdb
go

--drop view V_DetailJobTaskFiles

Create or Alter View V_DetailJobTaskFiles
AS
    Select
        -- Columns from LKACSoft_JobTaskFile (Main Table)
        JTF.Code as JobTaskFileCode,
        JTF.[FileName],
        JTF.FileType,
        JTF.CreatedAt,
        JTF.AccountantReceivedAt,
        JTF.ReadyToBeReturnedAt,
        JTF.PhysicalLocation,
        JTF.ArchivedDate,
        JTF.ReturnedDate,

        -- Columns from LKACSoft_AccountingStatus (Accounting Status)
        accountingStatus.ID as AccountingStatusID,
        accountingStatus.Name as AccountingStatusName,

        -- Columns from LKACSoft_ArchivingStatus (Archiving Status)
        archivingStatus.ID as ArchivingStatusID,
        archivingStatus.Name as ArchivingStatusName,

        -- Columns from LkacSoft_User (Accountant)
        accountant.ID AS accountantID,
        accountant.Username AS accountantUsername,
        accountant.Firstname AS accountantFirstname,
        accountant.Lastname AS accountantLastname,
        accountant.Avatar AS accountantAvatar,
        accountant.Address AS accountantAddress,
        accountant.District AS accountantDistrict,
        accountant.Dob AS accountantDob,
        accountant.IsQuitJob AS accountantIsQuitJob,
        accountant.DateCreate AS accountantDateCreate,
        accountant.Team AS accountantTeam,

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

        -- Columns from LKACSoft_Execution
        LKACSoft_Execution.ExecutionID,
        LKACSoft_Execution.ExecutionName,
        LKACSoft_Execution.CreatedBy AS CreatedByForExecution,
        LKACSoft_Execution.DateCreated,
        LKACSoft_Execution.IsPeriodic,

        -- Columns from LKACSoft_CustomerDocumentType
        CDT.DocumentTypeID,

        -- Columns from LKACSoft_DocumentType
        DT.DocumentTypeName,
        CDT.DocumentReceivingMechanism,
        CDT.AvgAmount,
        CDT.RegisteredAmount,
        --CDT.CurrentTotalAmount,

        --Columns from LKACSoft_Customer
        C.*


    from LKACSoft_JobTaskFile JTF
        Left join LkacSoft_AccountingStatus accountingStatus
            on JTF.AccountingStatus = accountingStatus.ID
        Left join LkacSoft_ArchivingStatus archivingStatus
            on JTF.ArchivingStatus = archivingStatus.ID
        Left join LkacSoft_User Accountant
            on JTF.AccountantID = accountant.ID
        Left join LKACSoft_User createdBy
            on JTF.CreatedBy = createdBy.ID
        Left join LKACSoft_Execution
            on JTF.RelatedToExecution = ExecutionID
        Left join LKACSoft_CustomerDocumentType CDT
            on JTF.DocumentType = CDT.DocumentTypeID
            and JTF.CustomerCode = CDT.CustomerCode
        Left join LKACSoft_DocumentType DT
            on JTF.DocumentType = DT.DocumentTypeID
        Left join LKACSoft_Customer C
            on JTF.CustomerCode = C.Code
        
go

-- SELECT OBJECT_TYPE = o.type_desc
-- FROM sys.objects o
-- WHERE o.name = 'V_DetailCustomers';