using AutoMapper;
using EscuelaPrimariaAPI.Data;
using EscuelaPrimariaAPI.Mapping;
using EscuelaPrimariaAPI.Services;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure; 
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configurar Licencia de QuestPDF ---
QuestPDF.Settings.License = LicenseType.Community;

// Add services to the container.

// Configuracion de la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// --- 3. IMPORTANTE: Configurar CORS para Blazor ---
// Esto permite que tu Frontend (Blazor) descargue archivos de esta API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor", policy =>
    {
        policy.AllowAnyOrigin() // En producción, cambia esto por la URL de tu Blazor
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddHttpClient();

var app = builder.Build();

// Sacamos Swagger del "if" para que funcione en Azure (Producción)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowBlazor");

app.UseStaticFiles(); 

app.UseAuthorization();

app.MapControllers();

// Redirección a Swagger en la raíz
app.MapGet("/", context => {
    context.Response.Redirect("/swagger/index.html");
    return Task.CompletedTask;
});

app.Run();