using System.ComponentModel.DataAnnotations;

namespace net_jobs.Models;

public class Company
{
    public int Id { get; set; }
    [Required] [StringLength(48)] public string Name { get; set; }
    [Required] [StringLength(48)] public string Address { get; set; }
    [Required] public int Employees { get; set; }
    [Required] public string Description { get; set; }
    public bool Hiring { get; set; } = true;

    public List<Job> Jobs { get; set; }
}