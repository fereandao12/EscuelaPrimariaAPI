using AutoMapper;
using EscuelaPrimariaAPI.Data;
using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimariaAPI.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProfesorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Profesor> ActualizarProfesor(string dni, CrearProfesorDto profesorDto)
        {
            var profesor = await _context.Profesores.FindAsync(dni);
            if(await _context.Profesores.AnyAsync(p => p.Dni == profesorDto.Dni && p.Dni != dni))
            {
                throw new Exception("Ya existe un profesor con el mismo DNI.");
            }
            if (profesor == null) return null;
            _mapper.Map(profesorDto, profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<Profesor> CrearProfesor(CrearProfesorDto profesorDto)
        {
            var profesor = _mapper.Map<Profesor>(profesorDto);
            if(await _context.Profesores.AnyAsync(p => p.Dni == profesor.Dni))
            {
                throw new Exception("Ya existe un profesor con el mismo DNI.");
            }
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<bool> EliminarProfesor(string dni)
        {
            var profesor = await _context.Profesores.FindAsync(dni);
            if (profesor == null) return false;
            _context.Profesores.Remove(profesor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Profesor>> ObtenerProfesores()
        {
            return await _context.Profesores.ToListAsync();
        }

        public async Task<Profesor> ObtenerProfesorPorDni(string dni)
        {
            var profesor = await _context.Profesores.FirstOrDefaultAsync(p => p.Dni == dni);
            if (profesor == null) return null;
            return profesor;
        }
    }
}
