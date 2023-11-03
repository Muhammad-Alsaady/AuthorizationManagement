using System.Security.Claims;

namespace EmployeeManagement_Demo.ViewModels
{
    public class UserClaimsViewModel
    {
        public string userId { get; set; } = string.Empty;

        public List<UserClaim> Claims { get; set; } = new List<UserClaim>();
    }
}
