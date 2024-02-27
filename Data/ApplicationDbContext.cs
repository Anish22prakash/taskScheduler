using Microsoft.EntityFrameworkCore;
using TaskSchedulerAPI.Model;

namespace TaskSchedulerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }
        
       public DbSet<User> Users { get; set; }
       public DbSet<TaskAssignment> TaskAssignments { get; set; }
    }
}
