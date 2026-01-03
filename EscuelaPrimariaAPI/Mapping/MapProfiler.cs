using AutoMapper;
using Azure;
using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;

namespace EscuelaPrimariaAPI.Mapping
{
    public class MapProfiler : Profile
    {
        public MapProfiler()
        {
            CreateMap<CrearEstudianteDto, Estudiante>();
            CreateMap<CrearProfesorDto, Profesor>();
            CreateMap<CrearSeccionDto, Seccion>();

            CreateMap<CrearApoderadoDto, Apoderado>();

            CreateMap<AsignarSeccionEstudianteDto, Estudiante>();
            CreateMap<AsignarSeccionProfesorDto, Profesor>();

            //Vistas
            CreateMap<Estudiante, VerEstudianteDto>();
            CreateMap<Estudiante, FichaEstudianteDto>();
        }
    }
}
