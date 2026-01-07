namespace EscuelaPrimariaAPI.Services
{
    public interface IImagenService
    {
        Task<string> GuardarImagen(string base64Img, string nombreCarpeta);
    }
}
