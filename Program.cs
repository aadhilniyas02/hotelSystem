using dealSystem.Data;
using dealSystem.services;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// POSTGRE SQL database context
builder.Services.AddDbContext<DealContext>(options => 
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register Services
builder.Services.AddScoped<IDealService>();


// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
