using Microsoft.EntityFrameworkCore;
using FoodDelivery.Persistencia.Repositorios;
using FoodDelivery.Aplicacion.Servicios;
using FoodDelivery.Domain.Servicios;
using FoodDelivery.Aplicacion;
using FoodDelivery.Domain.Modelos;
using AutoMapper;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FoodDelivery.Persistencia.Repositorios.FoodDeliveryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


// Registrar repositorio y servicio Cliente
builder.Services.AddScoped<IClienteRepository, FoodDelivery.Persistencia.Repositorios.Implementaciones.ClienteRepository>();
builder.Services.AddScoped<IClienteService, ClienteService>();

// Registrar repositorio y servicio Empresa
builder.Services.AddScoped<IEmpresaRepository, FoodDelivery.Persistencia.Repositorios.Implementaciones.EmpresaRepository>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();

// Registrar repositorio y servicio Adicional
builder.Services.AddScoped<IAdicionalRepository, FoodDelivery.Persistencia.Repositorios.Implementaciones.AdicionalRepository>();
builder.Services.AddScoped<IAdicionalService, AdicionalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
