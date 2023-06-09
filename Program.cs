using api_arduino;
using api_arduino.Interfaces;
using api_arduino.Services;
using Hangfire;
using Hangfire.MySql;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();

// CORS
builder.Services.AddCors(o => o.AddPolicy("all", b => b.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));

// Database
var connectionString = "server=localhost;user=root;password=123456;database=arduino;Allow User Variables=true;";
var serverVersion = new MySqlServerVersion(new Version(10, 11, 2));

builder.Services.AddDbContext<ArduinoDbContext>(
    dbContextOptions => dbContextOptions
        .UseMySql(connectionString, serverVersion)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
        );

builder.Services.AddHangfire(hangfire => {
    hangfire.UseStorage(
        new MySqlStorage(connectionString,
            new MySqlStorageOptions
            {
                TablesPrefix = "Hangfire."
            }
        )
    );
});

builder.Services.AddHangfireServer(options => options.WorkerCount = 1);

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

app.UseCors("all");

// Hangfire
app.UseHangfireDashboard("/jobs");

app.Run();
