use db_ab6e43_lkacsoftdb
go

--drop view V_DocumentTypeDtos

Create or Alter View V_DocumentTypeDtos
AS
    Select
        -- Columns from LKACSoft_DocumentType(Main Table)
        DT.DocumentTypeID,
        DT.DocumentTypeName,
        JTF.*,

        --ArchivedAmount
        SUM(CASE WHEN LAS.Name = N'Đang lưu trữ' THEN 1 ELSE 0 END) AS ArchivedAmount,

        --LendAmount
        SUM(CASE WHEN DLH.LendStatus = N'Bị mất' THEN 1 ELSE 0 END) AS LendAmount,

        --LostAmount
        SUM(CASE WHEN LAS.Name = N'Bị mất' THEN 1 ELSE 0 END) AS LostAmount


    from LkacSoft_DocumentType DT
        Left join LKACSoft_JobTaskFile JTF
            on DT.DocumentTypeID = JTF.DocumentType
        Left join LKACSoft_ArchivingStatus LAS
            on JTF.ArchivingStatus = LAS.ID
        Left join LKACSoft_LendDocument LD
            on JTF.Code = LD.FileCode
        Left join LKACSoft_DocumentLendingHistory DLH
            on LD.DocumentLendID = DLH.DocumentLendID
    Group BY
        DT.DocumentTypeID,
        DT.DocumentTypeName,
        JTF.Code,
        JTF.FileName,
        JTF.FileType,
        JTF.FileS3Key,
        JTF.CreatedAt,
        JTF.AccountantID,
        JTF.AccountingStatus,
        JTF.AccountantReceivedAt,
        JTF.ReadyToBeReturnedAt,
        JTF.ArchivingStatus,
        JTF.PhysicalLocation,
        JTF.ArchivedDate,
        JTF.ReturnedDate,
        JTF.CreatedBy,
        JTF.RelatedToExecution,
        JTF.DocumentType,
        JTF.CustomerCode
go

-- select *
-- from AspNetUsers

-- SELECT OBJECT_TYPE = o.type_desc
-- FROM sys.objects o
-- WHERE o.name = 'V_DetailCustomers';