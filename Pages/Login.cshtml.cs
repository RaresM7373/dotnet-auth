using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net_jobs.Models;

namespace net_jobs.Pages;

public class Login : PageModel
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public Login(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    [BindProperty] public Models.Login Model { get; set; }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (!ModelState.IsValid) return Page();


        var result = await _signInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.RememberMe, false);


        if (result.Succeeded)
            return string.IsNullOrWhiteSpace(returnUrl) ? RedirectToPage("Index") : RedirectToPage(returnUrl);

        ModelState.AddModelError("password", "Username or password incorrect");
        return Page();
    }
}