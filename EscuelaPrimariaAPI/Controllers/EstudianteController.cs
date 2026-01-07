using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;
using EscuelaPrimariaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //Guardar imagen
        [HttpPost]
        public async Task<ActionResult<Estudiante>> PostEstudiante(CrearEstudianteDto estudianteDto)
        {
           
        }
    }
}
