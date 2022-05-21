using Microsoft.EntityFrameworkCore;
using StarterAPI.Commons.Extensions;
using StarterAPI.Interfaces;
using StarterAPI.Middleware;
using StarterAPI.Persistence;
using StarterAPI.Services;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Services.AddAppDbContext();
builder.Services.AddMappingConfiguration();
builder.Services.AddCors();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAppDependencies();

var app = builder.Build();


// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    AppDbSeeder.CreateMockStudentsIfNotExists(app);

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x
   .AllowAnyOrigin()
   .AllowAnyMethod()
   .AllowAnyHeader());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();


// dotnet ef

// dotnet ef migrations

// dotnet ef migrations add "Initial"

// dotnet ef database

// add additional properties to student entity

// add new migration --
// dotnet ef migrations add "Alter Student Entity"


// dotnet ef database update







