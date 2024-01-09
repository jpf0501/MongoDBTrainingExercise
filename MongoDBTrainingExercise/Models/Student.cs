using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace MongoDBTrainingExercise.Models
{
    public class Student : BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public int studentId { get; set; }

        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public int age { get; set; }
        public string address { get; set; } = null!;
    }
}
