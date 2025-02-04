using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BookStore.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        // JSONPropName -> represents the property name in the web API's serialized JSON response.
        [JsonPropertyName("Name")]
        public string BookName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public string Author { get; set; } = null!;
        [BsonRepresentation(BsonType.Boolean)]
        public bool IsBorrowable { get; set; } = true;
    }
}
