using System.ComponentModel.DataAnnotations;

namespace net_jobs.Models;

public class RegisterModel
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "The passwords must match!")]
    public string ConfirmPassword { get; set; }

    public string? Role { get; set; }
}