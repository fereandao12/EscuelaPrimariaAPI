using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;

namespace EscuelaPrimariaAPI.Services
{
    public interface IProfesorService
    {
        Task<List<Profesor>> ObtenerProfesores();
        Task<Profesor> ObtenerProfesorPorDni(string dni);
        Task<Profesor> ActualizarProfesor(string dni, CrearProfesorDto profesorDto);
        Task<Profesor> CrearProfesor(CrearProfesorDto profesorDto);
        Task<bool> EliminarProfesor(string dni);
    }
}
