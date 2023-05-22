using System.Text.Json.Serialization;

namespace DesafioWLABS.Models
{
    public class EnderecoAwesomeApi
    {
        [JsonPropertyName("cep")]
        public string? Cep { get; set; }

        [JsonPropertyName("address_type")]
        public string? AddressType { get; set; }

        [JsonPropertyName("address_name")]
        public string? AddressName { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("district")]
        public string? District { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("lat")]
        public string? Lat { get; set; }

        [JsonPropertyName("lng")]
        public string? Lng { get; set; }

        [JsonPropertyName("ddd")]
        public string? Ddd { get; set; }

        [JsonPropertyName("city_ibge")]
        public string? CityIbge { get; set; }
    }
}
