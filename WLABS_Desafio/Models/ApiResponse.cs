using System.Net;
using System.Text.Json;

namespace WLABS_Desafio.Models
{
    public class ApiResponse
    {
        public HttpStatusCode? StatusCode { get; set; }

        public string? Data { get; set; }
        /*
        public string ToJson()
        {
            // Converter a string JSON em um objeto anônimo
            var dataObj = JsonSerializer.Deserialize<object>(Data);

            // Serializar o objeto em uma string JSON sem caracteres de escape
            string json = JsonSerializer.Serialize(dataObj, new JsonSerializerOptions { WriteIndented = true });

            return json;
        }*/

    }


}
