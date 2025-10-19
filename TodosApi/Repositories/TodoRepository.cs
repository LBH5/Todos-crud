using Microsoft.EntityFrameworkCore;
using TodosApi.Data;
using TodosApi.Entities;

namespace TodosApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {

        private readonly TodosDbContext _dbContext;

        public TodoRepository(TodosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Todo>> GetAllTodos() => await _dbContext.Todos.ToListAsync();

        public async Task<Todo?> GetTodo(int id) => await _dbContext.Todos.FindAsync(id);

        public async Task AddTodo(Todo todo)
        {
            if (todo is null) return;
            await _dbContext.Todos.AddAsync(todo);
            await _dbContext.SaveChangesAsync();

        }
        public async Task<Todo?> UpdateTodoAsync(int id, Todo todo)
        { 
            var ExistingTodo = await GetTodo(id);
            if (ExistingTodo == null) return null;
            ExistingTodo.Title = todo.Title;
            ExistingTodo.IsCompleted = todo.IsCompleted;
            await _dbContext.SaveChangesAsync();
            return ExistingTodo;
        }

        public async Task<Todo?> DeleteTodoAsync(int id)
        {
            var todo = await GetTodo(id);
            if (todo == null) return null;
            _dbContext.Todos.Remove(todo);
            await _dbContext.SaveChangesAsync();
            return todo;
        }

        public async Task<List<Todo>> GetCompletedTodos()
        {
            return await _dbContext.Todos
                .Where(x => x.IsCompleted)
                .ToListAsync();
        }
    }
}
