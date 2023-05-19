using System.Net;

namespace WLABS_Desafio.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Data { get; set; }
    }
}
