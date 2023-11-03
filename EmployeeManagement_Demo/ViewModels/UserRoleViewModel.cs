using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Demo.ViewModels
{
    public class UserRoleViewModel
    {
        [MaxLength(100)]
        public string UserId { get; set; } = string.Empty;
        [MaxLength(100)]
        public string RoleId { get; set; } = string.Empty;
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}
