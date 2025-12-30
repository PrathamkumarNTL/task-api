var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 🔹 Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskService,TaskService>();
builder.Services.AddScoped<ITaskRepository,TaskRepository>();

var app = builder.Build();

// 🔹 Enable Swagger ALWAYS (for now)
app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync("An unexpected error occurred.");
    });

});

// 🔴 TEMPORARILY COMMENT HTTPS REDIRECTION
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
