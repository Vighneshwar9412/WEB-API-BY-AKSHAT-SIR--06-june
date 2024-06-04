using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MobAppCoreAPI;
using MobAppCoreAPI.Data;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Repository;
using System.Text;
using Microsoft.Extensions.FileProviders;
using MobAppCoreAPI.Interfaces.Portal;
using MobAppCoreAPI.Repository.Portal;
using MobAppCoreAPI.Interfaces.General;
using MobAppCoreAPI.Repository.General;
using MobAppCoreAPI.Interfaces.Portal_P2;
using MobAppCoreAPI.Interfaces.Employee;
using MobAppCoreAPI.Repository.Employee;
using MobAppCoreAPI.Interfaces.ChannelPartner;
using MobAppCoreAPI.Repository.ChannelPartner;
using MobAppCoreAPI.Interfaces.InventoryGUI;
using MobAppCoreAPI.Repository.InventoryGUI;
using MobAppCoreAPI.Controllers;
using MobAppCoreAPI.Repository.Construction;
using MobAppCoreAPI.Hub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
builder.Services.AddScoped<MobAppCoreAPI.Interfaces.IValidateKey, MobAppCoreAPI.Repository.ValidateKeyRepository>();
builder.Services.AddScoped<IListAllEnquiryMasters, ListAllEnquiryMastersRepository>();
builder.Services.AddScoped<ILeadIcons, LeadIconsRepository>();
builder.Services.AddScoped<ItodaysiteVisit, ListalltodaySiteVisitRepository>();
builder.Services.AddScoped<IDashboard_GetTodayLeads, Dashboard_GetTodayLeadsRepository>();
builder.Services.AddScoped<ILogin, LoginRepository>();
builder.Services.AddScoped<IListAllLeads, ListAllLeadsRepository>();
builder.Services.AddScoped<ISVDoneList, SVDoneListRepository>();
builder.Services.AddScoped<IFollowUplisting, FollowUpListingRepository>();
builder.Services.AddScoped<Idashboard, DashboardRepository>();
builder.Services.AddScoped<ILogout, LogoutRepository>();
builder.Services.AddScoped<ILeadSave, LeadSaveRepository>();
builder.Services.AddScoped<IUpdateCustomer, UpdateCustomerRepository>();
builder.Services.AddScoped<ITransferUserList, TransferuserlistRepository>();
builder.Services.AddScoped<ITransferProcess, TransferProcessRepository>();
builder.Services.AddScoped<IFollowUpSave, SaveFollowUpRepository>();
builder.Services.AddScoped<ILeadDump, LeadDumpRepository>();
builder.Services.AddScoped<ISaveSVLocation, SaveSVLocationRepository>();
builder.Services.AddScoped<ILeadSuccess, LeadSuccessRepository>();
builder.Services.AddScoped<ISendleadSMS, SendLeadSMSRepository>();
builder.Services.AddScoped<ILeadSendEmail, SendLeadEmailRepository>();
builder.Services.AddScoped<IProjectDocs, ProjectDocsRepository>();
builder.Services.AddScoped<IUpdateRequirement, UpdateRequirementRepository>();
builder.Services.AddScoped<IReports, ReportsRepository>();
builder.Services.AddScoped<IClickToCall, ClickToCallRepository>();
builder.Services.AddScoped<IPortalCustomer, CustomerRepository>();
builder.Services.AddScoped<IPortalLogin, LoginRepositoryPortal>();
builder.Services.AddScoped<IGeneralHomepage, GeneralHomePageRepository>();
builder.Services.AddScoped<IPortalCustomer_P2, CustomerRepository_P2>();
builder.Services.AddScoped<IPortalLogin_P2, LoginRepositoryPortal_P2>();
builder.Services.AddScoped<ICommonLogin , CommonLoginRepository>();
builder.Services.AddScoped<MobAppCoreAPI.Interfaces.General.IValidateKey, MobAppCoreAPI.Repository.General.ValidateKeyRepository>();
builder.Services.AddScoped<IPortalHR, HRRepositoryPortal>();
builder.Services.AddScoped<IPortalFirebase, FirebaseRepositoryPortal>();
builder.Services.AddScoped<IInventory, InventoryRepository>();
builder.Services.AddScoped<IInventoryOperations, InventoryOperationsRepository>();
builder.Services.AddScoped<IEmployee, EmployeeRepository>();
builder.Services.AddScoped<IChannelPartnerLead, ChannelPartnerLeadRepository>();
builder.Services.AddScoped<IChannelPartner, ChannelPartnerRepository>();
builder.Services.AddScoped<IInventoryGUI, InventoryGUIRepository>();
builder.Services.AddScoped<IMobileAppCall, MobAppCallRepository>();
builder.Services.AddScoped<IGetLeadDetails, GetLeadDetailsRepository>();
builder.Services.AddScoped<IGetTeamWiseEmployees, GetTeamWiseEmployeesRepository>();
builder.Services.AddScoped<IHR, HRRepository>();
builder.Services.AddScoped<ILeadInventory, LeadInventoryRepository>();

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string key = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    /* For multiple definitions */
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "MobAppCoreAPI", Version = "v1" });
    x.SwaggerDoc("p1", new OpenApiInfo { Title = "CustomerPortal", Version = "p1" });
    x.SwaggerDoc("cp1", new OpenApiInfo { Title = "ChannelPartner", Version = "cp1" });
    x.SwaggerDoc("e1", new OpenApiInfo { Title = "Employee", Version = "e1" });
    x.SwaggerDoc("i1", new OpenApiInfo { Title = "Inventory", Version = "i1" });
    x.SwaggerDoc("g1", new OpenApiInfo { Title = "General", Version = "g1" });
    x.SwaggerDoc("p2", new OpenApiInfo { Title = "CustomerPortal New", Version = "p2" });
    x.SwaggerDoc("ig1", new OpenApiInfo { Title = "Inventory GUI", Version = "ig1" });
    x.SwaggerDoc("con1", new OpenApiInfo { Title = "Construction", Version = "con1" });
    x.SwaggerDoc("m1", new OpenApiInfo { Title = "Miscellaneous", Version = "m1" });

    x.EnableAnnotations();
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
                .AddJwtBearer(x =>
                {                   
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

builder.Services.AddSignalR();
//builder.Services.AddCors();

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyHeader()
                   .AllowAnyMethod()
                   .SetIsOriginAllowed((host) => true)
                   .AllowCredentials();
        }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MobAppCoreAPI");
        c.SwaggerEndpoint("/swagger/p1/swagger.json", "CustomerPortal");
        c.SwaggerEndpoint("/swagger/cp1/swagger.json", "ChannelPartner");
        c.SwaggerEndpoint("/swagger/e1/swagger.json", "Employee");
        c.SwaggerEndpoint("/swagger/i1/swagger.json", "Inventory");
        c.SwaggerEndpoint("/swagger/g1/swagger.json", "General");
        c.SwaggerEndpoint("/swagger/p2/swagger.json", "CustomerPortal New");
        c.SwaggerEndpoint("/swagger/ig1/swagger.json", "Inventory GUI");
        c.SwaggerEndpoint("/swagger/con1/swagger.json", "Construction");
        c.SwaggerEndpoint("/swagger/m1/swagger.json", "Miscellaneous");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "images")),
    RequestPath = "/images"
});
//Enable directory browsing
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "images")),
    RequestPath = "/images"
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

app.UseEndpoints(endpoints => {
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/signalR");
});

app.MapControllers();

app.Run();
