--drop database LKACSoft_DB
--go

--create database LKACSoft_DB
--go

use db_ab6e43_lkacsoftdb
go

---***CREATE TABLE SCHEMA***---

-----LKACSoft_ArchivingStatus Table
-- drop Table dbo.LKACSoft_ArchivingStatus
-- go

CREATE TABLE dbo.LKACSoft_ArchivingStatus
(
    ID       INT NOT NULL,
    Name     NVARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_ArchivingStatus PRIMARY KEY (ID)
);
GO


-----LKACSoft_AccountingStatus Table
-- drop Table dbo.LKACSoft_AccountingStatus
-- go

CREATE TABLE dbo.LKACSoft_AccountingStatus
(
    ID       INT NOT NULL,
    Name     NVARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_AccountingStatus PRIMARY KEY (ID)
);
GO


-----LKACSoft_JobTaskFile Table
-- drop Table dbo.LKACSoft_JobTaskFile
-- go

CREATE TABLE dbo.LKACSoft_JobTaskFile
(
    Code                VARCHAR(255) NOT NULL,
    FileName            NVARCHAR(255) NULL,
    FileType            NVARCHAR(255) NULL,
    FileS3Key             VARCHAR(255) NULL,
    CreatedAt               DATE     NULL,
    AccountantID        VARCHAR(255) NULL,
    AccountingStatus    INT NULL,
    AccountantReceivedAt    DATE     NULL,
    ReadyToBeReturnedAt     DATE     NULL,
    ArchivingStatus     INT NULL,
    PhysicalLocation    NVARCHAR(255) NULL,
    ArchivedDate        DATE         NULL,
    ReturnedDate        DATE         NULL,
    CreatedBy           VARCHAR(255) NULL,
    RelatedToExecution  VARCHAR(255) NULL,
    DocumentType        VARCHAR(255) NULL,
    CustomerCode        VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_JobTaskFile PRIMARY KEY (Code)
);
GO

-----LKACSoft_User Table
-- drop table dbo.LKACSoft_User
-- go

CREATE TABLE dbo.LKACSoft_User
(
    ID           VARCHAR(255) NOT NULL,
    Username     NVARCHAR(255) NULL,
    Firstname    NVARCHAR(100) NULL,
    LastName     NVARCHAR(100) NULL,
    Avatar       NVARCHAR(255) NULL,
    Address      NVARCHAR(255) NULL,
    District     NVARCHAR(255) NULL,
    Dob          DATE         NULL,
    IsQuitJob    BIT          NULL,
    DateCreate   DATE         NULL,  -- or DATETIME if preferred
    Team         VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_User PRIMARY KEY (ID)
);
GO

-----LKACSoft_Notification Table
-- drop table dbo.LKACSoft_Notification
-- go

CREATE TABLE dbo.LKACSoft_Notification
(
    NotificationID      VARCHAR(255)    NOT NULL,
    Detail              NVARCHAR(255)   NULL,
    UserCreate          VARCHAR(255)    NULL,
    DateCreated         DATETIME        NULL,  -- or DATETIME if preferred

    CONSTRAINT PK_LKACSoft_Notification PRIMARY KEY (NotificationID)
);
GO

-----LKACSoft_UserNotification Table
-- drop table dbo.LKACSoft_UserNotification
-- go

CREATE TABLE dbo.LKACSoft_UserNotification
(
    UserID              VARCHAR(255) NOT NULL,
    NotificationID      VARCHAR(255) NOT NULL,

    CONSTRAINT PK_LKACSoft_UserNotification PRIMARY KEY (UserID, NotificationID)
);
GO

-----LKACSoft_DocumentLendingHistory Table
-- drop table dbo.LKACSoft_DocumentLendingHistory
-- go

CREATE TABLE dbo.LKACSoft_DocumentLendingHistory
(
    DocumentLendID         VARCHAR(255) NOT NULL,
    LendExpiry             DATE         NULL,
    LendDate               DATE         NULL,
    ReturnedDate           DATE         NULL,
    LendStatus             NVARCHAR(255) NULL,
    LendDocument           NVARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_DocumentLendingHistory PRIMARY KEY (DocumentLendID)
);
GO

-----LKACSoft_LendDocument Table
-- drop table dbo.LKACSoft_LendDocument
-- go

