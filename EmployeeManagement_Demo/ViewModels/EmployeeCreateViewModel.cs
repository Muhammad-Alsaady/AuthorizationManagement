using EmployeeManagement_Demo.Models;
using EmployeeManagement_Demo.Utilities;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Demo.ViewModels
{
    public class EmployeeCreateViewModel
    {
        
        [MaxLength(50, ErrorMessage = "Maximum Length of the name is 50 characters")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50, ErrorMessage = "Maximum Length of the name is 50 characters")]

        [EmailAddress]
        [ValidateEmailDomain("egtech.com", "test.com", ErrorMessage = "Not Allowed domain for Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required]
        public Department? Department { get; set; }
        public IFormFile Photo { get; set; } 
    }
}
