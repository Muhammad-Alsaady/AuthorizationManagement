//using EmployeeManagement_Demo.Models;

//namespace EmployeeManagement_Demo.Services
//{
//    public class EmployeeService : IEmployeeReprository
//    {
//        private static List<Employee> _employee;

//        public EmployeeService()
//        {
//            _employee = new List<Employee>
//            {
//                new Employee() { Id = 1, Name = "Sara", Email = "sara@egtech.com", Department = Department.Accounting },
//                new Employee() { Id = 2, Name = "John", Email = "john@egtech.com", Department = Department.Engineer },
//                new Employee() { Id = 3, Name = "Smith", Email = "smith@egtech.com", Department = Department.IT },
//                new Employee() { Id = 4, Name = "Brono", Email = "brono@egtech.com", Department = Department.Management },

//            };
  
//        }

//        public Employee AddEmployee(Employee emp)
//        {
//            emp.Id = _employee.Max(x => x.Id) + 1;
//            _employee.Add(emp);
//            return emp;
//        }

//        public Employee DeleteEmployee(int? id)
//        {
//            var emp = _employee.FirstOrDefault(x => x.Id == id);
//            if (emp != null)
//            {
//                _employee.Remove(emp);
//            }
//            return emp;
//        }

//        public Employee GetEmployeeByID(int? id)
//        {
//            return _employee.FirstOrDefault(emp => emp.Id == id);
//        }

//        public List<Employee> GetEmployees()
//        {
//            return _employee;
//        }

//        public Employee UpdateEmployee(Employee emp)
//        {
//            return emp;
//        }
//    } 
//}

