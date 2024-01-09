using MongoDBTrainingExercise.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models.ViewModels;

namespace MongoDBTrainingExercise.Services
{
    public class Courses : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        public Courses(IOptions<MongoDBSetting> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _courseCollection = database.GetCollection<Course>("courses");
        }

        public IEnumerable<CourseViewModel> Get()
        {
            var filter = Builders<Course>.Sort.Ascending(x => x.courseId);
            var result = _courseCollection.Aggregate().Sort(filter).ToList();
            IEnumerable<CourseViewModel> viewModel = result.Select(s => new CourseViewModel
            {
                Id = s.Id,
                courseId = s.courseId,
                name = s.name,
                category = s.category,
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
