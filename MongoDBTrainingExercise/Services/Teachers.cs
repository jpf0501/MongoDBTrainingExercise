using MongoDBTrainingExercise.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models.ViewModels;

namespace MongoDBTrainingExercise.Services
{
    public class Teachers : ITeacherService
    {
        private readonly IMongoCollection<Teacher> _teacherCollection;
        public Teachers(IOptions<MongoDBSetting> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _teacherCollection = database.GetCollection<Teacher>("teachers");
        }

        public IEnumerable<TeacherViewModel> Get() 
        {
            var filter = Builders<Teacher>.Sort.Ascending(x => x.teacherId);
            var result = _teacherCollection.Aggregate().Sort(filter).ToList();
            IEnumerable<TeacherViewModel> viewModel = result.Select(s => new TeacherViewModel
                {
                    Id = s.Id,
                    teacherId = s.teacherId,
                    firstName = s.firstName,
                    lastName = s.lastName,
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
