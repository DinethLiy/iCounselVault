using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using icounselvault.Utility;
using icounselvault.Utility.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using icounselvault.Business.Interfaces.Auth;
using icounselvault.Business.Services.Auth;
using icounselvault.Business.Interfaces.Client;
using icounselvault.Business.Services.Client;
using icounselvault.Business.Interfaces.Admin;
using icounselvault.Business.Services.Admin;
using icounselvault.Business.Interfaces.Dashboard;
using icounselvault.Business.Services.Dashboard;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Database Connection
var connectionString = builder.Configuration.GetConnectionString("MainConnection");
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Authentication
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>();

// Controller-View association
builder.Services.AddControllersWithViews();

// Token-based security
var key = builder.Configuration.GetSection("AppSettings:TokenKey").Value;
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key))
    };
});
builder.Services.AddSingleton<IJwtAuth>(new Auth(key));

// Session-based storage
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpContextAccessor();

// Interface-Service Initialization
// Auth
builder.Services.AddScoped<IAuthService, AuthService>();
// Admin
builder.Services.AddScoped<IReportService, ReportService>();
// Client
builder.Services.AddScoped<IClientSurveyService, ClientSurveyService>();
// Dashboards
builder.Services.AddScoped<ISuperAdminDashboardService, SuperAdminDashboardService>();
builder.Services.AddScoped<IAdminDashboardService, AdminDashboardService>();
builder.Services.AddScoped<ICounselorDashboardService, CounselorDashboardService>();
builder.Services.AddScoped<IClientDashboardService, ClientDashboardService>();


// Add services to the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Service initialization for app
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

// Set initial view
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();
