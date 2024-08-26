using System.Data.Entity;
using TaskManagementApp.Models;

namespace TaskManagementApp.Contexts
{
    internal class UserContext : DbContext
    {

        public UserContext(string connectionString) : base(connectionString)
        {

        }



        // Create User Table
        public DbSet<User> Users { get; set; }

    }
}
