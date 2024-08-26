using System.ComponentModel.DataAnnotations;

namespace TaskManagementApp.Models
{
    internal class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
