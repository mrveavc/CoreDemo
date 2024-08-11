
using Microsoft.EntityFrameworkCore;
namespace BlogApiDemo.DataAccessLayer
{
    public class ApiContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=DESKTOP-KMBI23L\\SQLEXPRESS;Database=CoreBlogApiDb;TrustServerCertificate=true;Integrated Security=true;");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
