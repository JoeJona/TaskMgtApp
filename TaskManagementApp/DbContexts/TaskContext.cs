using System.Data.Entity;
using Task = TaskManagementApp.Models.Task;

namespace TaskManagementApp.DbContexts
{
    internal class TaskContext : DbContext
    {
        public TaskContext(string connectionString) : base(connectionString)
        {

        }


        // Create Task Table
        public DbSet<Task> Tasks { get; set; }
    }
}
