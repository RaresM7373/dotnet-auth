using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net_jobs.Data;
using net_jobs.Models;

namespace net_jobs.Pages;

public class Apply : PageModel
{
    private readonly NetJobsDbContext _context;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public Apply(NetJobsDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [BindProperty] public Application ApplicationModel { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var jobId = Request.Query["id"];
        var userId = _userManager.GetUserId(User);
        Console.WriteLine("User id is " + userId);

        if (!string.IsNullOrEmpty(jobId) && !string.IsNullOrEmpty(userId))
        {
            var application = new Application
            {
                Email = ApplicationModel.Email,
                PhoneNumber = ApplicationModel.PhoneNumber,
                CoverLetter = ApplicationModel.CoverLetter,
                JobId = int.Parse(jobId),
                UserId = userId
            };

            await _context.Applications.AddAsync(application);

            var job = await _context.Jobs.FindAsync(int.Parse(jobId));


            job.Applications.Add(application);
            await _context.SaveChangesAsync();
        }

        ApplicationModel = null;
        return RedirectToPage("Index");
    }

    public void OnGet()
    {
    }
}