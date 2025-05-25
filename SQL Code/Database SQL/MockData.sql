use db_ab6e43_lkacsoftdb
go

--1)
------------dbo.LKACSoft_Department
--Thông tin phòng ban

INSERT INTO dbo.LKACSoft_Department (Code, Name, DisplayOrder, Closed)
VALUES
('D01', N'Phòng Kế toán',                   1, 0),
('D02', N'Phòng Chăm sóc khách hàng',       2, 0),
('D03', N'Phòng Hành chính - Giao nhận',    3, 0),
('D04', N'Phòng Nhân sự',                   4, 0);
Go


--2)
------------dbo.LKACSoft_Position
--Thông tin chức vụ

INSERT INTO dbo.LKACSoft_Position (Code, Name, DisplayOrder)
VALUES
('TP',  N'Trưởng phòng', 1),
('NV',  N'Nhân viên',    2),
('HC',  N'Giao nhận',    3),
('GN',   N'Hành chính',  4);
Go

--6)
------------dbo.LKACSoft_AccountantTeam
--Thông tin nhóm kế toán

INSERT INTO dbo.LKACSoft_AccountantTeam (TeamID, TeamName, TeamLeader)
VALUES
('T001', N'Nhóm Alpha', null)  -- TeamLeader = kế toán
Go



--3)
------------dbo.LKACSoft_User
--Thông tin người dùng

INSERT INTO dbo.LKACSoft_User
(
    ID,
    Username,
    Firstname,
    LastName,
    Avatar,
    Address,
    District,
    Dob,
    IsQuitJob,
    DateCreate,
    Team
    -- (các cột khác như TeamID, Team,... nếu có, tuỳ ý để NULL)
)
VALUES
-- 1) Trưởng phòng CSKH
('U001', N'cskh.tp',   N'Lương',   N'Anh',   'cskh_tp.png',  N'Số 10, Đường A', N'Quận 1', '1985-01-01', 0, '2020-01-01', null),

-- 2) Nhân viên CSKH
('U002', N'cskh.nv1',  N'Lê',      N'Bình',  'cskh_nv1.png', N'Số 20, Đường B', N'Quận 1', '1990-02-02', 0, '2020-02-01', null),

-- 3) Trưởng phòng Hành chính - Giao nhận
('U003', N'hcgn.tp',   N'Nguyễn',  N'Cường', null,  N'Số 30, Đường C', N'Quận 2', '1982-03-03', 0, '2020-03-01', null),
('U004', N'ketoan.tp',  N'Trần',  N'Dũng',   'ketoan_tp.png',  N'Số 40, Đường D', N'Quận 3', '1980-04-04', 0, '2020-04-01', null),
('U005', N'ketoan.nv1', N'Hoàng', N'Minh',   'ketoan_nv1.png', N'Số 50, Đường E', N'Quận 3', '1992-05-05', 0, '2020-05-01', null),
('U006', N'giaonhan.tp', N'Phạm',  N'Thanh',  'giaonhan_tp.png', N'Số 60, Đường F', N'Quận 4', '1987-06-06', 0, '2020-06-01', null),
('U007', N'giaonhan.nv1', N'Vũ',  N'Long',   'giaonhan_nv1.png', N'Số 70, Đường G', N'Quận 5', '1991-07-07', 0, '2020-07-01', null),
('U008', N'kiemtoan.tp',  N'Đinh', N'Hà',     'kiemtoan_tp.png',  N'Số 80, Đường H', N'Quận 6', '1983-08-08', 0, '2020-08-01', null),
('U009', N'kiemtoan.nv1', N'Bùi',  N'Vân',    'kiemtoan_nv1.png', N'Số 90, Đường I', N'Quận 7', '1994-09-09', 0, '2020-09-01', 'T001'),
('U010', N'hanhchinh.nv1', N'Ngô',  N'Hải', null ,N'Số 100, Đường J', N'Quận 8', '1996-10-10', 0, '2020-10-01', 'T001');
Go




------------dbo.LKACSoft_RolePosition
--Thông tin chức vụ của người dùng

INSERT INTO dbo.LKACSoft_RolePosition
(
    RoleID,
    LKACSoft_PositionCode,
    LKACSoft_DepartmentCode
)
VALUES
-- 1) Trưởng phòng CSKH
('R001', 'TP', 'D02'),

-- 2) Nhân viên CSKH
('R002', 'NV', 'D02'),

-- 3) Trưởng phòng Hành chính - Giao nhận
('R003', 'TP', 'D03'),

-- 4) Nhân viên Hành chính
('R004', 'HC', 'D03'),

-- 5) Nhân viên Giao nhận
('R005', 'GN', 'D03'),

-- 6) Nhân viên Nhân sự
('R006', 'NV', 'D04'),

-- 7) Trưởng phòng Nhân sự
('R007', 'TP', 'D04'),

-- 8) Nhân viên Kế toán (NV1)
('R008', 'NV', 'D01'),

-- 9) Trưởng phòng Kế toán (NV1)
('R009', 'TP', 'D01');
Go

--4)
------------dbo.LKACSoft_UserPosition
--Thông tin chức vụ của người dùng

INSERT INTO dbo.LKACSoft_UserPosition
(
    UserID,
    RoleID,
    AssignedDate
)
VALUES
-- 1) Trưởng phòng CSKH
('U001', 'R001', '2020-01-01'),

-- 2) Nhân viên CSKH
('U002', 'R002', '2020-01-01'),

-- 3) Trưởng phòng Hành chính - Giao nhận
('U003', 'R003', '2020-01-01'),

-- 4) Trưởng phòng kế toán
('U004', 'R009', '2020-01-01'),

-- 5) Nhân viên Kế toán
('U005', 'R008', '2020-01-01'),

-- 6) Trưởng phòng Nhân sự-GiaoNhan
('U006', 'R003', '2020-01-01'),

