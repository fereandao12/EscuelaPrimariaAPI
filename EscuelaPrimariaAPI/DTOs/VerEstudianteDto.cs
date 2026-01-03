namespace EscuelaPrimariaAPI.DTOs
{
    public class VerEstudianteDto
    {
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        public bool Repitente { get; set; }
        public bool Discapacidad { get; set; }
        public string? imgUrl { get; set; }

        public int Edad => DateTime.Now.Year - FechaNacimiento.Year;
    }
}
