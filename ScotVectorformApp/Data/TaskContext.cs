using Microsoft.EntityFrameworkCore;

namespace ScotVectorformApp.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options)
                       : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ToDoDatabase.sqlite");
        }

        public DbSet<Models.Task> Tasks { get; set; }
    }
}
