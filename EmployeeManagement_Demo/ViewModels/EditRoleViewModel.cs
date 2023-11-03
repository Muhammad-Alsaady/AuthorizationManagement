namespace EmployeeManagement_Demo.ViewModels
{
    public class EditRoleViewModel
    {
        public string Id { get; set; }

        public string RoleName { get; set; } = string.Empty;

        public List<string> Users { get; set; } = new List<string>();
    }
}
