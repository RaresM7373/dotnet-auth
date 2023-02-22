using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace net_jobs.Models;

public class Application
{
    public IdentityUser User;

    public int Id { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required] [StringLength(500)] public string CoverLetter { get; set; }

    public string UserId { get; set; }

    public int JobId { get; set; }
    public Job Job { get; set; }
}