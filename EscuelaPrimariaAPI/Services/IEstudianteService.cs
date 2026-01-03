using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;

namespace EscuelaPrimariaAPI.Services
{
    public interface IEstudianteService
    {
        Task<List<VerEstudianteDto>> ObtenerEstudiantes();
        Task<VerEstudianteDto> ObtenerEstudiantePorDni(string dni);
        Task<Estudiante> ActualizarEstudiante(string dni, CrearEstudianteDto crearEstudianteDto);
        Task<Estudiante> CrearEstudiante(CrearEstudianteDto crearEstudianteDto);
        Task<bool> EliminarEstudiante(string dni);
        Task<FichaEstudianteDto> verFichaEstudiante(string dni);
    }
}
