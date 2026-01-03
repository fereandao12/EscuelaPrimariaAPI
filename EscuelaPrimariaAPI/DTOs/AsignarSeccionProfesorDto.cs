using EscuelaPrimariaAPI.Models;

namespace EscuelaPrimariaAPI.DTOs
{
    public class AsignarSeccionProfesorDto
    {
        public int idProfesor { get; set; }
        public Seccion? Seccion { get; set; }
    }
}
