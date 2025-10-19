using Microsoft.EntityFrameworkCore;
using TodosApi.Data;
using TodosApi.Endpoints;
using TodosApi.Entities;
using TodosApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add DI - AddService
builder.Services.AddDbContext<TodosDbContext>(opt =>opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddTransient<ITodoRepository, TodoRepository>();

// Adding swagger support (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure piplline - UseMethod (Middleware)

// Map Endpoints
app.MapTodoEndpoints();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodosDbContext>();
    // Ensure the database is created, which is a no-op for in-memory
    // but good practice.
    db.Database.EnsureCreated();

    // Add seed data if the database is empty
    if (!db.Todos.Any())
    {
        db.Todos.AddRange(
            new Todo { Title = "Buy groceries", IsCompleted = false },
            new Todo { Title = "Learning .net core microservices", IsCompleted = false },
            new Todo { Title = "Walk the dog", IsCompleted = true }
        );
        db.SaveChanges();
    }
}
app.Run();



