var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    Console.WriteLine(System.Environment.GetEnvironmentVariable("PORT"));

    options.ListenAnyIP(int.Parse(System.Environment.GetEnvironmentVariable("PORT") ?? "8080"));
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