-- 7) Trưởng phòng Kế toán
('U007', 'R005', '2020-01-01'),

-- 8) Nhân viên Kế toán (NV1)
('U008', 'R009', '2020-01-01'),
('U009', 'R008', '2020-01-01'),
('U010', 'R004', '2020-01-01');
Go




--5)
------------dbo.LKACSoft_ProcessStatus
--Các trạng thái của 1 quy trình giao nhận, vốn là trạng thái của JobTask

INSERT INTO dbo.LKACSoft_ProcessStatus (ProcessStatusID, StatusName, DesignatedColor)
VALUES
('PS02', N'Sắp đi nhận', '#FF9500'),
('PS03', N'Đang đi nhận', '#FFCC00'),
('PS04', N'Trục trặc nhận', '#D70040'),
('PS05', N'Đã nhận', '#34C759'),
('PS06', N'Hành chính nhận', '#007AFF'),
('PS07', N'Đã scan lưu', '#009861'),
('PS08', N'Kế toán nhận', '#FFD60A'),
('PS09', N'Sẵn sàng trả', '#32D74B'),
('PS10', N'Hành chính nhận trả', '#0A84FF'),
('PS11', N'Đã nhận trả', '#248F24'),
('PS12', N'Đang trả cty', '#FF9F0A'),
('PS13', N'Đã trả cty', '#1C6B1C');
Go




--7)
------------dbo.LKACSoft_Customer
--Thông tin khách hàng

INSERT INTO dbo.LKACSoft_Customer 
(
    Code, 
    Name,
    ShortName,
    Address, 
    LogoS3Key, 
    FilterLocation,
    GetDocsDate, 
    DateCreate, 
    Suspended, 
    SuspendedTo, 
    Dissolved, 
    DissolvedDate, 
    MainAccountant, 
    CreatedBy, 
    AssignedToCustomerSupport, 
    ResponsibleAccountantTeam,
    LKACSoft_DepartmentCode,
    ContractExpiry,
    ContractSignedDate
)
VALUES
('C001', N'Công ty ABC',            'ABC', N'123 Trần Hưng Đạo',                   'logo_abc.png',         N'Khu vực A',     15, '2022-03-01', 0, NULL, 0, NULL, 'U008', 'U001', 'U001', 'T001', 'D01', '2026-03-27 03:29:17.320', '2024-09-20 03:29:17.320'),
('C002', N'Công ty XYZ',            'XYZ', N'456 Lê Lợi',                          'logo_xyz.png',         N'Khu vực B',     30, '2022-04-01', 0, NULL, 0, NULL, 'U009', 'U001', 'U001', 'T001', 'D01', '2026-03-27 03:29:17.320', '2025-06-3 03:29:17.320'),
('C003', N'Cá nhân Nguyễn Văn Tư',  'NVT', N'789 Phan Bội Châu',                   'logo_cust3.png',       N'Khu vực C',     20, '2023-01-01', 0, NULL, 0, NULL, 'U010', 'U001', 'U001', 'T001', 'D01', '2026-03-27 03:29:17.320', '2025-03-27 03:29:17.320'),
('C004', N'Apple Inc.',             'Apple', N'One Apple Park Way, Cupertino, CA',   'logo_apple.png',       N'North America', 10, '2023-05-15', 0, NULL, 0, NULL, 'U008', 'U001', 'U002', 'T001', 'D02', '2026-03-27 03:29:17.320', '2025-03-27 03:29:17.320'),
('C005', N'Microsoft Corporation',  'Microsoft', N'One Microsoft Way, Redmond, WA',      'logo_microsoft.png',   N'North America', 25, '2023-06-20', 0, NULL, 0, NULL, 'U009', 'U001', 'U002', 'T001', 'D02', '2026-03-27 03:29:17.320', '2025-03-27 03:29:17.320'),
('C006', N'Tesla, Inc.',            'Tesla', N'13101 Tesla Road, Austin, TX',        'logo_tesla.png',       N'North America', 18, '2023-07-10', 0, NULL, 0, NULL, 'U010', 'U001', 'U002', 'T001', 'D02', '2026-03-27 03:29:17.320', '2025-03-27 03:29:17.320');
Go




--8)
------------dbo.LKACSoft_DocumentType
--Loại chứng từ và số lượng trung bình của các công ty
INSERT INTO dbo.LKACSoft_DocumentType 
(
    DocumentTypeID,
    DocumentTypeName
)
VALUES
('DT001', N'Invoice'),
('DT002', N'Receipt'),
('DT003', N'Contract'),
('DT004', N'Purchase'),
('DT005', N'Report'),
('DT006', N'Credit Note'),
('DT007', N'Debit Note'),
('DT008', N'Checklist'),
('DT009', N'Specification Document'),
('DT010', N'Proposal');
GO


------------dbo.LKACSoft_DocumentType

