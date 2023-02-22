using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net_jobs.Data;
using net_jobs.Models;

namespace net_jobs.Pages;

public class IndexModel : PageModel
{
    private readonly NetJobsDbContext _context;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, NetJobsDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty] public List<Job> Jobs { get; set; }

    public void OnGet()
    {
    }
}