using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using net_jobs.Data;
using net_jobs.Models;

namespace net_jobs.Pages.Admin;

public class Companies : PageModel
{
    private readonly NetJobsDbContext _context;

    public Companies(NetJobsDbContext context)
    {
        _context = context;
    }

    [BindProperty] public List<Company> CompaniesList { get; set; }

    public void OnGet()
    {
        CompaniesList = _context.Companies.Include(c => c.Jobs).ToList();
    }
}