INSERT INTO [dbo].[LKACSoft_CustomerDocumentType] (
    [CustomerCode],
    [DocumentTypeID],
    [DocumentReceivingMechanism],
    [AvgAmount],
    [RegisteredAmount]
)
VALUES
('C001', 'DT001', N'Client Portal', 60217, 10567),
('C001', 'DT002', N'In Person', 37687, 41567),
('C001', 'DT003', N'In Person', 73746, 42944),
('C001', 'DT004', N'Postal Mail', 82066, 71632),
('C001', 'DT005', N'Client Portal', 29794, 79174),
('C001', 'DT006', N'In Person', 56148, 3953),
('C001', 'DT007', N'Postal Mail', 35742, 2038),
('C001', 'DT010', N'In Person', 31392, 48136),
('C001', 'DT008', N'Client Portal', 6540, 70029),
('C001', 'DT009', N'In Person', 17640, 60881),
('C002', 'DT001', N'In Person', 12655, 73301),
('C002', 'DT002', N'Courier', 54814, 12822),
('C002', 'DT003', N'In Person', 21480, 7305),
('C002', 'DT004', N'Email', 16236, 29389),
('C002', 'DT005', N'Courier', 13431, 85375),
('C002', 'DT006', N'In Person', 53525, 29270),
('C002', 'DT007', N'Courier', 34100, 62229),
('C002', 'DT010', N'In Person', 17768, 43933),
('C002', 'DT008', N'In Person', 37628, 41726),
('C002', 'DT009', N'In Person', 58717, 81743),
('C003', 'DT001', N'Postal Mail', 96253, 3832),
('C003', 'DT002', N'Client Portal', 59359, 9477),
('C003', 'DT003', N'Courier', 7118, 67696),
('C003', 'DT004', N'Email', 76224, 61279),
('C003', 'DT005', N'In Person', 65184, 97777),
('C003', 'DT006', N'Postal Mail', 68307, 7600),
('C003', 'DT007', N'Postal Mail', 62850, 29806),
('C003', 'DT010', N'Postal Mail', 92361, 54695),
('C003', 'DT008', N'Courier', 56619, 91278),
('C003', 'DT009', N'Courier', 70832, 50016),
('C004', 'DT001', N'Client Portal', 47117, 82291),
('C004', 'DT002', N'In Person', 8907, 56437),
('C004', 'DT003', N'In Person', 79981, 73923),
('C004', 'DT004', N'Postal Mail', 26157, 44618),
('C004', 'DT005', N'In Person', 44430, 95516),
('C004', 'DT006', N'Courier', 4895, 58304),
('C004', 'DT007', N'Client Portal', 84868, 69855),
('C004', 'DT010', N'Postal Mail', 19883, 90496),
('C004', 'DT008', N'Courier', 60086, 43796),
('C004', 'DT009', N'Postal Mail', 50877, 19358),
('C005', 'DT001', N'In Person', 4869, 64408),
('C005', 'DT002', N'In Person', 3443, 12241),
('C005', 'DT003', N'Client Portal', 51224, 91636),
('C005', 'DT004', N'In Person', 93934, 46684),
('C005', 'DT005', N'Postal Mail', 9446, 92298),
('C005', 'DT006', N'Client Portal', 61463, 85554),
('C005', 'DT007', N'Client Portal', 4634, 86178),
('C005', 'DT010', N'Courier', 29101, 93206),
('C005', 'DT008', N'Courier', 75021, 24045),
('C005', 'DT009', N'Postal Mail', 81161, 94638),
('C006', 'DT001', N'Client Portal', 74838, 616),
('C006', 'DT002', N'Email', 54966, 46786),
('C006', 'DT003', N'Email', 15308, 32037),
('C006', 'DT004', N'Courier', 23822, 57718),
('C006', 'DT005', N'Client Portal', 84948, 34793),
('C006', 'DT006', N'Postal Mail', 85411, 49767),
('C006', 'DT007', N'Client Portal', 33704, 56378),
('C006', 'DT010', N'Email', 80179, 7944),
('C006', 'DT008', N'Courier', 63066, 8447),
('C006', 'DT009', N'In Person', 73064, 39073);






--9)
------------dbo.LKACSoft_DocumentLendingHistory 
--Quản lý các lượt mượn lại chứng từ

INSERT INTO dbo.LKACSoft_DocumentLendingHistory 
(
    DocumentLendID, 
    LendExpiry, 
    LendDate, 
    ReturnedDate, 
    LendStatus, 
    LendDocument
)
VALUES
('DLH001', '2023-01-10', '2023-01-01', NULL,         N'Đang cho mượn', N'Hồ sơ ABC Q4'),
('DLH002', '2023-02-15', '2023-02-10', '2023-02-20', N'Đã lấy lại',    N'Hợp đồng XYZ 2022');
Go


INSERT INTO [dbo].[LKACSoft_ProcessSchema] 
(ProcessSchemaID, Name, Description, CreatedAt, UpdatedAt) 
VALUES 
('SC001', N'Quy trình nhận và nhập liệu chứng từ', '', GETDATE(), GETDATE()),
('SC002', N'Quy trình trả chứng từ', '', GETDATE(), GETDATE());
go



INSERT INTO [dbo].[LKACSoft_ProcessSchemaStatus] 
    ([ProcessSchemaStatusID]
      ,[ProcessSchema]
      ,[ProcessStatus]
      ,[OrderIndex]
      ,[CreatedAt])
VALUES 
('PSS02', 'SC001',  'PS02', 1, GETDATE()),
('PSS03', 'SC001',  'PS03', 2, GETDATE()),
('PSS04', 'SC001',  'PS04', 3, GETDATE()),
('PSS05', 'SC001',  'PS05', 4, GETDATE()),
('PSS06', 'SC001',  'PS06', 5, GETDATE()),
('PSS07', 'SC001',  'PS07', 6, GETDATE()),
('PSS08', 'SC001',  'PS08', 7, GETDATE()),
('PSS09', 'SC001',  'PS09', 8, GETDATE()),
('PSS10', 'SC002',  'PS10', 1, GETDATE()),
('PSS11', 'SC002', 'PS11', 2, GETDATE()),
('PSS12', 'SC002', 'PS12', 3, GETDATE()),
('PSS13', 'SC002', 'PS13', 4, GETDATE());
go

-- select *
-- from LKACSoft_ProcessSchemaStatus

-- Update LKACSoft_ProcessSchemaStatus
-- set OrderIndex = CAST(SUBSTRING(ProcessStatus, 3, 2) AS INT) - 9
-- where ProcessSchema = 'SC002'
-- go

--10)
------------dbo.LKACSoft_Execution
--Quy trình trong một tháng của một khách hàng, sẽ bao gồm task của tất cả các bộ phận giao nhận, hành chính, kế toán

