
using EscuelaPrimariaAPI.Models;
using System.Text.Json;

namespace EscuelaPrimariaAPI.Services
{
    public class ImagenService : IImagenService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "211b8cab2ae726f894adcf17c07df1e0";

        public ImagenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> GuardarImagen(string base64Img, string nombreCarpeta)
        {
            if (string.IsNullOrEmpty(base64Img) || base64Img.Length < 200)
            {
                return base64Img;
            }

            try
            {
                // ImgBB necesita el string limpio, tal como muestra el ejemplo de tu imagen.
                var base64Limpio = base64Img.Contains(",") ? base64Img.Split(',')[1] : base64Img;

                // 3. Preparar los datos para enviar (Formulario)
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ApiKey), "key");
                content.Add(new StringContent(base64Limpio), "image");
                // content.Add(new StringContent("600"), "expiration"); 

                // 4. Enviar la petición POST a ImgBB
                var response = await _httpClient.PostAsync("https://api.imgbb.com/1/upload", content);

                if (response.IsSuccessStatusCode)
                {
                    // 5. Leer la respuesta
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var imgbbResponse = JsonSerializer.Deserialize<ImgbbResponse>(jsonString);

                    // 6. Retornar solo la URL pública
                    return imgbbResponse.data.url;
                }
                else
                {
                    // Si falla ImgBB, puedes lanzar error o devolver null
                    Console.WriteLine("Error al subir a ImgBB: " + response.ReasonPhrase);
                    return "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción en ImagenService: " + ex.Message);
                return "";
            }
        }
    }
}
