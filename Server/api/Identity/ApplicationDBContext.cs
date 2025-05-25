using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using api.Dtos.Account;
using api.Models.AuthModels;
using api.Models;
using LKACSoftModel;
using Microsoft.AspNetCore.Identity;
using api.Interfaces;
using api.Dtos.LK_Dtos.LKACSoft_TaskDTO;

namespace api.Identity
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        //private readonly string _connectionString;
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        //private string BuildConnectionString(string baseConnectionString, string userId, string password)
        //{
        //    return baseConnectionString
        //        .Replace("{UserId}", userId)
        //        .Replace("{Password}", password);
        //}

        // New constructor for SQL Authentication
        //public ApplicationDBContext(LoginDto loginDto, IConfiguration configuration)
        //{
        //    // Build the connection string with SQL Authentication credentials
        //    var baseConnectionString = configuration.GetConnectionString("LoginConnection");
        //    _connectionString = BuildConnectionString(baseConnectionString, loginDto.EmailAddress, loginDto.Password);
        //}

        //Entity for API Authentication

        public DbSet<UserRolesModel> userRolesModels { get; set; }

        public DbSet<V_Role> RoleModels { get; set; }

        public DbSet<RoleMenuPermAll> rolemenupermAll {  get; set; }


        //Entity for Identity
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuPermission> MenuPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }
        public DbSet<ApplicationRoleMenu> RoleMenus { get; set; }
        public DbSet<ApplicationAPI> APIs { get; set; }
        public DbSet<ApplicationRoleAPI> RoleAPIs { get; set; }
        public DbSet<APIPermission> ApiPermissions { get; set; }

        //View for Identity
        public DbSet<V_Menu> VMenus { get; set; }
        public DbSet<V_Permission_RoleMenu> VPermission_Roles { get; set; }
        public DbSet<V_Role_Menu> VRole_Menus { get; set; }
        public DbSet<V_User> V_Users { get; set; }
        public DbSet<V_UserId_RoleId> V_UserId_RoleIds { get; set; }
        public DbSet<V_ApiPermissionRole> V_ApiPermissionRole { get; set; }

        //Entity for LK_DB_Model
        public DbSet<LKACSoft_User> LKACSoft_User { get; set; }

        public DbSet<LKACSoft_Task> LKACSoft_Task { get; set; }

        public DbSet<LKACSoft_Priority> LKACSoft_Priority { get; set; }

        public DbSet<LKACSoft_TaskStatus> LKACSoft_TaskStatus { get; set; }

        public DbSet<LKACSoft_TaskType> LKACSoft_TaskType { get; set; }
        
        public DbSet<LKACSoft_TaskTypeResponsiblePosition> LKACSoft_TaskTypeResponsiblePosition { get; set; }

        public DbSet<LKACSoft_Position> LKACSoft_Position { get; set; }

        public DbSet<LKACSoft_Execution> LKACSoft_Execution { get; set; }

        public DbSet<LKACSoft_ProcessStatus> LKACSoft_ProcessStatus { get; set; }

        public DbSet<LKACSoft_ProcessSchema> LKACSoft_ProcessSchema { get; set; }

        public DbSet<LKACSoft_JobTaskFile> LKACSoft_JobTaskFile { get; set; }

        public DbSet<LKACSoft_AccountingStatus> LKACSoft_AccountingStatus { get; set; }

        public DbSet<LKACSoft_ArchivingStatus> LKACSoft_ArchivingStatus { get; set; }

        public DbSet<LKACSoft_Customer> LKACSoft_Customer { get; set; }

        public DbSet<LKACSoft_AccountantTeam> LKACSoft_AccountantTeam { get; set; }

        public DbSet<LKACSoft_Department> LKACSoft_Department { get; set; }

        public DbSet<LKACSoft_UserNotification> LKACSoft_UserNotification { get; set; }

        public DbSet<LKACSoft_Notification> LKACSoft_Notification { get; set; }

        //Entity for LK_DB_Model Views
        public DbSet<V_DetailTasks> V_DetailTasks { get; set; }

        public DbSet<V_DetailCustomers> V_DetailCustomers { get; set; }

        public DbSet<V_DetailExecutions> V_DetailExecutions { get; set; }

        public DbSet<V_DetailDocumentTypes> V_DetailDocumentTypes { get; set; }

        public DbSet<V_DocumentTypeDtos> V_DocumentTypeDtos { get; set; }

        public DbSet<V_DetailJobTaskFiles> V_DetailJobTaskFiles { get; set; }

        public DbSet<V_DetailUsers> V_DetailUsers { get; set; }

        public DbSet<V_DetailUsersKPI> V_DetailUsersKPI { get; set; }

        public DbSet<V_DetailProcessSchemaStatuses> V_DetailProcessSchemaStatuses { get; set; }

        public DbSet<LKACSoft_AmountTaskStatusDto> LKACSoft_AmountTaskStatusDto { get; set; }

        public DbSet<LKACSoft_AmountRetriedTaskDto> LKACSoft_AmountRetriedTaskDto { get; set; }

        public DbSet<LKACSoft_TaskVisualizationDto> LKACSoft_TaskVisualizationDto { get; set; }

        public DbSet<LKACSoft_TaskAverageCompletionTimePerQuarterDto> LKACSoft_TaskAverageCompletionTimePerQuarterDto { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetUserRoles_Users

                entity.HasOne<IdentityRole>()
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetUserRoles_Roles
            });

            // AspNetUserClaims
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetUserClaims_Users
            });

            // AspNetRoleClaims
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.HasOne<IdentityRole>()
                    .WithMany()
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetRoleClaims_Roles
            });

            // AspNetUserLogins
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetUserLogins_Users
            });

            // AspNetUserTokens
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasOne<IdentityUser>()
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetUserTokens_Users
            });

            // AspNetRoleMenu (custom entity)
            modelBuilder.Entity<ApplicationRoleMenu>(entity =>
            {
                entity.ToTable("AspNetRoleMenu");

                entity.HasKey(rm => new { rm.Id });

                entity.HasOne(rm => rm.Role)
                    .WithMany()
                    .HasForeignKey(rm => rm.RoleId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetRoleMenu_Roles

                entity.HasOne(rm => rm.MenuItem)
                    .WithMany()
                    .HasForeignKey(rm => rm.MenuId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetRoleMenu_Menu
            });

            // MenuPermission (custom entity)
            modelBuilder.Entity<MenuPermission>(entity =>
            {
                entity.ToTable("MenuPermission");

                entity.HasKey(mp => new { mp.RoleMenuId, mp.PermissionId });

                entity.HasOne(mp => mp.RoleMenu)
                    .WithMany(rm => rm.Permissions)
                    .HasForeignKey(mp => mp.RoleMenuId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_MenuPermission_RoleMenu

                entity.HasOne(mp => mp.Permission)
                    .WithMany(p => p.MenuPermissions)
                    .HasForeignKey(mp => mp.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_MenuPermission_Permission
            });

            // AspNetAPI
            modelBuilder.Entity<ApplicationAPI>(item =>
            {
                item.ToTable("AspNetAPI");

                item.HasMany(t => t.RoleApis)
                    .WithOne(u => u.API)
                    .HasForeignKey(r => r.ApiId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // AspNetMenu
            modelBuilder.Entity<MenuItem>(item =>
            {
                item.ToTable("AspNetMenu");
                item.HasMany(y => y.Children)
                    .WithOne(r => r.ParentItem)
                    .HasForeignKey(u => u.ParentId);

                item.HasMany(t => t.RoleMenus)
                    .WithOne(u => u.MenuItem)
                    .HasForeignKey(r => r.MenuId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // ApiPermission (custom entity)
            modelBuilder.Entity<APIPermission>(entity =>
            {
                entity.ToTable("ApiPermission");

                entity.HasKey(ap => new { ap.RoleApiId, ap.PermissionId });

                entity.HasOne(ap => ap.Permission)
                    .WithMany(p => p.ApiPermissions)
                    .HasForeignKey(ap => ap.PermissionId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_ApiPermission_Permission

                entity.HasOne(ap => ap.RoleApi)
                    .WithMany(ra => ra.Permissions)
                    .HasForeignKey(ap => ap.RoleApiId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_ApiPermission_AspNetRoleAPI
            });

            // AspNetRoleAPI (custom entity)
            modelBuilder.Entity<ApplicationRoleAPI>(entity =>
            {
                entity.ToTable("AspNetRoleAPI");

                entity.HasOne(ra => ra.Role)
                    .WithMany()
                    .HasForeignKey(ra => ra.RoleId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetRoleAPI_AspNetRoles

                entity.HasOne(ra => ra.API)
                    .WithMany()
                    .HasForeignKey(ra => ra.ApiId)
                    .OnDelete(DeleteBehavior.Cascade); // FK_AspNetRoleAPI_AspNetAPI
            });

            // Permission (custom entity)
            modelBuilder.Entity<Permission>(mp =>
            {
                mp.ToTable("Permission");

                mp.HasKey(l => l.Id);

                mp.HasMany(o => o.MenuPermissions)
                    .WithOne(i => i.Permission)
                    .HasForeignKey(y => y.PermissionId);
            });

            modelBuilder.Entity<V_ApiPermissionRole>()
                .HasKey(v => new { v.API, v.Permission, v.Role }); // Composite key for V_ApiPermissionRole

            modelBuilder.Entity<V_Menu>()
                .HasKey(v => new { v.menuID }); // Composite key for V_Menu
            
            modelBuilder.Entity<V_Role>()
                .HasKey(v => new { v.RoleID }); // Composite key for V_Role

            modelBuilder.Entity<V_Permission_RoleMenu>()
                .HasKey(v => new { v.rolemenuID, v.permissionID }); // Composite key for V_Permission_RoleMenu

            modelBuilder.Entity<V_Role_Menu>()
                .HasKey(v => new { v.menuID, v.rolemenuID, v.permissionID }); // Composite key for V_Role_Menu
            
            modelBuilder.Entity<UserRolesModel>()
                .HasKey(v => new { v.Role }); // Composite key for V_Role_Menu

            modelBuilder.Entity<RoleMenuPermAll>()
                .HasKey(rmpa => new { rmpa.RoleId, rmpa.MenuId, rmpa.PermId }); // Composite key for V_Menu
            
            modelBuilder.Entity<V_UserId_RoleId>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            //PrimaryKey for LK_DB_Model

            modelBuilder.Entity<LKACSoft_Task>()
                .HasKey(LKAC_T => new { LKAC_T.TaskID });

            modelBuilder.Entity<LKACSoft_Priority>()
                .HasKey(LKAC_Pri => new { LKAC_Pri.PriorityID });

            modelBuilder.Entity<LKACSoft_Execution>()
                .HasKey(LKAC_E => new { LKAC_E.ExecutionID });

            modelBuilder.Entity<LKACSoft_JobTaskFile>()
                .HasKey(LKAC_JTF => new { LKAC_JTF.Code });

            modelBuilder.Entity<LKACSoft_AccountingStatus>()
                .HasKey(LKAC_ActS => new { LKAC_ActS.ID });

            modelBuilder.Entity<LKACSoft_ArchivingStatus>()
                .HasKey(LKAC_AcvS => new { LKAC_AcvS.ID });

            modelBuilder.Entity<LKACSoft_Customer>()
                .HasKey(LKAC_C => new { LKAC_C.Code });

            modelBuilder.Entity<LKACSoft_AccountantTeam>()
                .HasKey(LKAC_AT => new { LKAC_AT.TeamID });

            modelBuilder.Entity<LKACSoft_UserPosition>()
                .HasKey(LKAC_UP => new { LKAC_UP.UserID, LKAC_UP.RoleID});

            modelBuilder.Entity<LKACSoft_RolePosition>()
                .HasKey(LKAC_RP => new { LKAC_RP.RoleID});

            modelBuilder.Entity<LKACSoft_LendDocument>()
                .HasKey(LKAC_LD => new { LKAC_LD.DocumentLendID, LKAC_LD.FileCode });

            modelBuilder.Entity<LKACSoft_TaskTypeStatus>()
                .HasKey(LKAC_TTS => new { LKAC_TTS.TaskStatusID, LKAC_TTS.TaskTypeID });

            modelBuilder.Entity<LKACSoft_TaskTypeResponsiblePosition>()
                .HasKey(LKAC_TTS => new { LKAC_TTS.TaskStatusID, LKAC_TTS.TaskTypeID, LKAC_TTS.RoleID });

            modelBuilder.Entity<LKACSoft_Department>()
                .HasKey(LKAC_D => new { LKAC_D.Code });

            modelBuilder.Entity<LKACSoft_DocumentLendingHistory>()
                .HasKey(LKAC_DLH => new { LKAC_DLH.DocumentLendID });

            modelBuilder.Entity<LKACSoft_DocumentType>()
                .HasKey(LKAC_DT => new { LKAC_DT.DocumentTypeID });

            modelBuilder.Entity<LKACSoft_Feedback>()
                .HasKey(LKAC_FB => new { LKAC_FB.Code });

            modelBuilder.Entity<LKACSoft_Position>()
                .HasKey(LKAC_P => new { LKAC_P.Code });

            modelBuilder.Entity<LKACSoft_Department>()
                .HasKey(LKAC_D => new { LKAC_D.Code });

            modelBuilder.Entity<LKACSoft_ProcessStatus>()
                .HasKey(LKAC_PS => new { LKAC_PS.ProcessStatusID });

            modelBuilder.Entity<LKACSoft_ProcessSchema>()
                .HasKey(LKAC_PS => new { LKAC_PS.ProcessSchemaID });

            modelBuilder.Entity<LKACSoft_RequestToCustomerSupport>()
                .HasKey(LKAC_RTCS => new { LKAC_RTCS.RequestID });

            modelBuilder.Entity<LKACSoft_TaskComment>()
                .HasKey(LKAC_TC => new { LKAC_TC.CommentID });

            modelBuilder.Entity<LKACSoft_TaskStatus>()
                .HasKey(LKAC_TS => new { LKAC_TS.TaskStatusID });

            modelBuilder.Entity<LKACSoft_TaskType>()
                .HasKey(LKAC_TT => new { LKAC_TT.TaskTypeID });

            modelBuilder.Entity<LKACSoft_User>()
                .HasKey(LKAC_U => new { LKAC_U.ID});

            modelBuilder.Entity<LKACSoft_UserNotification>()
                .HasKey(LKAC_UN => new { LKAC_UN.UserID, LKAC_UN.NotificationID });

            modelBuilder.Entity<LKACSoft_Notification>()
                .HasKey(LKAC_N => new { LKAC_N.NotificationID });

            //PrimaryKey for LK_DB_Model Views

            modelBuilder.Entity<V_DetailTasks>()
                .HasKey(V_dt => new { V_dt.TaskID });

            modelBuilder.Entity<V_DetailCustomers>()
                .HasKey(V_dc => new { V_dc.Code });

            modelBuilder.Entity<V_DetailExecutions>()
                .HasKey(V_de => new { V_de.ExecutionID });

            modelBuilder.Entity<V_DetailDocumentTypes>()
                .HasKey(V_dt => new { V_dt.DocumentTypeID, V_dt.Code });

            modelBuilder.Entity<V_DocumentTypeDtos>()
                .HasKey(V_dtDto => new { V_dtDto.DocumentTypeID});

            modelBuilder.Entity<V_DetailJobTaskFiles>()
                .HasKey(V_djtf => new { V_djtf.JobTaskFileCode });

            modelBuilder.Entity<V_DetailUsers>()
                .HasKey(V_du => new { V_du.UserID});

            modelBuilder.Entity<V_DetailUsersKPI>()
                .HasKey(V_duk => new { V_duk.UserID });

            modelBuilder.Entity<V_DetailProcessSchemaStatuses>()
                .HasKey(V_dpss => new { V_dpss.ProcessSchemaStatusID });

            modelBuilder.Entity<LKACSoft_AmountTaskStatusDto>()
                .HasNoKey();

            modelBuilder.Entity<LKACSoft_AmountRetriedTaskDto>()
                .HasNoKey();

            modelBuilder.Entity<LKACSoft_TaskVisualizationDto>()
                .HasNoKey();

            modelBuilder.Entity<LKACSoft_TaskAverageCompletionTimePerQuarterDto>()
                .HasNoKey();
        }
    }
}
