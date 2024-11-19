using System.ComponentModel.DataAnnotations;

namespace UserManagement.ViewModels;

public class UserActionViewModel
{
    public UserActionViewModel()
    {
        UserIds = new List<string>();
    }

    public List<string> UserIds { get; set; }
    public string ActionName { get; set; }
}
