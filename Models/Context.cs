using Microsoft.EntityFrameworkCore;

namespace MVC_multipleClass.Models
{
    public class Context:DbContext
    {
        public DbSet<Subject>Subjects { get; set; } 
        public DbSet<Teacher>Teachers { get; set; } 
        public DbSet<Student>Students { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(@"Data Source=UJJWAL\SQLEXPRESS;Initial Catalog=MVS_multipleClass;Integrated Security=true;trustServerCertificate=True;");
        } 

    }
}
