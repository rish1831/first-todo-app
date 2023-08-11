using Microsoft.EntityFrameworkCore;

namespace first_api_project.Models
{
    public class TodoContext : DbContext
    {
        public DbSet<TodoItem> TodoInfo { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }
    }
}
