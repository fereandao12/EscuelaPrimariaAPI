using System.ComponentModel.DataAnnotations;

namespace EscuelaPrimariaAPI.Models
{
    public class Turno
    {
        [Key]
        public int idTurno { get; set; }
        public string Nombre { get; set; }
    }
}
