using Microsoft.AspNetCore.Identity;
using net_jobs.Interfaces;

namespace net_jobs.Services;

public class RoleService : IRoleService
{
    private readonly IConfiguration _configuration;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;


    public RoleService(RoleManager<IdentityRole> roleManager, IConfiguration configuration,
        UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;

        Task.Run(CheckForInitialRoles).Wait();
    }

    public async Task CheckForInitialRoles()
    {
        IdentityResult roleResult = null;
        string[] roleNames = { "Admin", "User", "Company" };
        foreach (var roleName in roleNames)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists) roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var userEmail = _configuration.GetValue<string>("AppSettings:UserEmail");
        var userPassword = _configuration.GetValue<string>("AppSettings:Password");

        if (!string.IsNullOrEmpty(userEmail) && !string.IsNullOrEmpty(userPassword))
        {
            var powerUser = new IdentityUser
            {
                UserName = userEmail,
                Email = userEmail
            };

            var existingUser = await _userManager.FindByEmailAsync(userEmail);

            if (existingUser == null)
            {
                var createPowerUser = await _userManager.CreateAsync(powerUser, userPassword);
                if (createPowerUser.Succeeded) await _userManager.AddToRoleAsync(powerUser, "Admin");
            }
        }

        if (roleResult != null) Console.WriteLine(roleResult.ToString());
    }
}