using EscuelaPrimariaAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscuelaPrimariaAPI.DTOs
{
    public class AsignarSeccionEstudianteDto
    {
        public int idEstudiante { get; set; }
        public Seccion? Seccion { get; set; }
    }
}
