using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscuelaPrimariaAPI.Models
{
    public class Profesor
    {
        [Key]
        public int idProfesor { get; set; }
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public int Edad => DateTime.Now.Year - FechaNacimiento.Year;

    }
}
