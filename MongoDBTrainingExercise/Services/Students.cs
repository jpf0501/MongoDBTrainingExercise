using MongoDBTrainingExercise.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models.ViewModels;

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

        public IEnumerable<StudentViewModel> Get() 
        {
            var filter = Builders<Student>.Sort.Ascending(x => x.studentId);
            var result = _studentCollection.Aggregate().Sort(filter).ToList();
            IEnumerable<StudentViewModel> viewModel = result.Select(s => new StudentViewModel
                {
                    Id = s.Id,
                    studentId = s.studentId,
                    firstName = s.firstName,
                    lastName = s.lastName,
                    age = s.age,
                    address = s.address,
                }
            );
            return viewModel;
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
