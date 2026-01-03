using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;

namespace EscuelaPrimariaAPI.Services
{
    public interface IApoderadoService
    {
        Task<List<Apoderado>> ObtenerApoderados();
        Task<Apoderado> ObtenerApoderadoPorDni(string dni);
        Task<Apoderado> CrearAporedado(CrearApoderadoDto apoderadoDto);
        Task<Apoderado> ActualizarApoderado(string dni, CrearApoderadoDto apoderadoDto);
        Task<bool> EliminarApoderado(string dni);
        Task<Apoderado> AsignarMenor(string dniApoderado, string dniMenor);
    }
}
