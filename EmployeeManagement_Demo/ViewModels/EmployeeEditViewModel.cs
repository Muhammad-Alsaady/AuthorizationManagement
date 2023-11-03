namespace EmployeeManagement_Demo.ViewModels
{
    public class EmployeeEditViewModel: EmployeeCreateViewModel
    {
        public int id { get; set; }
        public string existedPhoto { get; set; } = string.Empty;
    }
}
