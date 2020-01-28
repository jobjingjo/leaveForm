using LeaveForm.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeaveForm.Data
{
    public class LeaveDbContext: DbContext
    {
        public LeaveDbContext(DbContextOptions<LeaveDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Leave> LeaveForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<Leave>().ToTable("LeaveForms");            
        }
    }
}
