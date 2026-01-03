using AutoMapper;
using EscuelaPrimariaAPI.Data;
using EscuelaPrimariaAPI.Mapping;
using EscuelaPrimariaAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Configuracion de la cadena de conexion a la base de datos SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//Automapper manual config
var mapingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapProfiler());
});

IMapper mapper = mapingConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

//Services
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IApoderadoService, ApoderadoService>();
builder.Services.AddScoped<ISeccionService, SeccionService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esta línea corta el bucle infinito automáticamente
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

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
