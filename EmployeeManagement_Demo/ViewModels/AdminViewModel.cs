using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Demo.ViewModels
{
    public class AdminViewModel
    {
        [MaxLength(20)]
        public string RoleName { get; set; } = string.Empty;
    }
}
