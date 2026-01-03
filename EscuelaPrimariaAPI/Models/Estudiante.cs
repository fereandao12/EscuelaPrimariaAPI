using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EscuelaPrimariaAPI.Models
{
    public class Estudiante
    {
        [Key]
        public int idEstudiante { get; set; }
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public bool Repitente { get; set; }
        public bool Discapacidad { get; set; }
        public string? imgUrl { get; set; }

        public int Edad => DateTime.Now.Year - FechaNacimiento.Year;

        //Relaciones
        public int? idSeccion { get; set; }
        [ForeignKey("idSeccion")]
        [JsonIgnore]
        public Seccion? Seccion { get; set; }
        public List<Apoderado>? Apoderados { get; set; }
    }
}
