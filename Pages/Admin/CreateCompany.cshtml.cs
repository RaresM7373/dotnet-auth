using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using net_jobs.Data;
using net_jobs.Models;

namespace net_jobs.Pages.Admin;

[Authorize(Roles = "Admin")]
public class CreateCompany : PageModel
{
    private readonly NetJobsDbContext _context;

    public CreateCompany(NetJobsDbContext context)
    {
        _context = context;
    }

    [BindProperty] public Company CompanyModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var company = new Company
        {
            Name = CompanyModel.Name, Description = CompanyModel.Description, Address = CompanyModel.Address,
            Employees = CompanyModel.Employees
        };

        await _context.Companies.AddAsync(company);
        await _context.SaveChangesAsync();
        ModelState.Clear();
        CompanyModel = null;
        return Page();
    }
}