INSERT INTO [dbo].[LKACSoft_Execution] ([ExecutionID], [ExecutionName], [CreatedBy], [DateCreated], [IsPeriodic], [ProcessSchemaStatus], [ProcessSchemaID], [RelatedToCustomer])
VALUES
('E001', N'Kế toán tháng 02/2025 - ABC',                    'U007', '2025-02-01', 1, 'PSS02', 'SC001', 'C001'),
('E002', N'Kiểm toán tháng 02/2025 - XYZ',                  'U007', '2025-02-01', 1, 'PSS02', 'SC001', 'C002'),
('E003', N'Kế toán tháng 02/2025 - Nguyễn Văn Tư',          'U007', '2025-02-01', 1, 'PSS02', 'SC001', 'C003'),
('E004', N'Kế toán tháng 02/2025 - Apple Inc.',             'U007', '2025-02-01', 1, 'PSS02', 'SC001', 'C004'),
('E005', N'Kế toán tháng 02/2025 - Microsoft Corporation',  'U007', '2025-02-01', 1, 'PSS02', 'SC001', 'C005'),
('E006', N'Kế toán tháng 02/2025 - Tesla, Inc.',            'U007', '2025-02-01', 1, 'PSS02', 'SC001', 'C006'),
('E007', N'Kế toán tháng 02/2025 - ABC',                    'U007', '2025-02-01', 1, 'PSS10', 'SC002', 'C001'),
('E008', N'Kiểm toán tháng 02/2025 - XYZ',                  'U007', '2025-02-01', 1, 'PSS10', 'SC002', 'C002'),
('E009', N'Kế toán tháng 02/2025 - Nguyễn Văn Tư',          'U007', '2025-02-01', 1, 'PSS10', 'SC002', 'C003'),
('E010', N'Kế toán tháng 02/2025 - Apple Inc.',             'U007', '2025-02-01', 1, 'PSS10', 'SC002', 'C004'),
('E011', N'Kế toán tháng 02/2025 - Microsoft Corporation',  'U007', '2025-02-01', 1, 'PSS10', 'SC002', 'C005'),
('E012', N'Kế toán tháng 02/2025 - Tesla, Inc.',            'U007', '2025-02-01', 1, 'PSS10', 'SC002', 'C006');
go




--11)
------------dbo.LKACSoft_Feedback
--Feedback cho một quy trình giao nhận, vốn là feedback của JobTask

INSERT INTO dbo.LKACSoft_Feedback
(
    FromWhoCode,
    ToWhomCode,
    FromCustomer,
    ToCustomer,
    FeedbackMsg,
    DateFeedback,
    ExecutionID
)
VALUES
(1, 2, 0, 0, N'Cần bổ sung chi tiết cho hóa đơn VAT', '2023-01-15', 'E001'),
(2, 1, 0, 0, N'Kiểm toán đang ổn, nhưng làm rõ quỹ lương', '2023-03-10', 'E002');
Go





--12)
------------dbo.LKACSoft_TaskStatus
--Trạng thái của 1 Task

INSERT INTO dbo.LKACSoft_TaskStatus
(
    TaskStatusID,
    TaskStatusName,
    DesignatedColor
)
VALUES
('TS02', N'Cần làm', '#0880EA'),
('TS03', N'Đang thực hiện', '#5D49D2D2'),
('TS04', N'Đã hoàn thành', '#009861'),
('TS05', N'Xác nhận hoàn thành', '#067A6F'),
('TS06', N'Làm lại', '#DF0728');
Go





--13)
------------dbo.LKACSoft_TaskType
--Phân loại task giữa giao nhận, hành chính, kế toán, để phục vụ xử lí
INSERT INTO dbo.LKACSoft_TaskType
(
    TaskTypeID,
    TaskTypeName,
    Description,
    TaskTypeDesignatedColor
)
VALUES
('TT01', N'Nhận chứng từ',       N'Công việc giao nhận lấy chứng từ từ khách', NULL),
('TT02', N'Hành chính scan',     N'Nhân viên hành chính scan chứng từ',        NULL),
('TT03', N'Kế toán nhập liệu',   N'Nhân viên kế toán nhập số liệu',           NULL),
('TT04', N'Hành chính nhận trả', N'Hành chính nhận giao trả chứng từ đã mượn', NULL),
('TT05', N'Giao chứng từ',       N'Giao trả chứng từ cho khách hàng',         NULL);
Go





--14)
------------dbo.LKACSoft_TaskTypeStatus
--Trạng thái quy trình gắn liền với TaskStatus nhằm hỗ trợ xử lí trạng quy trình một cách tự động
INSERT INTO dbo.LKACSoft_TaskTypeStatus
(
    TaskStatusID,
    TaskTypeID,
    AssociatedProcessSchemaStatus,
    RequiredProcessSchemaStatus
)
VALUES

------Lay chung tu

('TS02', 'TT01', 'PSS02', NULL),
('TS03', 'TT01', 'PSS03', 'PSS02'),
('TS04', 'TT01', 'PSS05', 'PSS03'),
('TS05', 'TT01', 'PSS05', 'PSS05'),
('TS06', 'TT01', NULL, NULL),

------Hanh chinh scan

('TS02', 'TT02', 'PSS05', 'PSS05'),

('TS03', 'TT02', 'PSS06', 'PSS05'),
('TS04', 'TT02', 'PSS07', 'PSS06'),
('TS05', 'TT02', 'PSS07', 'PSS07'),
('TS06', 'TT02', NULL, NULL),

------Ke toan nhap lieu

('TS02', 'TT03', 'PSS07', 'PSS07'),

('TS03', 'TT03', 'PSS08', 'PSS07'),
('TS04', 'TT03', 'PSS09', 'PSS08'),
('TS05', 'TT03', 'PSS09', 'PSS09'),
('TS06', 'TT03', NULL, NULL),

