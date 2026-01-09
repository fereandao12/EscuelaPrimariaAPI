using AutoMapper;
using EscuelaPrimariaAPI.Data;
using EscuelaPrimariaAPI.Mapping;
using EscuelaPrimariaAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configuracion de la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// IMPORTANTE: Si usas SmarterASP, a veces necesitas TrustServerCertificate=True
// Asegúrate de que tu Connection String en Azure tenga eso.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Automapper manual config
var mapingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapProfiler());
});

IMapper mapper = mapingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Services
builder.Services.AddScoped<IEstudianteService, EstudianteService>();
builder.Services.AddScoped<IProfesorService, ProfesorService>();
builder.Services.AddScoped<IApoderadoService, ApoderadoService>();
builder.Services.AddScoped<ISeccionService, SeccionService>();
builder.Services.AddScoped<IImagenService, ImagenService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Solo una llamada a AddControllers con las opciones JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddHttpClient();

var app = builder.Build();

// --- CORRECCIÓN AQUÍ ---
// Sacamos Swagger del "if" para que funcione en Azure (Producción)
app.UseSwagger();
app.UseSwaggerUI();
// -----------------------

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

// Opcional: Esto hace que al abrir la raiz (/) te lleve directo al Swagger
app.MapGet("/", context => {
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.Run();