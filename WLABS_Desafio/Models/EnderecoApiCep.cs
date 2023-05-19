using System.Text.Json.Serialization;

namespace DesafioWLABS.Models
{
    public class EnderecoApiCep
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("district")]
        public string? District { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }
    }
}