------Giao chung tu

('TS02', 'TT04', 'PSS09', 'PSS09'),

('TS03', 'TT04', 'PSS10', 'PSS09'),
('TS04', 'TT04', 'PSS11', 'PSS10'),
('TS05', 'TT04', 'PSS11', 'PSS11'),
('TS06', 'TT04', NULL, NULL),

------Hanh chinh nhan tra

('TS02', 'TT05', 'PSS11', 'PSS11'),

('TS03', 'TT05', 'PSS12', 'PSS11'),
('TS04', 'TT05', 'PSS13', 'PSS12'),
('TS05', 'TT05', 'PSS13', 'PSS13'),
('TS06', 'TT05', NULL, NULL);
GO





--15)
------------dbo.LKACSoft_TaskTypeResponsiblePosition
--Bảng liên kết vị trí nào nhận công việc Task gì
-- INSERT INTO dbo.LKACSoft_TaskTypeResponsiblePosition
-- (
--     TaskStatusID,
--     TaskTypeID,
--     RoleID
-- )
-- VALUES
-- ('TS02', 'TT01', 'TP'), -- Trưởng phòng cũng có thể thực hiện "Nhận chứng từ" ở trạng thái TS01
-- ('TS03', 'TT03', 'NV'); -- Nhân viên thực hiện "Nhập liệu" ở trạng thái TS02
-- Go



INSERT INTO dbo.LKACSoft_Priority
(
    PriorityID,
    PriorityName,
    DesignatedColor
)
VALUES
('PRI01', N'Không có', '#919095'),
('PRI02', N'Thấp', '#575659'),
('PRI03', N'Thường', '#5627FE'),
('PRI04', N'Cao', '#E9B80B'),
('PRI05', N'Khẩn cấp', '#DF0728');



--16)
------------dbo.LKACSoft_Task
--Tác vụ riêng lẻ của nhân viên trong một quy trình

INSERT INTO dbo.LKACSoft_Task
(
    TaskID,
    DateAssigned,
    TaskDeadline,
    AssignedTo,
    Title,
    Detail,
    TaskStatusID,
    DateAccepted,
    DateCompleted,
    ReviewedBy,
    ReviewNote,
    DateReview,
    IsRetried,
    RelatedToExecution,
    TaskType,
    Priority,
    CreatedAt
)
VALUES
-------------------------
('TK001', '2025-02-05', '2025-02-10', 'U005', N'Lấy chứng từ từ ABC', N'Lấy chứng từ từ ABC',
 'TS02', NULL, NULL, 'U001', NULL, NULL, 0, 'E001', 'TT01', 'PRI02', GETDATE()),
('TK002', '2025-02-06', '2025-02-11', 'U004', N'Scan hóa đơn ABC', N'Scan hóa đơn ABC',
 'TS02', NULL, NULL, 'U002', NULL, NULL, 0, 'E001', 'TT02', 'PRI02', GETDATE()),
