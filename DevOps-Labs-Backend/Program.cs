using DevOps_Labs_Backend.Context;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("MySQL"),
            new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
builder.Services.AddScoped<ApplicationDbContextInitialiser>();
builder.Services.AddHealthChecks();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed((_) => true)));

if (!builder.Environment.IsEnvironment("Local"))
{
    builder.WebHost.UseKestrel(configure =>
    {
        configure.Listen(System.Net.IPAddress.Any, 4000);
        configure.Listen(System.Net.IPAddress.Any, 80);
    });
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
    await initialiser.InitialiseAsync();
    await initialiser.SeedAsync();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health");

app.Run();
