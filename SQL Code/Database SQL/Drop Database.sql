

-----DROP DATABSE
use db_ab6e43_lkacsoftdb
go


-----------------------------------------------Drop all constraints
DECLARE @sql NVARCHAR(MAX) = N'';

SELECT @sql += N'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id)) + '.' 
                + QUOTENAME(OBJECT_NAME(parent_object_id)) +
                ' DROP CONSTRAINT ' + QUOTENAME(name) + ';' + CHAR(13)
FROM sys.foreign_keys;

EXEC sp_executesql @sql;
go


--Drop dependent tables first:
DROP TABLE dbo.LKACSoft_TaskTypeResponsiblePosition;
--go
DROP TABLE dbo.LKACSoft_TaskTypeStatus;
--go
DROP TABLE dbo.LKACSoft_TaskComment;
--go
DROP TABLE dbo.LKACSoft_RequestToCustomerSupport;
--go
DROP TABLE dbo.LKACSoft_UserPosition;
--go
DROP TABLE dbo.LKACSoft_RolePosition;
--go
DROP TABLE dbo.LKACSoft_LendDocument;
--go
DROP TABLE dbo.LKACSoft_JobTaskFile;
--go
DROP TABLE dbo.LKACSoft_AccountingStatus;
--go
DROP TABLE dbo.LKACSoft_ArchivingStatus;
--go
DROP TABLE dbo.LKACSoft_Priority;
--go
DROP TABLE dbo.LKACSoft_Task;
--go
DROP TABLE dbo.LKACSoft_Feedback;
--go

-- Then drop tables that are referenced by others:
DROP TABLE dbo.LKACSoft_TaskType;
--go
DROP TABLE dbo.LKACSoft_TaskStatus;
--go
DROP TABLE dbo.LKACSoft_Execution;
--go
DROP TABLE dbo.LKACSoft_DocumentType;
--go
DROP TABLE dbo.LKACSoft_Customer;
--go
DROP TABLE dbo.LKACSoft_CustomerDocumentType;
--go
DROP TABLE dbo.LKACSoft_AccountantTeam;
--go
DROP TABLE dbo.LKACSoft_User;
--go
DROP TABLE dbo.LKACSoft_ProcessStatus;
--go
DROP TABLE dbo.LKACSoft_Department;
--go
DROP TABLE dbo.LKACSoft_Position;
--go
DROP TABLE dbo.LKACSoft_DocumentLendingHistory;
--go
DROP TABLE dbo.LKACSoft_ProcessSchema;
--go
DROP TABLE dbo.LKACSoft_ProcessSchemaStatus;

DROP TABLE dbo.LKACSoft_ExecutionAttributesValue;

DROP TABLE dbo.LKACSoft_ExecutionAttributesDefinition;
Go