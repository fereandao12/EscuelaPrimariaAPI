using EscuelaPrimariaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EscuelaPrimariaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Apoderado> Apoderados { get; set; }

    }

}

