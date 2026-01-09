using AutoMapper;
using EscuelaPrimariaAPI.Data;
using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimariaAPI.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImagenService _imagenService;

        public EstudianteService(AppDbContext context, IMapper mapper, IImagenService imagenService)
        {
            _context = context;
            _mapper = mapper;
            _imagenService = imagenService;
        }

        public async Task<Estudiante> CrearEstudiante(CrearEstudianteDto crearEstudianteDto)
        {
            if(await _context.Estudiantes.AnyAsync(e => e.Dni == crearEstudianteDto.Dni))
            {
                throw new Exception("Ya existe un estudiante con el mismo DNI.");
            }
            if(crearEstudianteDto.Edad > 14 || crearEstudianteDto.Edad < 6)
            {
                throw new Exception("La edad de un estudiante de primaria debe estar entre 6 y 14 años.");
            }


            if (!string.IsNullOrEmpty(crearEstudianteDto.imgUrl) && crearEstudianteDto.imgUrl.Length > 200)
            {
                crearEstudianteDto.imgUrl = await _imagenService.GuardarImagen(crearEstudianteDto.imgUrl, "img");
            }

            var estudiante = _mapper.Map<Estudiante>(crearEstudianteDto);

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return estudiante;
        }

        public async Task<Estudiante> ActualizarEstudiante(string dni, CrearEstudianteDto crearEstudianteDto)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Dni == dni);

            if(await _context.Estudiantes.AnyAsync(e => e.Dni == crearEstudianteDto.Dni && e.Dni != dni))
            {
                throw new Exception("Ya existe un estudiante con el mismo DNI.");
            }
            if (estudiante == null) return null;

            if (!string.IsNullOrEmpty(crearEstudianteDto.imgUrl) && crearEstudianteDto.imgUrl.Length > 200)
            {
                crearEstudianteDto.imgUrl = await _imagenService.GuardarImagen(crearEstudianteDto.imgUrl, "img");
            }

            _mapper.Map(crearEstudianteDto, estudiante);
            await _context.SaveChangesAsync();
            return estudiante;
        }

        public async Task<bool> EliminarEstudiante(string dni)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Dni == dni);
            if (estudiante == null) return false;
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<VerEstudianteDto> ObtenerEstudiantePorDni(string dni)
        {
            var estudiante = await _context.Estudiantes.FirstOrDefaultAsync(e => e.Dni == dni);
            if (estudiante == null) return null;
            var estudianteDto = _mapper.Map<VerEstudianteDto>(estudiante);
            return estudianteDto;

        }

        public async Task<List<VerEstudianteDto>> ObtenerEstudiantes()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            var estudiantesDto = _mapper.Map<List<VerEstudianteDto>>(estudiantes);
            return estudiantesDto;
        }

        public async Task<FichaEstudianteDto> verFichaEstudiante(string dni)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Apoderados)
                .FirstOrDefaultAsync(e => e.Dni == dni);

            if (estudiante == null) return null;

            var estudianteDto = _mapper.Map<FichaEstudianteDto>(estudiante);

            return estudianteDto;
        }
    }
}