CREATE TABLE dbo.LKACSoft_LendDocument
(
    DocumentLendID VARCHAR(255) NOT NULL,
    FileCode       VARCHAR(255) NOT NULL,

    CONSTRAINT PK_LKACSoft_LendDocument 
        PRIMARY KEY (DocumentLendID, FileCode)
);
GO

-----LKACSoft_AccountantTeam Table
-- drop table dbo.LKACSoft_AccountantTeam
-- go

CREATE TABLE dbo.LKACSoft_AccountantTeam
(
    TeamID     VARCHAR(255) NOT NULL,
    TeamName   NVARCHAR(255) NULL,
    TeamLeader VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_AccountantTeam PRIMARY KEY (TeamID)
);
GO

-----LKACSoft_DocumentType Table
-- drop table dbo.LKACSoft_DocumentType
-- go

CREATE TABLE dbo.LKACSoft_DocumentType
(
    DocumentTypeID              VARCHAR(255) NOT NULL,
    DocumentTypeName            NVARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_DocumentType PRIMARY KEY (DocumentTypeID)
);
GO

-----LKACSoft_CustomerDocumentType Table
-- drop table dbo.LKACSoft_CustomerDocumentType
-- go
CREATE TABLE dbo.LKACSoft_CustomerDocumentType
(
    CustomerCode                VARCHAR(255) NOT NULL,
    DocumentTypeID              VARCHAR(255) NOT NULL,
    DocumentReceivingMechanism  NVARCHAR(255) NULL,
    AvgAmount                   INT          NULL,
    RegisteredAmount            INT          NULL,

    CONSTRAINT PK_LKACSoft_CustomerDocumentType PRIMARY KEY (CustomerCode, DocumentTypeID)
);
GO

-- ALTER TABLE dbo.LKACSoft_CustomerDocumentType DROP COLUMN CurrentTotalAmount;
-- GO

-----LKACSoft_ProcessStatus Table
-- drop table dbo.LKACSoft_ProcessStatus
-- go

CREATE TABLE dbo.LKACSoft_ProcessStatus
(
    ProcessStatusID   VARCHAR(255) NOT NULL,
    StatusName        NVARCHAR(255) NULL,
    DesignatedColor   VARCHAR(32)     NULL,

    CONSTRAINT PK_LKACSoft_ProcessStatus PRIMARY KEY (ProcessStatusID)
);
GO

-----LKACSoft_Execution Table
-- drop table dbo.LKACSoft_Exection
-- go

CREATE TABLE dbo.LKACSoft_Execution
(
    ExecutionID       VARCHAR(255) NOT NULL,
    ExecutionName     NVARCHAR(255) NULL,
    CreatedBy         VARCHAR(255) NULL,
    DateCreated       DATE         NULL,
    --GetDocsDate       DATE         NULL,
    IsPeriodic        BIT          NULL,
    -- This column points to LKACSoft_ProcessStatus(ProcessStatusID):
    ProcessSchemaStatus     VARCHAR(255) NULL,
    ProcessSchemaID         VARCHAR(255) NULL,
    RelatedToCustomer       VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_Execution PRIMARY KEY (ExecutionID)
);
GO

-----LKACSoft_ProcessSchema Table
-- drop table dbo.LKACSoft_ProcessSchema
-- go

CREATE TABLE dbo.LKACSoft_ProcessSchema
(
    ProcessSchemaID       VARCHAR(255) NOT NULL,
    Name                  NVARCHAR(255) NULL,
    Description           NVARCHAR(255) NULL,
    CreatedAt             DATE         NULL,
    UpdatedAt             DATE         NULL,

    CONSTRAINT PK_LKACSoft_ProcessSchema PRIMARY KEY (ProcessSchemaID)
);
GO

-----LKACSoft_ProcessSchemaStatus Table
-- drop table dbo.LKACSoft_ProcessSchemaStatus
-- go

CREATE TABLE dbo.LKACSoft_ProcessSchemaStatus
(
    ProcessSchemaStatusID   VARCHAR(255) NOT NULL,
    ProcessSchema           VARCHAR(255) NULL,
    ProcessStatus           VARCHAR(255) NULL,
    OrderIndex              INT NULL,
    CreatedAt               DATE NULL,

    CONSTRAINT PK_LKACSoft_Process PRIMARY KEY (ProcessSchemaStatusID)
);
GO

