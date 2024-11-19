using Microsoft.AspNetCore.Identity;
using UserManagement.Data;

namespace UserManagement.Utility;

public static class Extensions
{
    public static async Task<string> GetFullNameAsync(this UserManager<ApplicationUser> userManager, string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        return user != null ? $"{user.LastName}, {user.FirstName}" : string.Empty;
    }
}