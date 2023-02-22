using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using net_jobs.Data;
using net_jobs.Models;

namespace net_jobs.Pages.Admin;

public class CreateJob : PageModel
{
    private readonly NetJobsDbContext _context;

    public CreateJob(NetJobsDbContext context)
    {
        _context = context;
    }

    [BindProperty] public int SelectedCompany { get; set; }
    [BindProperty] public List<SelectListItem> CompaniesOptions { get; set; }
    [BindProperty] public Job JobModel { get; set; }

    public void OnGet()
    {
        CompaniesOptions =
            _context.Companies.Select(company => new SelectListItem
                { Value = company.Id.ToString(), Text = company.Name }).ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var job = new Job
        {
            Title = JobModel.Title,
            Description = JobModel.Description,
            AnualSalary = JobModel.AnualSalary,
            ExperienceLevel = JobModel.ExperienceLevel,
            CompanyId = SelectedCompany
        };

        await _context.Jobs.AddAsync(job);

        var company = await _context.Companies.FindAsync(SelectedCompany);
        Console.WriteLine(company.Name);
        company.Jobs.Add(job);


        await _context.SaveChangesAsync();

        return Page();
    }
}