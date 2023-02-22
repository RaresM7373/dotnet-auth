using System.ComponentModel.DataAnnotations;

namespace net_jobs.Models;

public class Job
{
    public int Id { get; set; }
    [Required] [StringLength(24)] public string Title { get; set; }
    [Required] [StringLength(500)] public string Description { get; set; }
    [Required] public string ExperienceLevel { get; set; }
    [Required] public string AnualSalary { get; set; }

    public int CompanyId { get; set; }
    public Company Company { get; set; }

    public List<Application> Applications { get; set; }
}