-----LKACSoft_Feedback Table
-- drop table dbo.LKACSoft_Feedback
-- go

CREATE TABLE dbo.LKACSoft_Feedback
(
    Code          INT          NOT NULL IDENTITY(1,1),
    FromWhoCode   INT          NULL,
    ToWhomCode    INT          NULL,
    FromCustomer  BIT          NULL,
    ToCustomer    BIT          NULL,
    FeedbackMsg   NVARCHAR(2048) NULL,
    DateFeedback  DATE         NULL,
    ExecutionID     VARCHAR(255) NULL,  -- references LKACSoft_Process(ProcessID)

    CONSTRAINT PK_LKACSoft_Feedback PRIMARY KEY (Code)
);
GO

-----LKACSoft_TaskStatus Table
-- drop table dbo.LKACSoft_TaskStatus
-- go

CREATE TABLE dbo.LKACSoft_TaskStatus
(
    TaskStatusID            VARCHAR(255) NOT NULL,
    TaskStatusName          NVARCHAR(255)     NULL,
    DesignatedColor         VARCHAR(32)     NULL,

    CONSTRAINT PK_LKACSoft_TaskStatus PRIMARY KEY (TaskStatusID)
);
GO

-----LKACSoft_TaskType Table
-- drop table dbo.LKACSoft_TaskType
-- go

CREATE TABLE dbo.LKACSoft_TaskType
(
    TaskTypeID            VARCHAR(255) NOT NULL,
    TaskTypeName          NVARCHAR(255) NULL,
    Description           NVARCHAR(1024) NULL,
    TaskTypeDesignatedColor VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_TaskType PRIMARY KEY (TaskTypeID)
);
GO

-----LKACSoft_TaskTypeStatus Table
-- drop table dbo.LKACSoft_TaskTypeStatus
-- go

CREATE TABLE dbo.LKACSoft_TaskTypeStatus
(
    TaskStatusID            VARCHAR(255) NOT NULL,
    TaskTypeID              VARCHAR(255) NOT NULL,
    AssociatedProcessSchemaStatus VARCHAR(255) NULL,
    RequiredProcessSchemaStatus VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_TaskTypeStatus PRIMARY KEY (TaskStatusID, TaskTypeID)
);
GO

-----LKACSoft_TaskTypeResponsiblePosition Table
-- drop table dbo.LKACSoft_TaskTypeResponsiblePosition
-- go

CREATE TABLE dbo.LKACSoft_TaskTypeResponsiblePosition
(
    TaskStatusID            VARCHAR(255) NOT NULL,
    TaskTypeID              VARCHAR(255) NOT NULL,
    RoleID                  VARCHAR(255) NOT NULL,
    CanExitStatus           BIT          NULL,
    CanEnterStatus          BIT          NULL,

    CONSTRAINT PK_LKACSoft_TaskTypeResponsiblePosition PRIMARY KEY (TaskStatusID, TaskTypeID, RoleID)
);
GO

-----LKACSoft_Task Table
-- drop table dbo.LKACSoft_Task
-- go

CREATE TABLE dbo.LKACSoft_Task
(
    TaskID                     VARCHAR(255) NOT NULL,
    DateAssigned               DATETIME         NULL,
    TaskDeadline               DATETIME         NULL,
    AssignedTo                 VARCHAR(255) NULL,
    Title                      NVARCHAR(255) NULL,
    Detail                     NVARCHAR(200) NULL,
    TaskStatusID               VARCHAR(255) NULL,
    DateAccepted               DATETIME         NULL,
    DateCompleted              DATETIME         NULL,
    ReviewedBy                 VARCHAR(255) NULL,
    ReviewNote                 NVARCHAR(2000) NULL,
    DateReview                 DATETIME         NULL,
    IsRetried                  BIT          NULL,
    RelatedToExecution         VARCHAR(255) NULL, -- references LKACSoft_Process?
    TaskType                   VARCHAR(255) NULL, -- references LKACSoft_TaskType
    --DocumentType               VARCHAR(255) NULL, -- If referencing DocumentType(DocumentTypeID)
    DesignatedNumberOfDocument INT          NULL,
    NumberOfCompletedDocument  INT          NULL,
    NumberOfRetrievedDocument  INT          NULL,
    Priority                   VARCHAR(255) NULL,
    CreatedAt                  DATETIME         NULL,

    CONSTRAINT PK_LKACSoft_Task PRIMARY KEY (TaskID)
);
GO

