using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;
using EscuelaPrimariaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace EscuelaPrimariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;
        private readonly IWebHostEnvironment _env;

        public EstudianteController(IEstudianteService estudianteService, IWebHostEnvironment env)
        {
            _estudianteService = estudianteService;
            _env = env;
        }

        //ENDPOINTS
        //Obtener todos los estudiantes
        [HttpGet]
        public async Task<IActionResult> GetEstudiantes()
        {
            var estudiantes = await _estudianteService.ObtenerEstudiantes();
            return Ok(estudiantes);
        }
        //Obtener estudiante por DNI
        [HttpGet("{dni}")]
        public async Task<IActionResult> GetEstudiantePorDni(string dni)
        {
            var estudiante = await _estudianteService.ObtenerEstudiantePorDni(dni);
            if (estudiante == null) return NotFound();
            return Ok(estudiante);
        }
        //Crear estudiante
        [HttpPost]
        public async Task<IActionResult> CrearEstudiante([FromBody] CrearEstudianteDto crearEstudianteDto)
        {
            try
            {
                var estudiante = await _estudianteService.CrearEstudiante(crearEstudianteDto);
                return Ok(estudiante);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }

        }
        //Editar estudiante
        [HttpPut("{dni}")]
        public async Task<IActionResult> EditarEstudiante(string dni, [FromBody] CrearEstudianteDto crearEstudianteDto)
        {
            var estudianteExistente = await _estudianteService.ObtenerEstudiantePorDni(dni);
            if (estudianteExistente == null) return NotFound();
            // Actualizar los campos del estudiante existente
            var estudianteActualizado = await _estudianteService.ActualizarEstudiante(dni, crearEstudianteDto);
            return Ok(estudianteActualizado);
        }
        //Eliminar estudiante
        [HttpDelete("{dni}")]
        public async Task<IActionResult> EliminarEstudiante(string dni)
        {
            var estudianteExistente = await _estudianteService.ObtenerEstudiantePorDni(dni);
            if (estudianteExistente == null) return NotFound();
            await _estudianteService.EliminarEstudiante(dni);
            return NoContent();
        }
        //Ver ficha estudiante
        [HttpGet("ficha-pdf/{dni}")]
        public async Task<IActionResult> DescargarFichaPdf(string dni)
        {
            // 1. Asegurar Licencia (Por seguridad, lo ponemos aquí también)
            QuestPDF.Settings.License = LicenseType.Community;

            var est = await _estudianteService.verFichaEstudiante(dni);
            if (est == null) return NotFound("Estudiante no encontrado");

            byte[] imagenBytes;

            try
            {
                if (string.IsNullOrEmpty(est.imgUrl))
                {
                    imagenBytes = Placeholders.Image(100, 100);
                }
                else if (est.imgUrl.StartsWith("/")) // CASO 1: IMAGEN LOCAL (Lectura directa de disco)
                {
                    // Convertimos "/img/foto.jpg" en "C:\wwwroot\img\foto.jpg"
                    // Quitamos la primera barra '/' para que Combine funcione bien
                    var rutaWebRoot = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    var rutaArchivo = Path.Combine(rutaWebRoot, est.imgUrl.TrimStart('/'));

                    if (System.IO.File.Exists(rutaArchivo))
                    {
                        imagenBytes = await System.IO.File.ReadAllBytesAsync(rutaArchivo);
                    }
                    else
                    {
                        // Si el archivo no existe físicamente, ponemos placeholder
                        imagenBytes = Placeholders.Image(100, 100);
                    }
                }
                else // CASO 2: IMAGEN IMGBB (Descarga web normal)
                {
                    using (var client = new HttpClient())
                    {
                        // A veces ImgBB requiere un User-Agent
                        client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
                        imagenBytes = await client.GetByteArrayAsync(est.imgUrl);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error imagen: " + ex.Message);
                imagenBytes = Placeholders.Image(100, 100);
            }

            // 2. Generar PDF (Tu código QuestPDF sigue igual aquí)
            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header().Text("FICHA DE MATRÍCULA").Bold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(1, Unit.Centimetre).Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text($"Estudiante: {est.Nombres} {est.Apellidos}");
                            col.Item().Text($"DNI: {est.Dni}");
                        });

                        row.ConstantItem(120).Border(1).Height(120).Image(imagenBytes).FitArea();
                    });
                });
            });

            var pdfBytes = documento.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Ficha_{dni}.pdf");
        }
    }
}
