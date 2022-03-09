using Microsoft.EntityFrameworkCore;
using Reports.DAL.Entities;

namespace Reports.Server.Database
{
    public class ReportsDatabaseContext : DbContext
    {
        public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }
        public DbSet<TaskChange> TaskChanges { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<EmployeeTask>().ToTable("EmployeeTasks");
            modelBuilder.Entity<TaskChange>().ToTable("TaskChanges");
            modelBuilder.Entity<Report>().ToTable("Reports");
            base.OnModelCreating(modelBuilder);
        }
    }
}