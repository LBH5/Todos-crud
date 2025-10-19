using TodosApi.Entities;

namespace TodosApi.Repositories
{
    public interface ITodoRepository
    {
        Task AddTodo(Todo todo);
        Task<Todo?> DeleteTodoAsync(int id);
        Task<List<Todo>> GetAllTodos();
        Task<List<Todo>> GetCompletedTodos();
        Task<Todo?> GetTodo(int id);
        Task<Todo?> UpdateTodoAsync(int id, Todo todo);
    }
}