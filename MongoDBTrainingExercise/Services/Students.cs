using MongoDBTrainingExercise.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDBTrainingExercise.Interface;

namespace MongoDBTrainingExercise.Services
{
    public class Students : IStudentService
    {
        private readonly IMongoCollection<Student> _studentCollection;
        public Students(IOptions<MongoDBSetting> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _studentCollection = database.GetCollection<Student>("students");
        }

        public async Task<List<Student>> Get() 
        {
            return await _studentCollection.Find(new BsonDocument()).ToListAsync();
        }
        public async Task Create(Student student)
        { 
        }
        public async Task Update(Student student) 
        { 
        }
        public async Task Delete(string id)
        {
        }
    }
}
