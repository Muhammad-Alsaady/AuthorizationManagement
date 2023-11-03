using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Demo.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress, MaxLength(50)]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
