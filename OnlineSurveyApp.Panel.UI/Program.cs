using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using OnlineSurveyApp.DataAccess.Concrete;
using OnlineSurveyApp.EntityLayer.Entities;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<Context>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Identity Start
//Identity Start
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.Password.RequireDigit = true;      
    x.Password.RequireLowercase = true;     
    x.Password.RequireUppercase = true;     
    x.Password.RequireNonAlphanumeric = true; 
    x.Password.RequiredLength = 8;           
}).AddEntityFrameworkStores<Context>();
//Identity End


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 4;
    options.Lockout.AllowedForNewUsers = true;
});
//Identity End




// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

//Cookie start 

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddMvc();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Account/SignIn/";
    }
    );







var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

//
app.UseAuthentication();
app.UseSession();
//

//404 page
app.UseStatusCodePagesWithReExecute("/Error/Error404", "?code={0}");

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Homepage}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "TestStart",
        pattern: "Test/Start/{testId}", 
        defaults: new { controller = "Test", action = "Start" }
    );

});


app.Run();