('TK003', '2025-02-07', '2025-02-12', 'U008', N'Nhập dữ liệu hóa đơn ABC', N'Nhập dữ liệu hóa đơn ABC',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E001', 'TT03', 'PRI02', GETDATE()),
('TK004', '2025-02-08', '2025-02-13', 'U005', N'Giao chứng từ cho khách ABC', N'Giao chứng từ cho khách ABC',
 'TS03', NULL, NULL, 'U004', NULL, NULL, 0, 'E001', 'TT04', 'PRI02', GETDATE()),
('TK005', '2025-02-09', '2025-02-14', 'U004', N'Nhận chứng từ từ khách ABC', N'Nhận chứng từ từ khách ABC',
 'TS04', NULL, NULL, 'U005', NULL, NULL, 0, 'E001', 'TT05', 'PRI02', GETDATE()),

-------------------------

('TK006', '2025-02-05', '2025-02-10', 'U005', N'Lấy chứng từ từ XYZ', N'Lấy chứng từ từ XYZ',
 'TS02', NULL, NULL, 'U001', NULL, NULL, 0, 'E002', 'TT01', 'PRI02', GETDATE()),
('TK007', '2025-02-06', '2025-02-11', 'U004', N'Scan hóa đơn XYZ', N'Scan hóa đơn XYZ',
 'TS02', NULL, NULL, 'U002', NULL, NULL, 0, 'E002', 'TT02', 'PRI02', GETDATE()),
('TK008', '2025-02-07', '2025-02-12', 'U009', N'Nhập dữ liệu hóa đơn XYZ', N'Nhập dữ liệu hóa đơn XYZ',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E002', 'TT03', 'PRI02', GETDATE()),
('TK009', '2025-02-08', '2025-02-13', 'U005', N'Giao chứng từ cho khách XYZ', N'Giao chứng từ cho khách XYZ',
 'TS03', NULL, NULL, 'U004', NULL, NULL, 0, 'E002', 'TT04', 'PRI02', GETDATE()),
('TK010', '2025-02-09', '2025-02-14', 'U004', N'Nhận chứng từ từ khách XYZ', N'Nhận chứng từ từ khách XYZ',
 'TS04', NULL, NULL, 'U005', NULL, NULL, 0, 'E002', 'TT05', 'PRI02', GETDATE()),

-------------------------

('TK011', '2025-02-05', '2025-02-10', 'U005', N'Lấy chứng từ từ Nguyễn Văn Tư', N'Lấy chứng từ từ Nguyễn Văn Tư',
 'TS02', NULL, NULL, 'U001', NULL, NULL, 0, 'E003', 'TT01', 'PRI02', GETDATE()),
('TK012', '2025-02-06', '2025-02-11', 'U004', N'Scan hóa đơn Nguyễn Văn Tư', N'Scan hóa đơn Nguyễn Văn Tư',
 'TS02', NULL, NULL, 'U002', NULL, NULL, 0, 'E003', 'TT02', 'PRI02', GETDATE()),
('TK013', '2025-02-07', '2025-02-12', 'U008', N'Nhập dữ liệu hóa đơn Nguyễn Văn Tư', N'Nhập dữ liệu hóa đơn Nguyễn Văn Tư',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E003', 'TT03', 'PRI02', GETDATE()),
('TK014', '2025-02-08', '2025-02-13', 'U005', N'Giao chứng từ cho khách Nguyễn Văn Tư', N'Giao chứng từ cho khách Nguyễn Văn Tư',
 'TS03', NULL, NULL, 'U004', NULL, NULL, 0, 'E003', 'TT04', 'PRI02', GETDATE()),
('TK015', '2025-02-09', '2025-02-14', 'U004', N'Nhận chứng từ từ khách Nguyễn Văn Tư', N'Nhận chứng từ từ khách Nguyễn Văn Tư',
 'TS04', NULL, NULL, 'U005', NULL, NULL, 0, 'E003', 'TT05', 'PRI02', GETDATE()),

-------------------------

('TK016', '2025-02-05', '2025-02-10', 'U005', N'Lấy chứng từ từ Microsoft', N'Lấy chứng từ từ Microsoft',
 'TS02', NULL, NULL, 'U001', NULL, NULL, 0, 'E005', 'TT01', 'PRI02', GETDATE()),
('TK017', '2025-02-06', '2025-02-11', 'U004', N'Scan hóa đơn Microsoft', N'Scan hóa đơn Microsoft',
 'TS02', NULL, NULL, 'U002', NULL, NULL, 0, 'E005', 'TT02', 'PRI02', GETDATE()),
('TK018', '2025-02-07', '2025-02-12', 'U010', N'Nhập dữ liệu hóa đơn Microsoft', N'Nhập dữ liệu hóa đơn Microsoft',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E005', 'TT03', 'PRI02', GETDATE()),
('TK019', '2025-02-07', '2025-02-12', 'U008', N'Nhập dữ liệu hóa đơn Microsoft', N'Nhập dữ liệu hóa đơn Microsoft',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E005', 'TT03', 'PRI02', GETDATE()),
('TK020', '2025-02-08', '2025-02-13', 'U005', N'Giao chứng từ cho khách Microsoft', N'Giao chứng từ cho khách Microsoft',
 'TS03', NULL, NULL, 'U004', NULL, NULL, 0, 'E005', 'TT04', 'PRI02', GETDATE()),
('TK021', '2025-02-09', '2025-02-14', 'U004', N'Nhận chứng từ từ khách Microsoft', N'Nhận chứng từ từ khách Microsoft',
 'TS04', NULL, NULL, 'U005', NULL, NULL, 0, 'E005', 'TT05', 'PRI02', GETDATE()),

-------------------------

('TK022', '2025-02-05', '2025-02-10', 'U005', N'Lấy chứng từ từ Tesla', N'Lấy chứng từ từ Tesla',
 'TS02', NULL, NULL, 'U001', NULL, NULL, 0, 'E006', 'TT01', 'PRI02', GETDATE()),
('TK023', '2025-02-06', '2025-02-11', 'U004', N'Scan hóa đơn Tesla', N'Scan hóa đơn Tesla',
 'TS02', NULL, NULL, 'U002', NULL, NULL, 0, 'E006', 'TT02', 'PRI02', GETDATE()),
('TK024', '2025-02-07', '2025-02-12', 'U010', N'Nhập dữ liệu hóa đơn Tesla', N'Nhập dữ liệu hóa đơn Tesla',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E006', 'TT03', 'PRI02', GETDATE()),
('TK025', '2025-02-07', '2025-02-12', 'U008', N'Nhập dữ liệu hóa đơn Tesla', N'Nhập dữ liệu hóa đơn Tesla',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E006', 'TT03', 'PRI02', GETDATE()),
('TK026', '2025-02-07', '2025-02-12', 'U009', N'Nhập dữ liệu hóa đơn Tesla', N'Nhập dữ liệu hóa đơn Tesla',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E006', 'TT03', 'PRI02', GETDATE()),
('TK027', '2025-02-08', '2025-02-13', 'U005', N'Giao chứng từ cho khách Tesla', N'Giao chứng từ cho khách Tesla',
 'TS03', NULL, NULL, 'U004', NULL, NULL, 0, 'E006', 'TT04', 'PRI02', GETDATE()),
('TK028', '2025-02-09', '2025-02-14', 'U004', N'Nhận chứng từ từ khách Tesla', N'Nhận chứng từ từ khách Tesla',
 'TS04', NULL, NULL, 'U005', NULL, NULL, 0, 'E006', 'TT05', 'PRI02', GETDATE()),

-------------------------

('TK029', '2025-02-05', '2025-02-10', 'U005', N'Lấy chứng từ từ Apple Inc.', N'Lấy chứng từ từ Apple Inc.',
 'TS02', NULL, NULL, 'U001', NULL, NULL, 0, 'E004', 'TT01', 'PRI02', GETDATE()),
('TK030', '2025-02-06', '2025-02-11', 'U004', N'Scan hóa đơn Apple Inc.', N'Scan hóa đơn Apple Inc.',
 'TS02', NULL, NULL, 'U002', NULL, NULL, 0, 'E004', 'TT02', 'PRI02', GETDATE()),
('TK031', '2025-02-07', '2025-02-12', 'U009', N'Nhập dữ liệu hóa đơn Apple Inc.', N'Nhập dữ liệu hóa đơn Apple Inc.',
 'TS02', NULL, NULL, 'U003', NULL, NULL, 0, 'E004', 'TT03', 'PRI02', GETDATE()),
('TK032', '2025-02-08', '2025-02-13', 'U005', N'Giao chứng từ cho khách Apple Inc.', N'Giao chứng từ cho khách Apple Inc.',
 'TS03', NULL, NULL, 'U004', NULL, NULL, 0, 'E004', 'TT04', 'PRI02', GETDATE()),
('TK033', '2025-02-09', '2025-02-14', 'U004', N'Nhận chứng từ từ khách Apple Inc.', N'Nhận chứng từ từ khách Apple Inc.',
 'TS04', NULL, NULL, 'U005', NULL, NULL, 0, 'E004', 'TT05', 'PRI02', GETDATE());


--17)
------------dbo.LKACSoft_TaskComment
--Comment do người đánh giá tác vụ để lại
 INSERT INTO dbo.LKACSoft_TaskComment
(
    CommentID,
    TaskID,
    CommentedBy,
    createdAt,
    updatedAt,
    Details
)
VALUES
('TC001', 'TK001', 'U001', '2023-01-06', '2023-01-07', N'Hãy lấy đủ hóa đơn điện, nước.'),
('TC002', 'TK002', 'U003', '2023-01-11', '2023-01-12', N'Cần scan rõ nét, tránh mờ chữ.');
Go




/* ==========================================================
   1.  Bảng trạng thái Kế toán (AccountingStatus)
   ==========================================================*/
INSERT INTO dbo.LKACSoft_AccountingStatus (ID, Name)
VALUES 
    (1, N'Chờ bổ sung'),   -- Pending
    (2, N'Đã nhận'),       -- Received
    (3, N'Chưa nhận'),     -- Unreceived
    (4, N'Không phù hợp'), -- Inappropriate
    (5, N'Sẵn sàng trả');  -- Ready to be returned




/* ==========================================================
   2.  Bảng trạng thái Lưu trữ (ArchivingStatus)
   ==========================================================*/
INSERT INTO dbo.LKACSoft_ArchivingStatus (ID, Name)
VALUES
    (1, N'Chưa lưu trữ'), -- Not archived
    (2, N'Đang lưu trữ'),   -- Archiving
    (3, N'Đang cho mượn'),   -- In use
    (4, N'Đã trả'),   -- Returned
    (5, N'Bị mất');       -- Lost





--18)
------------dbo.LKACSoft_JobTaskFile
--Quản lí file chứng từ
INSERT INTO dbo.LKACSoft_JobTaskFile
(
   Code,
   FileName,
   FileType,
   FileS3Key,
   CreatedAt,
   AccountantID,
   AccountingStatus,
   AccountantReceivedAt,
   ReadyToBeReturnedAt,
   ArchivingStatus,
   PhysicalLocation,
   ArchivedDate,
   ReturnedDate,
   CreatedBy,
   RelatedToExecution,
   DocumentType,
   CustomerCode
)
VALUES
('F001', Null, Null, Null,
'2023-01-05', 'U008', 1, NULL, NULL,
2, N'Kệ A1', NULL, NULL,
'U002', 'E001', 'DT001', 'C001'),

('F002', Null, Null, Null,
'2023-03-10', 'U009', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C001'),

('F003', Null, Null, Null,
'2023-03-10', 'U002', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C002'),

('F004', Null, Null, Null,
'2023-03-10', 'U009', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C003'),

('F005', Null, Null, Null,
'2023-01-05', 'U008', 1, NULL, NULL,
2, N'Kệ A1', NULL, NULL,
'U002', 'E001', 'DT001', 'C002'),

('F006', Null, Null, Null,
'2023-03-10', 'U009', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C002'),

('F007', Null, Null, Null,
'2023-03-10', 'U002', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C003'),

('F008', Null, Null, Null,
'2023-03-10', 'U009', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C003'),

('F009', Null, Null, Null,
'2023-01-05', 'U008', 1, NULL, NULL,
2, N'Kệ A1', NULL, NULL,
'U002', 'E001', 'DT001', 'C004'),

('F010', Null, Null, Null,
'2023-03-10', 'U009', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C004'),

('F011', Null, Null, Null,
'2023-03-10', 'U002', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C006'),

('F012', Null, Null, Null,
'2023-03-10', 'U009', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C006'),

('F013', Null, Null, Null,
'2023-01-05', 'U008', 1, NULL, NULL,
2, N'Kệ A1', NULL, NULL,
'U002', 'E001', 'DT001', 'C006'),

('F014', Null, Null, Null,
'2023-03-10', 'U009', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C005'),

('F015', Null, Null, Null,
'2023-03-10', 'U002', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C004'),

('F016', Null, Null, Null,
'2023-03-10', 'U009', 2, '2023-03-11', '2023-03-20',
2, N'Kệ B2', '2023-03-21', '2023-03-25',
'U008', 'E002', 'DT002', 'C005');
Go






--19)
------------dbo.LKACSoft_LendDocument
--Lưu trữ các chứng từ đã mượn của một lượt

INSERT INTO dbo.LKACSoft_LendDocument
(
   DocumentLendID,
   FileCode
)
VALUES
('DLH001', 'F001'),
('DLH002', 'F002');
Go







--20)
------------dbo.LKACSoft_RequestToCustomerSupport
--Quản lý yêu cầu lên CSKH
INSERT INTO dbo.LKACSoft_RequestToCustomerSupport
(
    RequestID,
    Title,
    Details,
    createAt,
    IsFromStaff,
    StaffID,
    IsFromCustomer,
    CustomerID,
    DateAssigned,
    DateResolved,
    DateVerified,
    Status,
    CustomerSupportComment,
    CustomerSupportID
)
VALUES
-- 1) RQS01: New
('REQ001',
 N'Yêu cầu cập nhật thông tin công ty',
 N'Khách ABC muốn thay đổi địa chỉ',
 '2023-01-01',
 1,               -- IsFromStaff=TRUE => do nhân viên tạo
 'U004',          -- StaffID
 0,               -- IsFromCustomer=FALSE
 'C001',          -- CustomerID
 '2023-01-02',    -- DateAssigned
 NULL,           -- DateResolved
 NULL,           -- DateVerified
 'RQS01',         -- Status
 N'Chưa xử lý',
 'U002'          -- CustomerSupportID=Trưởng phòng
),

-- 2) RQS02: Assigned (Trạng thái trước: RQS01)
('REQ002',
 N'Yêu cầu kiểm tra hợp đồng',
 N'Khách cần tư vấn điều khoản hợp đồng',
 '2023-01-03',
 1,
 'U004',
 0,
 'C001',
 '2023-01-04',
 NULL,
 NULL,
 'RQS02',
 N'Đã phân công cho NV CSKH',
 'U002'
),

-- 3) RQS03: Open (Trạng thái trước: RQS02 hoặc RQS08)
('REQ003',
 N'Hỏi tiến độ hoàn thành báo cáo',
 N'Cần biết khi nào xong BCTC quý 1',
 '2023-01-05',
 1,
 'U004',
 0,
 'C001',
 '2023-01-06',
 NULL,
 NULL,
 'RQS03',
 N'Đã liên hệ bộ phận kế toán',
 'U002'
),

-- 4) RQS04: Duplicated (Trạng thái trước: RQS03)
('REQ004',
 N'Yêu cầu bị trùng',
 N'Phát hiện request y hệt REQ003',
 '2023-01-07',
 1,
 'U004',
 0,
 'C001',
 '2023-01-08',
 NULL,
 NULL,
 'RQS04',
 N'Lỗi trùng yêu cầu',
 'U002'
),

-- 5) RQS05: Rejected (Trạng thái trước: RQS03)
('REQ005',
 N'Yêu cầu thay đổi không hợp lệ',
 N'Khách yêu cầu hủy hợp đồng sớm không lý do',
 '2023-01-09',
 1,
 'U004',
 0,
 'C001',
 '2023-01-10',
 NULL,
 NULL,
 'RQS05',
 N'Không chấp nhận yêu cầu',
 'U002'
),

-- 6) RQS06: Deffered (Trạng thái trước: RQS03)
('REQ006',
 N'Yêu cầu này tạm hoãn',
 N'Khách chưa cung cấp đủ thông tin, hoãn xử lý',
 '2023-01-11',
 1,
 'U004',
 0,
 'C001',
 '2023-01-12',
 NULL,
 NULL,
 'RQS06',
 N'Sẽ liên hệ lại sau',
 'U002'
),

-- 7) RQS07: Resolved (Trạng thái trước: RQS03)
('REQ007',
 N'Vấn đề đã giải quyết',
 N'Cung cấp đầy đủ báo cáo cho khách',
 '2023-01-13',
 1,
 'U004',
 0,
 'C001',
 '2023-01-14',
 '2023-01-15',  -- DateResolved
 NULL,
 'RQS07',
 N'Khách hài lòng',
 'U002'
),

-- 8) RQS08: Verifying (Trạng thái trước: RQS07)
('REQ008',
 N'Xác nhận lại thông tin giải quyết',
 N'Cần khách xác nhận qua email',
 '2023-01-16',
 1,
 'U004',
 0,
 'C001',
 '2023-01-17',
 NULL,
 NULL,
 'RQS08',
 N'Đang chờ xác nhận từ khách',
 'U002'
),

-- 9) RQS09: Re-opened (Trạng thái trước: RQS08)
('REQ009',
 N'Khách yêu cầu mở lại case',
 N'Một số điểm chưa rõ cần làm rõ tiếp',
 '2023-01-18',
 1,
 'U004',
 0,
 'C001',
 '2023-01-19',
 NULL,
 NULL,
 'RQS09',
 N'Case reopen',
 'U002'
),

-- 10) RQS10: Verified (Trạng thái trước: RQS09)
('REQ010',
 N'Đã xác thực hoàn tất',
 N'Khách xác nhận thông tin lần cuối',
 '2023-01-20',
 1,
 'U004',
 0,
 'C001',
 '2023-01-21',
 '2023-01-22',
 '2023-01-23',
 'RQS10',
 N'Ok, đã verify xong',
 'U002'
),

-- 11) RQS11: Closed (Trạng thái trước: RQS10)
('REQ011',
 N'Case đã đóng',
 N'Hoàn tất mọi xử lý, không còn phản hồi thêm',
 '2023-01-24',
 1,
 'U004',
 0,
 'C001',
 '2023-01-25',
 '2023-01-26',
 '2023-01-27',
 'RQS11',
 N'Case closed, kết thúc',
 'U002'
);
GO

-- Insert data into ProcessMetadata
INSERT INTO [dbo].[LKACSoft_ExecutionAttributesDefinition] 
    (FieldId
    ,FieldName
    ,DataType
    ,ProcessSchemaID)
VALUES 
('1', N'Ngày nhận chứng từ', 'Datetime', 'SC001'),
('2', N'Ngày trả chứng từ', 'Datetime', 'SC002');

-- update LKACSoft_ExecutionAttributesDefinition
-- set FieldName = N'Ngày trả chứng từ'
-- where FieldId = '2'

-- Insert data into LKACSoft_ExecutionMetadata
INSERT INTO [dbo].[LKACSoft_ExecutionAttributesValue]
    (ExecutionID
    ,FieldID
    ,FieldValue)
VALUES 
('E001', '1', '2025-02-05'),
('E002', '1', '2025-02-05'),
('E003', '1', '2025-02-05'),
('E004', '1', '2025-02-05'),
('E005', '1', '2025-02-05'),
('E006', '1', '2025-02-05'),
('E007', '2', '2025-12-31'),
('E008', '2', '2025-12-31'),
('E009', '2', '2025-12-31'),
('E010', '2', '2025-12-31'),
('E011', '2', '2025-12-31'),
('E012', '2', '2025-12-31');
Go