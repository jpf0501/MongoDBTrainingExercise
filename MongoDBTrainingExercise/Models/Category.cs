using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBTrainingExercise.Models
{
    public class Category : BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; } = null!;
    }
}
