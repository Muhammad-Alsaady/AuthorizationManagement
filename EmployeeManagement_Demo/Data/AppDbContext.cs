using EmployeeManagement_Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeeManagement_Demo.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {      
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /// Seeding Employees Table
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "Mary", Email = "mary@egtech.com", Department = Department.Engineer },
                new Employee() { Id = 2, Name = "Mark", Email = "mark@egtech.com", Department = Department.IT },
                new Employee() { Id = 3, Name = "Kareem", Email = "kareem@egtech.com", Department = Department.Management },
                new Employee() { Id = 4, Name = "Fady", Email = "fady@egtech.com", Department = Department.Accounting }
                );
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        public virtual DbSet<Employee> Employees { get; set; }
    }
}
