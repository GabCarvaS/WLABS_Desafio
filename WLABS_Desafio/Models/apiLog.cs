using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DesafioWLABS.Models
{
    public class apiLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = string.Empty;

        [BsonElement("MESSAGE")]
        [BsonRequired()]
        public string? message { get; set; }

    }
}
