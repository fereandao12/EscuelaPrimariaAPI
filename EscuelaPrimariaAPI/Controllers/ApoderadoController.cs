using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EscuelaPrimariaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApoderadoController : ControllerBase
    {
        private readonly IApoderadoService _apoderadoService;

        public ApoderadoController(IApoderadoService apoderadoService)
        {
            _apoderadoService = apoderadoService;
        }

        //ENDPOINTS
        //Obtener todos los apoderados
        [HttpGet]
        public async Task<IActionResult> GetApoderados()
        {
            var apoderados = await _apoderadoService.ObtenerApoderados();
            return Ok(apoderados);
        }
        //Obtener por dni
        [HttpGet("{dni}")]
        public async Task<IActionResult> GetApoderadoPorDni(string dni)
        {
            try
            {
                var apoderado = await _apoderadoService.ObtenerApoderadoPorDni(dni);
                return Ok(apoderado);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        //Crear apoderado
        [HttpPost]
        public async Task<IActionResult> CrearApoderado([FromBody] CrearApoderadoDto apoderadoDto)
        {
            try
            {
                var apoderado = await _apoderadoService.CrearAporedado(apoderadoDto);
                return Ok(apoderado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Actualizar apoderado
        [HttpPut("{dni}")]
        public async Task<IActionResult> ActualizarApoderado(string dni, [FromBody] CrearApoderadoDto apoderadoDto)
        {
            try
            {
                var apoderado = await _apoderadoService.ActualizarApoderado(dni, apoderadoDto);
                return Ok(apoderado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //Eliminar apoderado
        [HttpDelete("{dni}")]
        public async Task<IActionResult> EliminarApoderado(string dni)
        {
            var resultado = await _apoderadoService.EliminarApoderado(dni);
            if (!resultado)
            {
                return NotFound(new { message = "Apoderado no encontrado." });
            }
            return NoContent();
        }
        //Asignar menor
        [HttpPost("{dniApoderado}/asignar-menor/{dniMenor}")]
        public async Task<IActionResult> AsignarMenor(string dniApoderado, string dniMenor)
        {
            try
            {
                var apoderado = await _apoderadoService.AsignarMenor(dniApoderado, dniMenor);
                return Ok(apoderado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
