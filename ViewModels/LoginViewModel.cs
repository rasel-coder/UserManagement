using System.ComponentModel.DataAnnotations;

namespace UserManagement.ViewModels;

public class LoginViewModel
{
    [Required]
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}
