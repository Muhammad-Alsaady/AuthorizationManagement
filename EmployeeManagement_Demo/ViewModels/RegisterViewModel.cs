using EmployeeManagement_Demo.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Demo.ViewModels
{
    public class RegisterViewModel
    {
        [EmailAddress, MaxLength(50)]
        [Remote(action: "isEmailInUse", controller:"Account")]
        [ValidateEmailDomain("egtech.com", "test.com", ErrorMessage = "Not Allowed domain for Email Address")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public string City { get; set; } = string.Empty;
    }
}
