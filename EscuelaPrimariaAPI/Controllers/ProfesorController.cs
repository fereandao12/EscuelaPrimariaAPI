using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscuelaPrimariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesorController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        //ENDPOINTS
        //Obtener todos los profesores
        [HttpGet]
        public async Task<IActionResult> GetProfesores()
        {
            var profesores = await _profesorService.ObtenerProfesores();
            return Ok(profesores);
        }
        //Obtener por dni
        [HttpGet("{dni}")]
        public async Task<IActionResult> GetProfesorPorDni(string dni)
        {
            var profesor = await _profesorService.ObtenerProfesorPorDni(dni);
            if (profesor == null) return NotFound();
            return Ok(profesor);
        }
        //Crear profesor
        [HttpPost]
        public async Task<IActionResult> CrearProfesor([FromBody] CrearProfesorDto crearProfesorDto)
        {
            try
            {
                var profesor = await _profesorService.CrearProfesor(crearProfesorDto);
                return Ok(profesor);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Editar profesor
        [HttpPut("{dni}")]
        public async Task<IActionResult> EditarProfesor(string dni, [FromBody] CrearProfesorDto crearProfesorDto)
        {
            try
            {
                var profesorActualizado = await _profesorService.ActualizarProfesor(dni, crearProfesorDto);
                if (profesorActualizado == null) return NotFound();
                return Ok(profesorActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Eliminar profesor
        [HttpDelete("{dni}")]
        public async Task<IActionResult> EliminarProfesor(string dni)
        {
            var eliminado = await _profesorService.EliminarProfesor(dni);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
