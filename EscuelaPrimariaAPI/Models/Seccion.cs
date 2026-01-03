using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscuelaPrimariaAPI.Models
{
    public class Seccion
    {
        [Key]
        public int idSeccion { get; set; }
        public string Nombre { get; set; }
        public int Grado { get; set; }

        //Relaciones
        public List<Estudiante>? Estudiantes { get; set; }
        [ForeignKey("idProfesor")]
        public Profesor? Profesor { get; set; }
        [ForeignKey("idTurno")]
        public Turno? Turno { get; set; }   
    }
}
