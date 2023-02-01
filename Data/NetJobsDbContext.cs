using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace net_jobs.Data;

public class NetJobsDbContext : IdentityDbContext
{
    public NetJobsDbContext(DbContextOptions<NetJobsDbContext> options) : base(options)
    {
    }
}
