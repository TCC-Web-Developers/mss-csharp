using Microsoft.EntityFrameworkCore;
using StarterAPI.Interfaces;
using StarterAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(
    options => 
    options.UseSqlite("Data Source=./students.db")
);

//Dependency Injection - Singleton, Scoped, Transient
builder.Services.AddTransient<IApplicationDbContext>
    (provider => provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

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