-- ALTER TABLE dbo.LKACSoft_Task ALTER COLUMN CreatedAt DATETIME;  
-- GO  

Create table dbo.LKACSoft_Priority
(
    PriorityID                 VARCHAR(255) NOT NULL,
    PriorityName               NVARCHAR(50) NULL,
    DesignatedColor            VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_Priority PRIMARY KEY (PriorityID)
)

-----LKACSoft_TaskComment Table
-- drop table dbo.LKACSoft_TaskComment
-- go

CREATE TABLE dbo.LKACSoft_TaskComment
(
    CommentID    VARCHAR(255) NOT NULL,
    TaskID       VARCHAR(255) NULL,  -- references LKACSoft_Task
    CommentedBy  VARCHAR(255) NULL,
    createdAt    DATE         NULL,
    updatedAt    DATE         NULL,
    Details      NVARCHAR(2048) NULL,

    CONSTRAINT PK_LKACSoft_TaskComment PRIMARY KEY (CommentID)
);
GO

-----LKACSoft_Department Table
-- drop table dbo.LKACSoft_Department
-- go

CREATE TABLE dbo.LKACSoft_Department
(
    Code         VARCHAR(10) NOT NULL,
    Name         NVARCHAR(50) NULL,
    DisplayOrder INT         NULL,
    Closed       BIT         NULL,

    CONSTRAINT PK_LKACSoft_Department PRIMARY KEY (Code)
);
GO

-----LKACSoft_Position Table
-- drop table dbo.LKACSoft_Position
-- go

CREATE TABLE dbo.LKACSoft_Position
(
    Code         CHAR(2)     NOT NULL,
    Name         NVARCHAR(255) NULL,
    DisplayOrder INT         NULL,

    CONSTRAINT PK_LKACSoft_Position PRIMARY KEY (Code)
);
GO

-----LKACSoft_UserPosition Table
-- drop table dbo.LKACSoft_UserPosition
-- go

CREATE TABLE dbo.LKACSoft_UserPosition
(
    UserID         VARCHAR(255) NOT NULL,
    RoleID         VARCHAR(255) NOT NULL,
    AssignedDate   DATE         NULL,

    CONSTRAINT PK_LKACSoft_UserPosition PRIMARY KEY (UserID, RoleID)
);
GO

-----LKACSoft_RolePosition Table
-- drop table dbo.LKACSoft_RolePosition
-- go

CREATE TABLE dbo.LKACSoft_RolePosition
(
    RoleID                  VARCHAR(255) NOT NULL,
    LKACSoft_PositionCode   CHAR(2)      NULL,
    LKACSoft_DepartmentCode VARCHAR(10)  NULL,

    CONSTRAINT PK_LKACSoft_RolePosition PRIMARY KEY (RoleID)
);
GO

-----LKACSoft_Customer Table
-- drop table dbo.LKACSoft_Customer
-- go

CREATE TABLE dbo.LKACSoft_Customer
(
    Code                         VARCHAR(255) NOT NULL,
    Name                         NVARCHAR(255) NULL,
    ShortName                    NVARCHAR(255) NULL,
    Address                      NVARCHAR(255) NULL,
    LogoS3Key                    VARCHAR(255) NULL,
    FilterLocation               NVARCHAR(255) NULL,
    GetDocsDate                  INT          NULL,
    DateCreate                   DATE         NULL,
    Suspended                    BIT          NULL,
    SuspendedTo                  DATE         NULL,
    Dissolved                    BIT          NULL,
    DissolvedDate                DATE         NULL,
    MainAccountant               VARCHAR(255) NULL,
    CreatedBy                    VARCHAR(255) NULL,
    AssignedToCustomerSupport    VARCHAR(255) NULL,
    ResponsibleAccountantTeam    VARCHAR(255) NULL,
    LKACSoft_DepartmentCode      VARCHAR(10)  NULL,
    ContractExpiry               DATE         NULL,
    ContractSignedDate           DATE         NULL,

    CONSTRAINT PK_LKACSoft_Customer PRIMARY KEY (Code)
);
GO

-----LKACSoft_RequestToCustomerSupport Table
-- drop table dbo.LKACSoft_RequestToCustomerSupport
-- go

