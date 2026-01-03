using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;

namespace EscuelaPrimariaAPI.Services
{
    public interface ISeccionService
    {
        Task<Seccion> ObtenerSeccionPorId(int id);
        Task<List<Seccion>> ObtenerSecciones();
        Task<Seccion> CrearSeccion(CrearSeccionDto seccionDto);
        Task<Seccion> ActualizarSeccion(int id, CrearSeccionDto seccionDto);
        Task<bool> EliminarSeccion(int id);
        Task<Seccion> AsignarProfesor(int idSeccion, int idProfesor);
        Task<Seccion> AsignarEstudiante(int idSeccion, int idEstudiante);
        Task<Seccion> DefinirTurno(int idSeccion, string nombreTurno);
    }
}
