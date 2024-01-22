using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoDBTrainingExercise.Models
{
    public class UserAccount : BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int userId { get; set; }

        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
        public bool isAdmin { get; set; }
    }
}
