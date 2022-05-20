using Lottery.Data;
using Lottery.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(connectionString)
    options.UseLazyLoadingProxies().UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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
builder.Services.AddAuthentication().AddGoogle(googleOptions =>
{
    googleOptions.ClientId = "281699556061-m95drffeeovvntbve8imk07juoabs2k7.apps.googleusercontent.com";
    googleOptions.ClientSecret = "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";
});

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
app.UseSession();
app.MapHub<ChatHub>("/chat");

app.Run();
