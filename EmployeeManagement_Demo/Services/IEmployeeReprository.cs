using EmployeeManagement_Demo.Models;
using EmployeeManagement_Demo.ViewModels;

namespace EmployeeManagement_Demo.Services
{
    public interface IEmployeeReprository
    {
        public List<Employee> GetEmployees();
        public Employee GetEmployeeByID(int? id);
        public void AddEmployee(EmployeeCreateViewModel model);
        public void UpdateEmployee(int? id, EmployeeEditViewModel emp);
        public void DeleteEmployee(int? id);
    }
}