CREATE TABLE dbo.LKACSoft_RequestToCustomerSupport
(
    RequestID              VARCHAR(255) NOT NULL,
    Title                  VARCHAR(255) NULL,
    Details                VARCHAR(255) NULL,
    createAt               DATE         NULL,
    IsFromStaff            BIT          NULL,
    StaffID                VARCHAR(255) NULL,
    IsFromCustomer         BIT          NULL,
    CustomerID             VARCHAR(255) NULL,
    DateAssigned           DATE         NULL,
    DateResolved           DATE         NULL,
    DateVerified           DATE         NULL,
    Status                 VARCHAR(255) NULL,
    CustomerSupportComment VARCHAR(255) NULL,

    -- Primary key for this table:
    CustomerSupportID      VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_RequestToCustomerSupport PRIMARY KEY (RequestID)
);
GO

-----LKACSoft_ExecutionAttributesDefinition Table
-- drop table dbo.LKACSoft_ExecutionAttributesDefinition
-- go

CREATE TABLE dbo.LKACSoft_ExecutionAttributesDefinition (
    FieldId         INT          NOT NULL,
    FieldName       NVARCHAR(255) NULL,
    DataType        VARCHAR(255) NULL,
    ProcessSchemaID VARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_ExecutionAttributesDefinition PRIMARY KEY (FieldId)
);
GO

-- alter TABLE dbo.LKACSoft_ExecutionAttributesValue ALTER COLUMN FieldValue NVARCHAR(255) NULL

-----LKACSoft_ExecutionAttributesValue Table
-- drop table dbo.LKACSoft_ExecutionAttributesValue
-- go

CREATE TABLE dbo.LKACSoft_ExecutionAttributesValue (
    ExecutionID             VARCHAR(255) NOT NULL,
    FieldID                 INT NOT NULL,
    FieldValue              NVARCHAR(255) NULL,

    CONSTRAINT PK_LKACSoft_ExecutionAttributesValue PRIMARY KEY (ExecutionID, FieldID)
);
GO



---***ADD FK CONSTRAINT***---

--LKACSoft_RequestToCustomerSupport ForeignKey

ALTER TABLE dbo.LKACSoft_RequestToCustomerSupport
  ADD CONSTRAINT FK_LKACSoft_RequestToCustomerSupport_LKACSoft_User_StaffID
      FOREIGN KEY (StaffID)
      REFERENCES dbo.LKACSoft_User(ID)
      ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_RequestToCustomerSupport
  ADD CONSTRAINT FK_LKACSoft_RequestToCustomerSupport_LKACSoft_User_CustomerSupportID
      FOREIGN KEY (CustomerSupportID)
      REFERENCES dbo.LKACSoft_User(ID);

ALTER TABLE dbo.LKACSoft_RequestToCustomerSupport
  ADD CONSTRAINT FK_LKACSoft_RequestToCustomerSupport_LKACSoft_Customer
      FOREIGN KEY (CustomerID)
      REFERENCES dbo.LKACSoft_Customer(Code)
      ON DELETE CASCADE;
GO

--LKACSoft_UserPosition ForeignKey

ALTER TABLE dbo.LKACSoft_UserPosition
  ADD CONSTRAINT FK_LKACSoft_UserPosition_RolePosition
      FOREIGN KEY (RoleID)
      REFERENCES dbo.LKACSoft_RolePosition(RoleID)
      ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_UserPosition
  ADD CONSTRAINT FK_LKACSoft_UserPosition_LKACSoft_User
      FOREIGN KEY (UserID)
      REFERENCES dbo.LKACSoft_User(ID)
      ON DELETE CASCADE;
GO

--LKACSoft_RolePosition ForeignKey

ALTER TABLE dbo.LKACSoft_RolePosition
  ADD CONSTRAINT FK_LKACSoft_RolePosition_LKACSoft_Department
      FOREIGN KEY (LKACSoft_DepartmentCode)
      REFERENCES dbo.LKACSoft_Department(Code)
      ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_RolePosition
  ADD CONSTRAINT FK_LKACSoft_RolePosition_Position
      FOREIGN KEY (LKACSoft_PositionCode)
      REFERENCES dbo.LKACSoft_Position(Code)
      ON DELETE CASCADE;
GO

--LKACSoft_Customer ForeignKey

