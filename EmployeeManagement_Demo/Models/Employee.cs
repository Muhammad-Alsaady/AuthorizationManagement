using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement_Demo.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage ="Maximum Length of the name is 50 characters")]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50, ErrorMessage = "Maximum Length of the name is 50 characters")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public Department? Department { get; set; }
        [MaxLength(250, ErrorMessage = "Maximum Length of the name is 250 characters")]

        public string PhotoPath { get; set; } = string.Empty;
    }
}
