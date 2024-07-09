using Hangfire;
using Library_Management_System_BackEnd.Data;
using Library_Management_System_BackEnd.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Add services to the container.
builder.Services.AddDbContext<LibraryContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    );
});
builder.Services.AddHangfire(config => config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))); 

builder.Services.AddHangfireServer();

// Add Identity services to the container
builder
    .Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.SignIn.RequireConfirmedEmail = true; // Email confirmation required
        options.Lockout.MaxFailedAccessAttempts = 5; // Account lockout after 5 failed attempts
    })
    .AddEntityFrameworkStores<LibraryContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
await app.DatabaseMigrateAsync();

app.Run();
