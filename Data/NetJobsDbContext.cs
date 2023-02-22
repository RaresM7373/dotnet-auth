using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using net_jobs.Models;

namespace net_jobs.Data;

public class NetJobsDbContext : IdentityDbContext
{
    public NetJobsDbContext(DbContextOptions<NetJobsDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Job> Jobs { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
}