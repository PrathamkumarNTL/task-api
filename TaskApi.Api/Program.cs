var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 🔹 Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskService,TaskService>();

var app = builder.Build();

// 🔹 Enable Swagger ALWAYS (for now)
app.UseSwagger();
app.UseSwaggerUI();

// 🔴 TEMPORARILY COMMENT HTTPS REDIRECTION
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
