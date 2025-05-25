using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using Microsoft.Extensions.Options;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using api.Service;
using api.Repository;
using Microsoft.OpenApi.Models;
using api.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using api.Models.AuthModels;
using api.MiddleWare;
using System.Security.Claims;
using api.Interfaces.I_LKRepo;
using api.Repository.LK_Repo;
using Amazon.S3;
using api.Service.S3_Objects;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Mvc;
using api.Helpers;
using System.Text.Json;
using api.Controllers.Hubs;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddHttpLogging(options => {
//    options.LoggingFields = HttpLoggingFields.All; // Log all fields
//});

builder.Services.AddCors(options => { 
    options.AddPolicy("AllowSpecificOrigins", 
        builder => builder.WithOrigins("*")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(); // Enable camelCase
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Handle circular references
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Ignore null values
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented; // Enable pretty-printing
    });


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    //option.OperationFilter<FileUploadOperationFilter>();
});

builder.Services.AddTransient<ApplicationDBContext>();
builder.Services.AddTransient<ApplicationUserManager>();

//DbContext services
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>();

//Add Schema
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var signingKeyEncryption = builder.Configuration["JWT:SigningKey"];

    if (signingKeyEncryption == null)
        signingKeyEncryption = "";

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(signingKeyEncryption)),//Encryption

        // Map "role" as the role claim type
        //RoleClaimType = "role"
    };
    // Map "role" claims properly
    //options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
});

builder.Services.AddScoped<IAuthorizationHandler, AuthorizationService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiPermissionPolicy", policy =>
        policy.Requirements.Add(new ApiPermissionRequirement()));
});


//Identity Scope
builder.Services.AddScoped<IMenuResource_Repository, MenuResource_repository>();
builder.Services.AddScoped<IUser_Roles_repository, User_Roles_repository>();
builder.Services.AddScoped<IApiPermissionRole, ApiPermissionRole_repository>();

builder.Services.AddScoped<ITokenService, TokenService>();


//AddScoped Task
builder.Services.AddScoped<ILKACSoft_TaskRepository, LKACSoft_Task_repository>();

//AddScoped Process
builder.Services.AddScoped<ILKACSoft_PriorityRepository, LKACSoft_Priority_repository>();

//AddScoped DetailUser
builder.Services.AddScoped<ILKACSoft_DetailUserRepository, LKACSoft_DetailUser_repository>();

//AddScoped User
builder.Services.AddScoped<ILKACSoft_UserRepository, LKACSoft_User_repository>();

//AddScoped Notification
builder.Services.AddScoped<ILKACSoft_NotificationRepository, LKACSoft_Notification_repository>();

//AddScoped Process
builder.Services.AddScoped<ILKACSoft_ExecutionRepository, LKACSoft_Execution_repository>();

//AddScoped ProcessStatus
builder.Services.AddScoped<ILKACSoft_ProcessStatusRepository, LKACSoft_ProcessStatus_repository>();

//AddScoped ProcessSchema
builder.Services.AddScoped<ILKACSoft_ProcessSchemaRepository, LKACSoft_ProcessSchema_repository>();

//AddScoped TaskStatus
builder.Services.AddScoped<ILKACSoft_TaskStatusRepository, LKACSoft_TaskStatus_repository>();

//AddScoped TaskType
builder.Services.AddScoped<ILKACSoft_TaskTypeRepository, LKACSoft_TaskType_repository>();

//AddScoped TaskTypeResponsiblePosition
builder.Services.AddScoped<ILKACSoft_TaskTypeResponsiblePositionRepository, LKACSoft_TaskTypeResponsiblePosition_repository>();

//AddScoped DetailTask
builder.Services.AddScoped<ILKACSoft_DetailTaskRepository, LKACSoft_DetailTask_repository>();

//AddScoped DetailCustomer
builder.Services.AddScoped<ILKACSoft_DetailCustomerRepository, LKACSoft_DetailCustomer_repository>();

//AddScoped DetailProcess
builder.Services.AddScoped<ILKACSoft_DetailExecutionRepository, LKACSoft_DetailExecution_repository>();

//AddScoped DetailDocumentType
builder.Services.AddScoped<ILKACSoft_DetailDocumentTypeRepository, LKACSoft_DetailDocumentType_repository>();

//AddScoped DocumentType
builder.Services.AddScoped<ILKACSoft_DocumentTypeRepository, LKACSoft_DocumentType_repository>();

//AddScoped DetailProcessSchemaStatus
builder.Services.AddScoped<ILKACSoft_DetailProcessSchemaStatusRepository, LKACSoft_DetailProcessSchemaStatus_repository>();

//AddScoped AccountantTeam
builder.Services.AddScoped<ILKACSoft_AccountantTeamRepository, LKACSoft_AccountantTeam_repository>();

//AddScoped Department
builder.Services.AddScoped<ILKACSoft_DepartmentRepository, LKACSoft_Department_repository>();

//AddScoped JobTaskFile
builder.Services.AddScoped<ILKACSoft_JobTaskFileRepository, LKACSoft_JobTaskFile_repository>();

//AddScoped AccountingStatus
builder.Services.AddScoped<ILKACSoft_AccountingStatusRepository, LKACSoft_AccountingStatus_repository>();

//AddScoped ArchivingStatus
builder.Services.AddScoped<ILKACSoft_ArchivingStatusRepository, LKACSoft_ArchivingStatus_repository>();

//S3 AWS services

builder.Services.Configure<S3Settings>(builder.Configuration.GetSection("S3Settings"));

builder.Services.Configure<S3AWSCredentials>(builder.Configuration.GetSection("AWSCredentials"));


builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var s3Settings = sp.GetRequiredService<IOptions<S3Settings>>().Value;
    var config = new AmazonS3Config
    {
        RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(s3Settings.Region)
    };

    var awsCredentials = sp.GetRequiredService<IOptions<S3AWSCredentials>>().Value;

    // Provide explicit credentials
    var credentials = new Amazon.Runtime.BasicAWSCredentials(
           awsCredentials.AWS_ACCESS_KEY_ID,
           awsCredentials.AWS_SECRET_ACCESS_KEY
       );
    return new AmazonS3Client(credentials, config);
});


//SignalR services
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpLogging();
app.UseMiddleware<CustomLoggingMiddleware>();
app.UseMiddleware<ApiNotificationMiddleware>();

app.UseRouting(); 
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<NotificationsHub>("notifications");

app.MapControllers();


app.Run();
