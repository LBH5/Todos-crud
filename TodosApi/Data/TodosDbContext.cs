using Microsoft.EntityFrameworkCore;
using TodosApi.Entities;

namespace TodosApi.Data
{
    public class TodosDbContext:DbContext
    {
        public TodosDbContext(DbContextOptions<TodosDbContext> options) : base(options)
        {}
        public DbSet<Todo> Todos { get; set; }
    }
}
