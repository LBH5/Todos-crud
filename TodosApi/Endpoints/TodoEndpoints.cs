using Microsoft.AspNetCore.Mvc;
using TodosApi.Entities;
using TodosApi.Repositories;

namespace TodosApi.Endpoints
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this WebApplication app)
        {
            app.MapGet("/",() => "Welcome to the Todo API!");
            app.MapGet("/todos",async (ITodoRepository _todoRepository) =>
            {
                var todos =await _todoRepository.GetAllTodos();
                return Results.Ok(todos);
            });
            app.MapGet("/todos/completed",async (ITodoRepository _todoRepository) =>
            {
                var completedTodos = await _todoRepository.GetCompletedTodos();
                return Results.Ok(completedTodos);
            });
            app.MapGet("/todos/{id}", async([FromRoute] int id, ITodoRepository _todoRepository) =>
            {
                var todo =await _todoRepository.GetTodo(id);
                return todo is not null ? Results.Ok(todo) : Results.NotFound();
            });
            app.MapPost("/todos", ([FromBody] Todo todo, ITodoRepository _todoRepository) =>
            {
                _todoRepository.AddTodo(todo);
                return Results.Created($"/todos/{todo.Id}", todo);
            });
            app.MapPut("/todos/{id}",async ([FromRoute] int id, [FromBody] Todo todo, ITodoRepository _todoRepository) =>
            {
                var updatedTodo =await _todoRepository.UpdateTodoAsync(id, todo);
                return updatedTodo is not null ? Results.Ok(updatedTodo) : Results.NotFound();
            });
            app.MapDelete("/todos/{id}", async ([FromRoute] int id, ITodoRepository _todoRepository) =>
            {
                var deletedTodo = await _todoRepository.DeleteTodoAsync(id);
                return deletedTodo is not null ? Results.NoContent() : Results.NotFound();
            });

        }
    }
}
