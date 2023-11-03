using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Demo.ViewModels
{
    public class EditUserViewModel
    {
        [MaxLength(100)]
        public string Id { get; set; } = string.Empty;
        [MaxLength(100)]
        public string UserName { get; set; } = string.Empty;
        [MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [MaxLength(100)]
        public string City { get; set; } = default!;

        public List<string> Roles { get; set; } = new List<string>();
        public List<string> Claims { get; set; } = new List<string>();


    }
}
