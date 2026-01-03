using System.ComponentModel.DataAnnotations;

namespace EscuelaPrimariaAPI.Models
{
    public class Apoderado
    {
        [Key]
        public int idApoderado { get; set; }
        public string Dni { get; set; }
        public string NumeroTelf { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public string Rol { get; set; }
        public int Edad => DateTime.Now.Year - FechaNacimiento.Year;

        //Relaciones
        public List<Estudiante>? menoresACargo { get; set; }

    }
}
