using System.ComponentModel.DataAnnotations;

namespace UserManagement.ViewModels;

public class SignUpViewModel
{
    [Required]
    [Display(Name = "First Name")]
    public string? FirstName { get; set; }


    [Required]
    [Display(Name = "Last Name")]
    public string? LastName { get; set; }

    [Display(Name = "Company Name")]
    public string? CompanyName { get; set; }
    public string? Designation { get; set; }
    
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
    
    [Required]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
}