ALTER TABLE dbo.LKACSoft_Customer
  ADD CONSTRAINT FK_LKACSoft_Customer_LKACSoft_Department
      FOREIGN KEY (LKACSoft_DepartmentCode)
      REFERENCES dbo.LKACSoft_Department(Code)
      ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_Customer
ADD CONSTRAINT FK_LKACSoft_Customer_LKACSoft_User_MainAccountant
    FOREIGN KEY (MainAccountant)
    REFERENCES dbo.LKACSoft_User(ID)

ALTER TABLE dbo.LKACSoft_Customer
ADD CONSTRAINT FK_LKACSoft_Customer_LKACSoft_User_CreatedBy
    FOREIGN KEY (CreatedBy)
    REFERENCES dbo.LKACSoft_User(ID);

ALTER TABLE dbo.LKACSoft_Customer
ADD CONSTRAINT FK_LKACSoft_Customer_LKACSoft_User_AssignedToCustomerSupport
    FOREIGN KEY (AssignedToCustomerSupport)
    REFERENCES dbo.LKACSoft_User(ID);

ALTER TABLE dbo.LKACSoft_Customer
ADD CONSTRAINT FK_LKACSoft_Customer_LKACSoft_AccountantTeam
    FOREIGN KEY (ResponsibleAccountantTeam)
    REFERENCES dbo.LKACSoft_AccountantTeam(TeamID)
    ON DELETE CASCADE;

GO

--LKACSoft_User ForeignKey

ALTER TABLE dbo.LKACSoft_User
  ADD CONSTRAINT FK_LKACSoft_User_LKACSoft_AccountantTeam
      FOREIGN KEY (Team)
      REFERENCES dbo.LKACSoft_AccountantTeam(TeamID);
GO

--LKACSoft_UserNotification ForeignKey

ALTER TABLE dbo.LKACSoft_UserNotification
  ADD CONSTRAINT FK_LKACSoft_UserNotification_LKACSoft_User
      FOREIGN KEY (UserID)
      REFERENCES dbo.LKACSoft_User(ID)
      ON DELETE CASCADE;
GO

ALTER TABLE dbo.LKACSoft_UserNotification
  ADD CONSTRAINT FK_LKACSoft_UserNotification_LKACSoft_Notification
      FOREIGN KEY (NotificationID)
      REFERENCES dbo.LKACSoft_Notification(NotificationID)
      ON DELETE CASCADE;
GO

--LKACSoft_AccountantTeam ForeignKey

ALTER TABLE dbo.LKACSoft_AccountantTeam
ADD CONSTRAINT FK_LKACSoft_AccountantTeam_LKACSoft_User
    FOREIGN KEY (TeamLeader)
    REFERENCES dbo.LKACSoft_User(ID);
GO

--LKACSoft_JobTaskFile ForeignKey

