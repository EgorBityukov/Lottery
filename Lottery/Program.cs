using Lottery.Data;
using Lottery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));;

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();;

var config = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(connectionString)
    options.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IUserInfoService, UserInfoService>();
builder.Services.AddTransient<IDrawService, DrawService>();
builder.Services.AddTransient<ILotService, LotService>();
builder.Services.AddTransient<IEmailSender, GmailEmailService>();

builder.Services.AddSignalR();
builder.Services.AddCors();

//builder.Services.AddAuthentication()
//.AddGoogle(options =>
// {
//     IConfigurationSection googleAuthNSection =
//     config.GetSection("Authentication:Google");
//     options.ClientId = googleAuthNSection["ClientId"];
//     options.ClientSecret = googleAuthNSection["ClientSecret"];
// });
//builder.Services.AddAuthentication().AddGoogle(googleOptions =>
//{
//    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
//    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Lots}/{action=Index}/{id?}");
app.MapRazorPages();

app.UseCors();
app.MapHub<ChatHub>("/chat");

app.Run();
