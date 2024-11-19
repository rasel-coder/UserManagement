using Microsoft.AspNetCore.Identity;

namespace UserManagement.Data;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string? Designation { get; set; }
    public DateTime LastLoggedIn { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
