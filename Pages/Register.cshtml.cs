using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net_jobs.Models;

namespace net_jobs.Pages;

public class Register : PageModel
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;


    public Register(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [BindProperty] public RegisterModel Model { get; set; }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();


        var user = new ApplicationUser
        {
            UserName = Model.Email,
            Email = Model.Email
        };

        var result = await _userManager.CreateAsync(user, Model.Password);


        if (result.Succeeded)
        {
            var role = string.IsNullOrEmpty(Model.Role) ? "Admin" : Model.Role;

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists) await _roleManager.CreateAsync(new IdentityRole(role));
            var roleResult = await _userManager.AddToRoleAsync(user, role);

            if (roleResult.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToPage("Index");
            }
        }


        return Page();
    }
}