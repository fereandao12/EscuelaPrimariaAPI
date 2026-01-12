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

        public EstudianteController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
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
        [HttpGet("ficha/{dni}")]
        public async Task<IActionResult> VerFichaEstudiante(string dni)
        {
            var fichaEstudiante = await _estudianteService.verFichaEstudiante(dni);
            if (fichaEstudiante == null) return NotFound();
            return Ok(fichaEstudiante);
        }

        [HttpGet("ficha-pdf/{dni}")]
        public async Task<IActionResult> DescargarFichaPdf(string dni)
        {
            var est = await _estudianteService.verFichaEstudiante(dni);

            if (est == null) return NotFound("Estudiante no encontrado");

            byte[] imagenBytes;

            try
            {
                if (string.IsNullOrEmpty(est.imgUrl))
                {
                    // Si no tiene URL, usamos un cuadro gris por defecto
                    imagenBytes = Placeholders.Image(100, 100);
                }
                else
                {
                    using (var client = new HttpClient())
                    {
                        string urlParaDescargar = est.imgUrl;

                        // Necesitamos pegarle el dominio actual de la API para poder descargarla
                        if (est.imgUrl.StartsWith("/"))
                        {
                            var dominioActual = $"{Request.Scheme}://{Request.Host}";
                            urlParaDescargar = $"{dominioActual}{est.imgUrl}";
                        }

                        // Descargamos los bytes de la imagen
                        imagenBytes = await client.GetByteArrayAsync(urlParaDescargar);
                    }
                }
            }
            catch
            {
                // Si falla la descarga (ej: imagen borrada de ImgBB o error de red),
                // ponemos el placeholder para que el PDF no falle por completo.
                imagenBytes = Placeholders.Image(100, 100);
            }

            //Generar el PDF con QuestPDF
            QuestPDF.Settings.License = LicenseType.Community;

            var documento = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // Encabezado
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("FICHA DE MATRÍCULA").Bold().FontSize(20).FontColor(Colors.Blue.Medium);
                            col.Item().Text("Sistema de Gestión Escolar").FontSize(10);
                        });
                    });

                    // Contenido Principal
                    page.Content().PaddingVertical(1, Unit.Centimetre).Row(row =>
                    {
                        // Columna de Datos (Izquierda)
                        row.RelativeItem().PaddingRight(15).Column(col =>
                        {
                            col.Item().Text("Datos del Estudiante").Underline().Bold();
                            col.Item().PaddingTop(5).Text($"DNI: {est.Dni}");
                            col.Item().Text($"Nombres: {est.Nombres} {est.Apellidos}");
                            col.Item().Text($"Edad: {est.Edad} años");
                            col.Item().Text($"Nacimiento: {est.FechaNacimiento:dd/MM/yyyy}");

                            col.Item().PaddingTop(10).Text($"Repitente: {(est.Repitente ? "SÍ" : "NO")}");
                            col.Item().Text($"Discapacidad: {(est.Discapacidad ? "SÍ" : "NO")}");
                        });

                        // Columna de Foto (Derecha)
                        // Aquí usamos la variable 'imagenBytes' que preparamos arriba
                        row.ConstantItem(120).Column(col =>
                        {
                            col.Item().Border(1).BorderColor(Colors.Grey.Lighten2)
                               .Height(120).Image(imagenBytes).FitArea();

                            col.Item().AlignCenter().Text("Foto Actual").FontSize(8).Italic();
                        });
                    });

                    // Pie de Página
                    page.Footer().AlignCenter().Text(x =>
                    {
                        x.Span($"Generado el {DateTime.Now:g}");
                    });
                });
            });

            // 4. Generar y devolver el archivo
            var pdfBytes = documento.GeneratePdf();
            return File(pdfBytes, "application/pdf", $"Ficha_{dni}.pdf");
        }
    }
}
