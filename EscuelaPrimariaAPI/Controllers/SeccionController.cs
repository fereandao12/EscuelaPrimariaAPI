using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscuelaPrimariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeccionController : ControllerBase
    {
        private readonly ISeccionService _seccionService;

        public SeccionController(ISeccionService seccionService)
        {
            _seccionService = seccionService;
        }

        //ENDPOINTS
        //Obter todas las secciones
        [HttpGet]
        public async Task<IActionResult> GetSecciones()
        {
            var secciones = await _seccionService.ObtenerSecciones();
            return Ok(secciones);
        }
        //Obtener por id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSeccionPorId(int id)
        {
            var seccion = await _seccionService.ObtenerSeccionPorId(id);
            if (seccion == null)
            {
                return NotFound(new { message = "Sección no encontrada." });
            }
            return Ok(seccion);
        }
        //Crear seccion
        [HttpPost]
        public async Task<IActionResult> CrearSeccion([FromBody] CrearSeccionDto seccionDto)
        {
            try
            {
                var seccion = await _seccionService.CrearSeccion(seccionDto);
                return Ok(seccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Actualizar seccion
        [HttpPut("{id:int}")]
        public async Task<IActionResult> ActualizarSeccion(int id, [FromBody] CrearSeccionDto seccionDto)
        {
            try
            {
                var seccion = await _seccionService.ActualizarSeccion(id, seccionDto);
                if (seccion == null)
                {
                    return NotFound(new { message = "Sección no encontrada." });
                }
                return Ok(seccion);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Eliminar seccion
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> EliminarSeccion(int id)
        {
            try
            {
                var resultado = await _seccionService.EliminarSeccion(id);
                if (!resultado)
                {
                    return NotFound(new { message = "Sección no encontrada." });
                }
                return Ok(new { message = "Sección eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //AsignarProfesor
        [HttpPost("{idSeccion:int}/asignar-profesor/{idProfesor:int}")]
        public async Task<IActionResult> AsignarProfesor(int idSeccion, int idProfesor)
        {
            try
            {
                var seccion = await _seccionService.AsignarProfesor(idSeccion, idProfesor);
                return Ok(seccion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        //AsignarEstudiantes
        [HttpPost("{idSeccion:int}/asignar-estudiante/{idEstudiante:int}")]
        public async Task<IActionResult> AsignarEstudiante(int idSeccion, int idEstudiante)
        {
            try
            {
                var seccion = await _seccionService.AsignarEstudiante(idSeccion, idEstudiante);
                return Ok(seccion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Definir Turno
        [HttpPost("{idSeccion:int}/definir-turno{nombreTurno}")]
        public async Task<IActionResult> DefinirTurno(int idSeccion, string nombreTurno)
        {
            try
            {
                var seccion = await _seccionService.DefinirTurno(idSeccion, nombreTurno);
                return Ok(seccion);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
