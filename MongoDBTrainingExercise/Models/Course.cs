﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDBTrainingExercise.Models
{
    public class Course : BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public int courseId { get; set; }
        public string name { get; set; } = null!;
        public int categoryId { get; set; }
    }
}
