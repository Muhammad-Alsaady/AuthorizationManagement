using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement_Demo.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string City { get; set; } = default!;
    }
}
