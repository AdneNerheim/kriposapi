using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecurityWorkshop.Data;
using SecurityWorkshop.Models;
using SecurityWorkshop.Repositories;
using SecurityWorkshop.Repositories.Interfaces;
using SecurityWorkshop.Services;
using SecurityWorkshop.Services.Interfaces;
using SecurityWorkshop.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITopSecretRepository, TopSecretRepository>();
builder.Services.AddScoped<ITopSecretService, TopSecretService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.MapControllers();

app.Run();