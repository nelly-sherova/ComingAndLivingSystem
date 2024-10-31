using ComingAndLivingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ComingAndLivingSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base (options)  
        {

        }
        public DbSet<Employee> Employees { get; set; }  
        public DbSet<JobTitle> JobTitles { get; set; }
        public DbSet<Shift> Shifts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
