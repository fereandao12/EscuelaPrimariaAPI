using AutoMapper;
using EscuelaPrimariaAPI.Data;
using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimariaAPI.Services
{
    public class SeccionService : ISeccionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SeccionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Seccion>> ObtenerSecciones()
        {
            return await _context.Secciones.ToListAsync();
        }

        public async Task<Seccion> ObtenerSeccionPorId(int id)
        {
            var seccion = await _context.Secciones
                .Include(s => s.Turno)
                .Include(s => s.Profesor)
                .FirstOrDefaultAsync(s => s.idSeccion == id);
            if(seccion == null) return null;
            return seccion;
        }

        public async Task<Seccion> CrearSeccion(CrearSeccionDto seccionDto)
        {
            var seccion = _mapper.Map<Seccion>(seccionDto);
            _context.Secciones.Add(seccion);
            await _context.SaveChangesAsync();
            return seccion;
        }
        public async Task<Seccion> ActualizarSeccion(int id, CrearSeccionDto seccionDto)
        {
            var seccionExistente = await _context.Secciones.FindAsync(id);
            if (seccionExistente == null) return null;
            _mapper.Map(seccionDto, seccionExistente);
            await _context.SaveChangesAsync();
            return seccionExistente;
        }
        public async Task<bool> EliminarSeccion(int id)
        {
            var seccion = await _context.Secciones.FindAsync(id);
            if (seccion == null) return false;
            _context.Secciones.Remove(seccion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Seccion> AsignarProfesor(int idSeccion, int idProfesor)
        {
            var seccion = await _context.Secciones
                .Include(s => s.Profesor)
                .FirstOrDefaultAsync(s => s.idSeccion == idSeccion);

            var profesor = await _context.Profesores.FindAsync(idProfesor);
            
            if(seccion == null)
            {
                throw new Exception ("Seccion no econtrada");
            }
            if(profesor == null)
            {
                throw new Exception ("Profesor no econtrado");
            }

            seccion.Profesor = profesor;
            await _context.SaveChangesAsync();
            return seccion;

        }

        public async Task<Seccion> AsignarEstudiante(int idSeccion, int idEstudiante)
        {
            var seccion = await _context.Secciones
                .Include(s => s.Estudiantes)
                .FirstOrDefaultAsync(s => s.idSeccion == idSeccion);
            var estudiante = await _context.Estudiantes.FindAsync(idEstudiante);

            if (seccion == null)
            {
                throw new Exception("Seccion no econtrada");
            }
            if (estudiante == null)
            {
                throw new Exception("Estudiante no encontrado");
            }
            if (seccion.Estudiantes == null)
            {
                seccion.Estudiantes = new List<Estudiante>();
            }
            if(!seccion.Estudiantes.Any(e => e.idEstudiante == idEstudiante))
            {
                seccion.Estudiantes.Add(estudiante);
            }
            
            await _context.SaveChangesAsync();
            return seccion;
        }

        public async Task<Seccion> DefinirTurno(int idSeccion, string nombreTurno)
        {
            var seccion = await _context.Secciones
                .Include(s => s.Turno)
                .FirstOrDefaultAsync(s => s.idSeccion == idSeccion);

            var turno = await _context.Turnos
               .FirstOrDefaultAsync(s => s.Nombre == nombreTurno);

            if (string.IsNullOrWhiteSpace(nombreTurno))
                throw new ArgumentException("El nombre del turno no puede estar vacío.");

            if (seccion == null)
            {
                throw new Exception("Seccion no valida");
            }
            if(turno == null)
            {
                throw new Exception("Turno no existe");
            }

            seccion.Turno = turno;
            await _context.SaveChangesAsync();
            return seccion;
        }
    }
}
