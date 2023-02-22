using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using net_jobs.Data;
using net_jobs.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<NetJobsDbContext>(
    options => options.UseMySql(connString, ServerVersion.AutoDetect(connString))
);

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<NetJobsDbContext>();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Login";
    config.AccessDeniedPath = "/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();