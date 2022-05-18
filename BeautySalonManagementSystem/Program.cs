using BeautySalonManagementSystem.RepositoryServices.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BeautySalonContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("BeautySalonContextDB")));

builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
       .AllowAnyOrigin()
       .AllowAnyMethod()
       .WithHeaders("Accept", "Content-Type", "Origin", "X-My-Header"));

app.UseHttpsRedirection();

/*app.UseAuthorization();*/

app.MapControllers();

app.Run();