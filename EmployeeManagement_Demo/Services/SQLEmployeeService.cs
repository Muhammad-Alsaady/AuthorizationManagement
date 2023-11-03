using EmployeeManagement_Demo.Data;
using EmployeeManagement_Demo.Models;
using EmployeeManagement_Demo.ViewModels;

namespace EmployeeManagement_Demo.Services
{
    public class SQLEmployeeService : IEmployeeReprository
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string imagePath;

        public SQLEmployeeService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            imagePath = $"{webHostEnvironment.WebRootPath}/images";

        }

        public void AddEmployee(EmployeeCreateViewModel model)
        {
            /// Create a unique name for each photo in oreder to prevent the duplicate names and overriding
            /// Name: Global Unique Identifer followed by Photo extension
            var imageUniqueName = $"{Guid.NewGuid()}_{Path.GetExtension(model.Photo.FileName)}";

            /// Full Path to save in the DB --> [wwwroot/images/asd74897564asd487da89s7d97_.png]
            var uploadedImagePath = Path.Combine(imagePath, imageUniqueName);
            using var stream = File.Create(uploadedImagePath);
            model.Photo.CopyTo(stream);

            Employee employee = new Employee()
            {
                Name = model.Name,
                Email = model.Email,
                Department = model.Department,
                PhotoPath = imageUniqueName,
            };

            context.Employees.Add(employee);
            context.SaveChanges();


        }

        public void DeleteEmployee(int? id)
        {
            var emp = context.Employees.Find(id);
            if (emp != null)
            {
                context.Employees.Remove(emp);
                context.SaveChanges();
            }
            
        }

        public Employee GetEmployeeByID(int? id)
        {
            var emp = context.Employees.Find(id);
            return emp;
        }

        public List<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public void UpdateEmployee(int? id, EmployeeEditViewModel emp)
        {

            //employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //context.SaveChanges();
            

            var imageUniqueName = $"{Guid.NewGuid()}_{Path.GetExtension(emp.Photo.FileName)}";   
            var uploadedImagePath = Path.Combine(imagePath, imageUniqueName);
            using var stream = File.Create(uploadedImagePath);
            emp.Photo.CopyTo(stream);

            var editedEmployee = context.Employees.Find(id);
            if (editedEmployee != null)
            {
                editedEmployee.Name = emp.Name;
                editedEmployee.Email = emp.Email;
                editedEmployee.Department = emp.Department;
                editedEmployee.PhotoPath = imageUniqueName;
            }
            context.SaveChanges();

        }

        
    }
}
