using Microsoft.EntityFrameworkCore;
using WEBAPI.Data; // 👈 AppDbContext namespace

var builder = WebApplication.CreateBuilder(args);

// ✅ Add EF Core SQLite DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={Path.Combine("..", "SharedData", "VirtualEvent.db")}")); // dynamic path

// ✅ Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Add CORS BEFORE builder.Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ✅ Add controller support
builder.Services.AddControllers();

// ✅ Build the app AFTER all services are added
var app = builder.Build();

// ✅ Use CORS middleware
app.UseCors("AllowAll");

// ✅ Swagger middlewares
app.UseSwagger();
app.UseSwaggerUI(); // Enables Swagger UI at /swagger

// ✅ Other middlewares
app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ Route config
app.MapControllers();

app.Run();
