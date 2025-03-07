using Microsoft.EntityFrameworkCore;
using WebApi.Application.Services;
using WebApi.Domain.Repositories;
using WebApi.Domain.Services;
using WebApi.Infrastructure.Context;
using WebApi.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    Console.WriteLine(System.Environment.GetEnvironmentVariable("PORT"));

    options.ListenAnyIP(int.Parse(System.Environment.GetEnvironmentVariable("PORT") ?? "8080"));
});

builder.Services.AddDbContext<DatabaseContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();

app.UseCors();

app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
