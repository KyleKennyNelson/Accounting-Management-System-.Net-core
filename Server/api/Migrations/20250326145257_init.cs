using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetAPI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    API = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetAPI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetMenu_AspNetMenu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AspNetMenu",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_AccountantTeam",
                columns: table => new
                {
                    TeamID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeamLeader = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_AccountantTeam", x => x.TeamID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_Customer",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoS3Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilterLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetDocsDate = table.Column<int>(type: "int", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Suspended = table.Column<bool>(type: "bit", nullable: true),
                    SuspendedTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dissolved = table.Column<bool>(type: "bit", nullable: true),
                    DissolvedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MainAccountant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedToCustomerSupport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleAccountantTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LKACSoft_DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_Customer", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_Department",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    Closed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_Department", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_DocumentLendingHistory",
                columns: table => new
                {
                    DocumentLendID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LendExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LendStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LendDocument = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_DocumentLendingHistory", x => x.DocumentLendID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_DocumentType",
                columns: table => new
                {
                    DocumentTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DocumentReceivingMechanism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AvgAmount = table.Column<int>(type: "int", nullable: true),
                    RelatedToCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_DocumentType", x => x.DocumentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_Feedback",
                columns: table => new
                {
                    Code = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromWhoCode = table.Column<int>(type: "int", nullable: true),
                    ToWhomCode = table.Column<int>(type: "int", nullable: true),
                    FromCustomer = table.Column<bool>(type: "bit", nullable: true),
                    ToCustomer = table.Column<bool>(type: "bit", nullable: true),
                    FeedbackMsg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateFeedback = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_Feedback", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_JobTaskFile",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileS3Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountantID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountantReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReadyToBeReturnedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArchivingStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhysicalLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArchivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReturnedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedToProcess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_JobTaskFile", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_LendDocument",
                columns: table => new
                {
                    DocumentLendID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_LendDocument", x => new { x.DocumentLendID, x.FileCode });
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_Position",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_Position", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_Priority",
                columns: table => new
                {
                    PriorityID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignatedColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_Priority", x => x.PriorityID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_Process",
                columns: table => new
                {
                    ProcessID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GetDocsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPeriodicProcess = table.Column<bool>(type: "bit", nullable: true),
                    ProcessStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedToCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_Process", x => x.ProcessID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_ProcessStatus",
                columns: table => new
                {
                    ProcessStatusID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_ProcessStatus", x => x.ProcessStatusID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_RequestToCustomerSupport",
                columns: table => new
                {
                    RequestID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsFromStaff = table.Column<bool>(type: "bit", nullable: true),
                    StaffID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFromCustomer = table.Column<bool>(type: "bit", nullable: true),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateResolved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateVerified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerSupportComment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerSupportID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_RequestToCustomerSupport", x => x.RequestID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_Task",
                columns: table => new
                {
                    TaskID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaskDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskStatusID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAccepted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCompleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReview = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRetried = table.Column<bool>(type: "bit", nullable: true),
                    RelatedToProcess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignatedNumberOfDocument = table.Column<int>(type: "int", nullable: true),
                    NumberOfCompletedDocument = table.Column<int>(type: "int", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_Task", x => x.TaskID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_TaskComment",
                columns: table => new
                {
                    CommentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommentedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_TaskComment", x => x.CommentID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_TaskStatus",
                columns: table => new
                {
                    TaskStatusID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignatedColor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_TaskStatus", x => x.TaskStatusID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_TaskType",
                columns: table => new
                {
                    TaskTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredProcessStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_TaskType", x => x.TaskTypeID);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_TaskTypeResponsiblePosition",
                columns: table => new
                {
                    TaskStatusID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PositionCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_TaskTypeResponsiblePosition", x => new { x.TaskStatusID, x.TaskTypeID, x.PositionCode });
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_TaskTypeStatus",
                columns: table => new
                {
                    TaskStatusID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TaskTypeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssociatedProcessStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_TaskTypeStatus", x => new { x.TaskStatusID, x.TaskTypeID });
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_User",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TeamID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsQuitJob = table.Column<bool>(type: "bit", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rolemenupermAll",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    PermId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCount = table.Column<int>(type: "int", nullable: true),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PermName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolemenupermAll", x => new { x.RoleId, x.MenuId, x.PermId });
                });

            migrationBuilder.CreateTable(
                name: "RoleModels",
                columns: table => new
                {
                    RoleID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModels", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "userRolesModels",
                columns: table => new
                {
                    Role = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userRolesModels", x => x.Role);
                });

            migrationBuilder.CreateTable(
                name: "V_DetailCustomers",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoS3Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilterLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetDocsDate = table.Column<int>(type: "int", nullable: true),
                    DateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Suspended = table.Column<bool>(type: "bit", nullable: true),
                    SuspendedTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dissolved = table.Column<bool>(type: "bit", nullable: true),
                    DissolvedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MainAccountant = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedToCustomerSupport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponsibleAccountantTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LKACSoft_DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantTeamID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantFirstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainAccountantDob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MainAccountantIsQuitJob = table.Column<bool>(type: "bit", nullable: true),
                    MainAccountantDateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MainAccountantTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByTeamID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByFirstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedByDob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByIsQuitJob = table.Column<bool>(type: "bit", nullable: true),
                    CreatedByDateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportTeamID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportFirstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedSupportDob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedSupportIsQuitJob = table.Column<bool>(type: "bit", nullable: true),
                    AssignedSupportDateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedSupportTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentDisplayOrder = table.Column<int>(type: "int", nullable: true),
                    DepartmentClosed = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_DetailCustomers", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "V_DetailTasks",
                columns: table => new
                {
                    TaskID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TaskDeadline = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateAccepted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCompleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateReview = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRetried = table.Column<bool>(type: "bit", nullable: true),
                    RelatedToProcess = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesignatedNumberOfDocument = table.Column<int>(type: "int", nullable: true),
                    NumberOfCompletedDocument = table.Column<int>(type: "int", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserTeamID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserFirstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedUserDob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedUserIsQuitJob = table.Column<bool>(type: "bit", nullable: true),
                    AssignedUserDateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssignedUserTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserTeamID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserUsername = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserFirstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserLastname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserAvatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserDistrict = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedUserDob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedUserIsQuitJob = table.Column<bool>(type: "bit", nullable: true),
                    ReviewedUserDateCreate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewedUserTeam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GetDocsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsPeriodicProcess = table.Column<bool>(type: "bit", nullable: true),
                    ProcessStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedToCustomer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskStatusID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskStatusDesignatedColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriorityDesignatedColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskTypeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequiredProcessStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_DetailTasks", x => x.TaskID);
                });

            migrationBuilder.CreateTable(
                name: "V_TakePermission",
                columns: table => new
                {
                    Permission = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    API = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_TakePermission", x => x.Permission);
                });

            migrationBuilder.CreateTable(
                name: "V_TakeRole",
                columns: table => new
                {
                    RoleName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    API = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_TakeRole", x => x.RoleName);
                });

            migrationBuilder.CreateTable(
                name: "V_UserId_RoleIds",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_UserId_RoleIds", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "V_Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneVerified = table.Column<bool>(type: "bit", nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_V_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VMenus",
                columns: table => new
                {
                    menuID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    menuParent = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VMenus", x => x.menuID);
                });

            migrationBuilder.CreateTable(
                name: "VPermission_Roles",
                columns: table => new
                {
                    rolemenuID = table.Column<int>(type: "int", nullable: false),
                    permissionID = table.Column<int>(type: "int", nullable: false),
                    permissionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VPermission_Roles", x => new { x.rolemenuID, x.permissionID });
                });

            migrationBuilder.CreateTable(
                name: "VRole_Menus",
                columns: table => new
                {
                    menuID = table.Column<int>(type: "int", nullable: false),
                    rolemenuID = table.Column<int>(type: "int", nullable: false),
                    permissionID = table.Column<int>(type: "int", nullable: false),
                    roleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VRole_Menus", x => new { x.menuID, x.rolemenuID, x.permissionID });
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleAPI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleAPI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleAPI_AspNetAPI_ApiId",
                        column: x => x.ApiId,
                        principalTable: "AspNetAPI",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetRoleAPI_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleMenu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleMenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleMenu_AspNetMenu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "AspNetMenu",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetRoleMenu_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LKACSoft_UserPosition",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LKACSoft_PositionCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LKACSoft_DepartmentCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LKACSoft_UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LKACSoft_UserPosition", x => new { x.UserID, x.LKACSoft_PositionCode, x.LKACSoft_DepartmentCode });
                    table.ForeignKey(
                        name: "FK_LKACSoft_UserPosition_LKACSoft_Position_LKACSoft_PositionCode",
                        column: x => x.LKACSoft_PositionCode,
                        principalTable: "LKACSoft_Position",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LKACSoft_UserPosition_LKACSoft_User_LKACSoft_UserID",
                        column: x => x.LKACSoft_UserID,
                        principalTable: "LKACSoft_User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ApiPermission",
                columns: table => new
                {
                    RoleApiId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiPermission", x => new { x.RoleApiId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_ApiPermission_AspNetRoleAPI_RoleApiId",
                        column: x => x.RoleApiId,
                        principalTable: "AspNetRoleAPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiPermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MenuPermission",
                columns: table => new
                {
                    RoleMenuId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPermission", x => new { x.RoleMenuId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_MenuPermission_AspNetRoleMenu_RoleMenuId",
                        column: x => x.RoleMenuId,
                        principalTable: "AspNetRoleMenu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuPermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiPermission_PermissionId",
                table: "ApiPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetMenu_ParentId",
                table: "AspNetMenu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleAPI_ApiId",
                table: "AspNetRoleAPI",
                column: "ApiId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleAPI_RoleId",
                table: "AspNetRoleAPI",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleMenu_MenuId",
                table: "AspNetRoleMenu",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleMenu_RoleId",
                table: "AspNetRoleMenu",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LKACSoft_UserPosition_LKACSoft_PositionCode",
                table: "LKACSoft_UserPosition",
                column: "LKACSoft_PositionCode");

            migrationBuilder.CreateIndex(
                name: "IX_LKACSoft_UserPosition_LKACSoft_UserID",
                table: "LKACSoft_UserPosition",
                column: "LKACSoft_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuPermission_PermissionId",
                table: "MenuPermission",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiPermission");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "LKACSoft_AccountantTeam");

            migrationBuilder.DropTable(
                name: "LKACSoft_Customer");

            migrationBuilder.DropTable(
                name: "LKACSoft_Department");

            migrationBuilder.DropTable(
                name: "LKACSoft_DocumentLendingHistory");

            migrationBuilder.DropTable(
                name: "LKACSoft_DocumentType");

            migrationBuilder.DropTable(
                name: "LKACSoft_Feedback");

            migrationBuilder.DropTable(
                name: "LKACSoft_JobTaskFile");

            migrationBuilder.DropTable(
                name: "LKACSoft_LendDocument");

            migrationBuilder.DropTable(
                name: "LKACSoft_Priority");

            migrationBuilder.DropTable(
                name: "LKACSoft_Process");

            migrationBuilder.DropTable(
                name: "LKACSoft_ProcessStatus");

            migrationBuilder.DropTable(
                name: "LKACSoft_RequestToCustomerSupport");

            migrationBuilder.DropTable(
                name: "LKACSoft_Task");

            migrationBuilder.DropTable(
                name: "LKACSoft_TaskComment");

            migrationBuilder.DropTable(
                name: "LKACSoft_TaskStatus");

            migrationBuilder.DropTable(
                name: "LKACSoft_TaskType");

            migrationBuilder.DropTable(
                name: "LKACSoft_TaskTypeResponsiblePosition");

            migrationBuilder.DropTable(
                name: "LKACSoft_TaskTypeStatus");

            migrationBuilder.DropTable(
                name: "LKACSoft_UserPosition");

            migrationBuilder.DropTable(
                name: "MenuPermission");

            migrationBuilder.DropTable(
                name: "rolemenupermAll");

            migrationBuilder.DropTable(
                name: "RoleModels");

            migrationBuilder.DropTable(
                name: "userRolesModels");

            migrationBuilder.DropTable(
                name: "V_DetailCustomers");

            migrationBuilder.DropTable(
                name: "V_DetailTasks");

            migrationBuilder.DropTable(
                name: "V_TakePermission");

            migrationBuilder.DropTable(
                name: "V_TakeRole");

            migrationBuilder.DropTable(
                name: "V_UserId_RoleIds");

            migrationBuilder.DropTable(
                name: "V_Users");

            migrationBuilder.DropTable(
                name: "VMenus");

            migrationBuilder.DropTable(
                name: "VPermission_Roles");

            migrationBuilder.DropTable(
                name: "VRole_Menus");

            migrationBuilder.DropTable(
                name: "AspNetRoleAPI");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "LKACSoft_Position");

            migrationBuilder.DropTable(
                name: "LKACSoft_User");

            migrationBuilder.DropTable(
                name: "AspNetRoleMenu");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "AspNetAPI");

            migrationBuilder.DropTable(
                name: "AspNetMenu");

            migrationBuilder.DropTable(
                name: "AspNetRoles");
        }
    }
}
