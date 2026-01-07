
namespace EscuelaPrimariaAPI.Services
{
    public class ImagenService : IImagenService
    {
        private readonly IWebHostEnvironment _env;

        public ImagenService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> GuardarImagen(string base64Img, string nombreCarpeta)
        {
            if (string.IsNullOrEmpty(base64Img) || !base64Img.Contains("base64"))
            {
                return base64Img;
            }

            var nombreArchivo = $"{Guid.NewGuid()}";

            var rutaCarpeta = Path.Combine(_env.WebRootPath, nombreCarpeta);

            if (!Directory.Exists(rutaCarpeta))
            {
                Directory.CreateDirectory(rutaCarpeta);
            }

            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);

            var datos = base64Img.Contains(",") ? base64Img.Split(',')[1] : base64Img;
            var bytes = Convert.FromBase64String(datos);

            await File.WriteAllBytesAsync(rutaCompleta, bytes);

            return $"/{nombreCarpeta}/{nombreArchivo}";
        }
    }
}
