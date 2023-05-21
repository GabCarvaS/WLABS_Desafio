using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WLABS_Desafio.Models
{
    /**
     * Classe que representa o registro de log da API.
     */
    public class ApiLog
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = string.Empty;

        [BsonElement("MESSAGE")]
        [BsonRequired()]
        public string? message { get; set; }

        [BsonElement("DATE")]
        [BsonRequired()]
        public DateTime? date { get; set; }

    }
}
