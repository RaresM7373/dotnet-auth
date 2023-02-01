using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net_jobs.Models;

namespace net_jobs.Pages;

public class Register : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;


    public Register(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty] public RegisterModel Model { get; set; }


    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();


        var user = new IdentityUser
        {
            UserName = Model.Email,
            Email = Model.Email
        };

        var result = await _userManager.CreateAsync(user, Model.Password);


        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return RedirectToPage("Index");
        }

        foreach (var error in result.Errors) ModelState.AddModelError("", error.Description);

        return Page();
    }
}