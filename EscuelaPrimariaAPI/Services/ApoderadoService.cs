using AutoMapper;
using EscuelaPrimariaAPI.Data;
using EscuelaPrimariaAPI.DTOs;
using EscuelaPrimariaAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimariaAPI.Services
{
    public class ApoderadoService : IApoderadoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ApoderadoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Apoderado> ActualizarApoderado(string dni, CrearApoderadoDto apoderadoDto)
        {
            var apoderado = await _context.Apoderados.FirstOrDefaultAsync(a => a.Dni == dni);
            if (await _context.Apoderados.AnyAsync(a => a.Dni == apoderadoDto.Dni))
            {
                throw new Exception("Ya existe un apoderado con el mismo DNI.");
            }
            if (apoderado == null)
            {
                throw new Exception("Apoderado no valido.");
            }
            _mapper.Map(apoderadoDto, apoderado);
            await _context.SaveChangesAsync();
            return apoderado;
        }

        public async Task<Apoderado> CrearAporedado(CrearApoderadoDto apoderadoDto)
        {
            var apoderado = _mapper.Map<Apoderado>(apoderadoDto);
            if (_context.Apoderados.Any(a => a.Dni == apoderado.Dni))
            {
                throw new Exception("Ya existe un apoderado con el mismo DNI.");
            }
            _context.Apoderados.Add(apoderado);
            await _context.SaveChangesAsync();
            return apoderado;
        }

        public async Task<bool> EliminarApoderado(string dni)
        {
            var apoderado = await _context.Apoderados.FirstOrDefaultAsync(a => a.Dni == dni);
            if (apoderado == null) return false;
            _context.Apoderados.Remove(apoderado);
            await _context.SaveChangesAsync();
            return true;    
        }

        public async Task<Apoderado> ObtenerApoderadoPorDni(string dni)
        {
            var apoderado = await _context.Apoderados
                .Include(a => a.menoresACargo)
                .FirstOrDefaultAsync(a => a.Dni == dni);
            if (apoderado == null)
            {
                throw new Exception("Apoderado no encontrado.");
            }
            return apoderado;
        }

        public async Task<List<Apoderado>> ObtenerApoderados()
        {
            return await _context.Apoderados
                .Include(a => a.menoresACargo)
                .ToListAsync();
        }
        public async Task<Apoderado> AsignarMenor(string dniApoderado, string dniMenor)
        {
            var apoderado = await _context.Apoderados
                .Include(a => a.menoresACargo)
                .FirstOrDefaultAsync(a => a.Dni == dniApoderado);

            if (apoderado == null)
            {
                throw new Exception("Apoderado no encontrado.");

            }

            var menor = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Dni == dniMenor);

            if (menor == null)
            {
                throw new Exception("Menor no encontrado.");
            }

            if (apoderado.menoresACargo == null)
            {
                apoderado.menoresACargo = new List<Estudiante>();
            }

            if(!apoderado.menoresACargo.Any(e => e.Dni == dniMenor))
            {
                apoderado.menoresACargo.Add(menor);
            }

            await _context.SaveChangesAsync();
            return apoderado;
        }
    }
}
