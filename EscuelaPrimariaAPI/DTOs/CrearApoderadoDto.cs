using System.ComponentModel.DataAnnotations;

namespace EscuelaPrimariaAPI.DTOs
{
    public class CrearApoderadoDto
    {
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Debe tener 8 digitos")]
        public string Dni { get; set; }
        public string NumeroTelf { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Rol { get; set; }
        public int Edad => DateTime.Now.Year - FechaNacimiento.Year;
    }
}