ALTER TABLE dbo.LKACSoft_JobTaskFile
ADD CONSTRAINT FK_LKACSoft_JobTaskFile_LKACSoft_ArchivingStatus
    FOREIGN KEY (ArchivingStatus)
    REFERENCES dbo.LKACSoft_ArchivingStatus(ID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_JobTaskFile
ADD CONSTRAINT FK_LKACSoft_JobTaskFile_LKACSoft_AccountingStatus
    FOREIGN KEY (AccountingStatus)
    REFERENCES dbo.LKACSoft_AccountingStatus(ID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_JobTaskFile
ADD CONSTRAINT FK_LKACSoft_JobTaskFile_LKACSoft_User_AccountantID
    FOREIGN KEY (AccountantID)
    REFERENCES dbo.LKACSoft_User(ID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_JobTaskFile
ADD CONSTRAINT FK_LKACSoft_JobTaskFile_LKACSoft_User_CreatedBy
    FOREIGN KEY (CreatedBy)
    REFERENCES dbo.LKACSoft_User(ID);

ALTER TABLE dbo.LKACSoft_JobTaskFile
ADD CONSTRAINT FK_LKACSoft_JobTaskFile_LKACSoft_CustomerDocumentType
    FOREIGN KEY (CustomerCode, DocumentType)
    REFERENCES dbo.LKACSoft_CustomerDocumentType(CustomerCode, DocumentTypeID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_JobTaskFile
ADD CONSTRAINT FK_LKACSoft_JobTaskFile_LKACSoft_Execution
    FOREIGN KEY (RelatedToExecution)
    REFERENCES dbo.LKACSoft_Execution(ExecutionID)
    ON DELETE CASCADE;
GO

--LKACSoft_LendDocument ForeignKey

ALTER TABLE dbo.LKACSoft_LendDocument
ADD CONSTRAINT FK_LKACSoft_LendDocument_LKACSoft_DocumentLendingHistory
    FOREIGN KEY (DocumentLendID)
    REFERENCES dbo.LKACSoft_DocumentLendingHistory(DocumentLendID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_LendDocument
ADD CONSTRAINT FK_LKACSoft_LendDocument_LKACSoft_JobTaskFile
    FOREIGN KEY (FileCode)
    REFERENCES dbo.LKACSoft_JobTaskFile(Code)
    ON DELETE CASCADE;
GO

--LKACSoft_Execution ForeignKey

ALTER TABLE dbo.LKACSoft_Execution
ADD CONSTRAINT FK_LKACSoft_Execution_LKACSoft_ProcessSchemaStatus
    FOREIGN KEY (ProcessSchemaStatus)
    REFERENCES dbo.LKACSoft_ProcessSchemaStatus(ProcessSchemaStatusID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_Execution
ADD CONSTRAINT FK_LKACSoft_Execution_LKACSoft_ProcessSchema
    FOREIGN KEY (ProcessSchemaID)
    REFERENCES dbo.LKACSoft_ProcessSchema(ProcessSchemaID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_Execution
ADD CONSTRAINT FK_LKACSoft_Execution_LKACSoft_User
    FOREIGN KEY (CreatedBy)
    REFERENCES dbo.LKACSoft_User(ID);

ALTER TABLE dbo.LKACSoft_Execution
ADD CONSTRAINT FK_LKACSoft_Execution_LKACSoft_Customer
    FOREIGN KEY (RelatedToCustomer)
    REFERENCES dbo.LKACSoft_Customer(Code)
    ON DELETE CASCADE;
GO

--LKACSoft_Feedback ForeignKey

ALTER TABLE dbo.LKACSoft_Feedback
ADD CONSTRAINT FK_LKACSoft_Feedback_LKACSoft_Execution
    FOREIGN KEY (ExecutionID)
    REFERENCES dbo.LKACSoft_Execution(ExecutionID)
    ON DELETE CASCADE;
GO

--LKACSoft_Task ForeignKey

ALTER TABLE dbo.LKACSoft_Task
ADD CONSTRAINT FK_LKACSoft_Task_LKACSoft_User_AssignedTo
    FOREIGN KEY (AssignedTo)
    REFERENCES dbo.LKACSoft_User(ID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_Task
ADD CONSTRAINT FK_LKACSoft_Task_LKACSoft_User_ReviewedBy
    FOREIGN KEY (ReviewedBy)
    REFERENCES dbo.LKACSoft_User(ID);

ALTER TABLE dbo.LKACSoft_Task
ADD CONSTRAINT FK_LKACSoft_Task_LKACSoft_Execution
    FOREIGN KEY (RelatedToExecution)
    REFERENCES dbo.LKACSoft_Execution(ExecutionID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_Task
ADD CONSTRAINT FK_LKACSoft_Task_LKACSoft_TaskStatus
    FOREIGN KEY (TaskStatusID)
    REFERENCES dbo.LKACSoft_TaskStatus(TaskStatusID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_Task
ADD CONSTRAINT FK_LKACSoft_Task_LKACSoft_TaskType
    FOREIGN KEY (TaskType)
    REFERENCES dbo.LKACSoft_TaskType(TaskTypeID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_Task
ADD CONSTRAINT FK_LKACSoft_Task_LKACSoft_Priority
    FOREIGN KEY (Priority)
    REFERENCES dbo.LKACSoft_Priority(PriorityID)
    ON DELETE CASCADE;
GO

--LKACSoft_TaskTypeStatus ForeignKey

ALTER TABLE dbo.LKACSoft_TaskTypeStatus
ADD CONSTRAINT FK_LKACSoft_TaskTypeStatus_LKACSoft_TaskStatus
    FOREIGN KEY (TaskStatusId)
    REFERENCES dbo.LKACSoft_TaskStatus(TaskStatusID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_TaskTypeStatus
ADD CONSTRAINT FK_LKACSoft_TaskTypeStatus_LKACSoft_TaskType
    FOREIGN KEY (TaskTypeID)
    REFERENCES dbo.LKACSoft_TaskType(TaskTypeID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_TaskTypeStatus
ADD CONSTRAINT FK_LKACSoft_TaskTypeStatus_LKACSoft_AssociatedProcessSchemaStatus
    FOREIGN KEY (AssociatedProcessSchemaStatus)
    REFERENCES dbo.LKACSoft_ProcessSchemaStatus(ProcessSchemaStatusID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_TaskTypeStatus
ADD CONSTRAINT FK_LKACSoft_TaskTypeStatus_LKACSoft_RequiredProcessSchemaStatus
    FOREIGN KEY (RequiredProcessSchemaStatus)
    REFERENCES dbo.LKACSoft_ProcessSchemaStatus(ProcessSchemaStatusID);
GO

--LKACSoft_ProcessSchemaStatus ForeignKey

ALTER TABLE dbo.LKACSoft_ProcessSchemaStatus
ADD CONSTRAINT FK_LKACSoft_ProcessSchemaStatus_LKACSoft_ProcessStatus
    FOREIGN KEY (ProcessStatus)
    REFERENCES dbo.LKACSoft_ProcessStatus(ProcessStatusID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_ProcessSchemaStatus
ADD CONSTRAINT FK_LKACSoft_ProcessSchemaStatus_LKACSoft_ProcessSchema
    FOREIGN KEY (ProcessSchema)
    REFERENCES dbo.LKACSoft_ProcessSchema(ProcessSchemaID);
GO

--LKACSoft_TaskComment ForeignKey

ALTER TABLE dbo.LKACSoft_TaskComment
ADD CONSTRAINT FK_LKACSoft_TaskComment_LKACSoft_Task
    FOREIGN KEY (TaskID)
    REFERENCES dbo.LKACSoft_Task(TaskID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_TaskComment
ADD CONSTRAINT FK_LKACSoft_TaskComment_LKACSoft_User
    FOREIGN KEY (CommentedBy)
    REFERENCES dbo.LKACSoft_User(ID);
GO

--LKACSoft_TaskTypeResponsiblePosition ForeignKey

ALTER TABLE dbo.LKACSoft_TaskTypeResponsiblePosition
ADD CONSTRAINT FK_LKACSoft_TaskTypeResponsiblePosition_LKACSoft_TaskTypeStatus
    FOREIGN KEY (TaskStatusId, TaskTypeID)
    REFERENCES dbo.LKACSoft_TaskTypeStatus(TaskStatusId, TaskTypeID)
    ON DELETE CASCADE;
GO

ALTER TABLE dbo.LKACSoft_TaskTypeResponsiblePosition
ADD CONSTRAINT FK_LKACSoft_TaskTypeResponsiblePosition_LKACSoft_RolePosition
    FOREIGN KEY (RoleID)
    REFERENCES dbo.LKACSoft_RolePosition(RoleID)
    ON DELETE CASCADE;
GO

--LKACSoft_ExecutionAttributesDefinition ForeignKey

ALTER TABLE dbo.LKACSoft_ExecutionAttributesDefinition
ADD CONSTRAINT FK_LKACSoft_ExecutionAttributesDefinition_LKACSoft_ProcessSchema
    FOREIGN KEY (ProcessSchemaID)
    REFERENCES dbo.LKACSoft_ProcessSchema(ProcessSchemaID)
    ON DELETE CASCADE;
GO

--LKACSoft_ExecutionAttributesValue ForeignKey

ALTER TABLE dbo.LKACSoft_ExecutionAttributesValue
ADD CONSTRAINT FK_LKACSoft_ExecutionAttributesValue_LKACSoft_Execution
    FOREIGN KEY (ExecutionID)
    REFERENCES dbo.LKACSoft_Execution(ExecutionID)
    ON DELETE CASCADE;

ALTER TABLE dbo.LKACSoft_ExecutionAttributesValue
ADD CONSTRAINT FK_LKACSoft_ExecutionAttributesValue_LKACSoft_ExecutionAttributesDefinition
    FOREIGN KEY (FieldID)
    REFERENCES dbo.LKACSoft_ExecutionAttributesDefinition(FieldID);
GO