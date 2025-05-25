use db_ab6e43_lkacsoftdb
go

--drop view V_DetailDocumentTypes

-- SELECT *
-- FROM LkacSoft_JobTaskFile
-- go

-- update LkacSoft_JobTaskFile
-- Set ArchivingStatus = 2
-- Where Code = 'F017'
-- go

Create or Alter View V_DetailDocumentTypes
AS
    Select
        -- Columns from LKACSoft_CustomerDocumentType(Main Table)
        CDT.DocumentTypeID,

        -- Columns from LKACSoft_DocumentType
        DT.DocumentTypeName,
        CDT.DocumentReceivingMechanism,
        CDT.AvgAmount,
        CDT.RegisteredAmount,
        Count(JTF.Code) AS CurrentTotalAmount,

        --Columns from LKACSoft_Customer
        C.*,

        --ArchivedAmount
        SUM(CASE WHEN LAS.Name = N'Đang lưu trữ' THEN 1 ELSE 0 END) AS ArchivedAmount,

        --LendAmount
        SUM(CASE WHEN DLH.LendStatus = N'Bị mất' THEN 1 ELSE 0 END) AS LendAmount,

        --LostAmount
        SUM(CASE WHEN LAS.Name = N'Bị mất' THEN 1 ELSE 0 END) AS LostAmount


    from LKACSoft_CustomerDocumentType CDT
        Left join LkacSoft_DocumentType DT
            on CDT.DocumentTypeID = DT.DocumentTypeID
        Left join LkacSoft_JobTaskFile JTF
            on CDT.DocumentTypeID = JTF.DocumentType
            and CDT.CustomerCode = JTF.CustomerCode
        Left join LKACSoft_ArchivingStatus LAS
            on JTF.ArchivingStatus = LAS.ID
        Left join LKACSoft_LendDocument LD
            on JTF.Code = LD.FileCode
        Left join LKACSoft_DocumentLendingHistory DLH
            on LD.DocumentLendID = DLH.DocumentLendID
        Left join LkacSoft_Customer C
            on CDT.CustomerCode = C.Code
    Group by
        -- Columns from LKACSoft_CustomerDocumentType(Main Table)
        CDT.DocumentTypeID,

        -- Columns from LKACSoft_DocumentType
        DT.DocumentTypeName,
        CDT.DocumentReceivingMechanism,
        CDT.AvgAmount,
        CDT.RegisteredAmount,

        C.Code,
        C.Name,
        C.ShortName,
        C.Address,
        C.LogoS3Key,
        C.FilterLocation,
        C.GetDocsDate,
        C.DateCreate,
        C.Suspended,
        C.SuspendedTo,
        C.Dissolved,
        C.DissolvedDate,
        C.MainAccountant,
        C.CreatedBy,
        C.AssignedToCustomerSupport,
        C.ResponsibleAccountantTeam,
        C.LKACSoft_DepartmentCode,
        C.ContractExpiry,
        C.ContractSignedDate
go

-- SELECT OBJECT_TYPE = o.type_desc
-- FROM sys.objects o
-- WHERE o.name = 'V_DetailCustomers';