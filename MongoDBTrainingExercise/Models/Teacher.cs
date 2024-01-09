using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoDBTrainingExercise.Models
{
    public class Teacher
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int teacherId { get; set; }

        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
    }
}
