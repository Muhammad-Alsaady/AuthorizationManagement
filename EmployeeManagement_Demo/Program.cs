using EmployeeManagement_Demo.Data;
using EmployeeManagement_Demo.Models;
using EmployeeManagement_Demo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews(options =>
{
    var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .Build();

    options.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddAuthorization(options => {
    options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role"));
    options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role"));
    options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));
});

var ConnectionString = builder.Configuration.GetConnectionString("DbConn") ??
        throw new InvalidOperationException("No Database Connection is Stablished");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString: ConnectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequiredLength = 10;
    }).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<IEmployeeReprository, SQLEmployeeService>();

//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequiredLength = 10;
//    /// And so on to override the default rules
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
