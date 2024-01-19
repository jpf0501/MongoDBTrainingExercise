using MongoDBTrainingExercise.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MongoDBTrainingExercise.Services
{
    public class Courses : ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly Categories _categoryService;
        public Courses(IOptions<MongoDBSetting> mongoDBSettings, Categories categoryService)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _courseCollection = database.GetCollection<Course>("courses");

            _categoryService = categoryService;
        }

        public IEnumerable<CourseViewModel> Get()
        {
            var filter = Builders<Course>.Sort.Ascending(x => x.courseId);
            var matchStage = Builders<Course>.Filter.Eq(x => x.isActive, true);
            var result = _courseCollection.Aggregate().Match(matchStage).Sort(filter).ToList();
            IEnumerable<CourseViewModel> viewModel = result.Select(s => new CourseViewModel
            {
                Id = s.Id,
                courseId = s.courseId,
                name = s.name,
                categoryId = s.categoryId,
                categoryName = s.categoryId != 0 ? _categoryService.GetCategoryNameById(s.categoryId) : ""
            }
            );
            return viewModel;
        }

        public IEnumerable<CourseViewModel> GetAll()
        {
            var filter = Builders<Course>.Sort.Ascending(x => x.courseId);
            var result = _courseCollection.Aggregate().Sort(filter).ToList();
            IEnumerable<CourseViewModel> viewModel = result.Select(s => new CourseViewModel
            {
                Id = s.Id,
                courseId = s.courseId,
                name = s.name,
                categoryId = s.categoryId,
            }
            );
            return viewModel;
        }

        public CourseViewModel GetById(int id)
        {
            var viewModel = new CourseViewModel();
            var filter = Builders<Course>.Filter.Eq(x => x.courseId, id);
            var result = _courseCollection.Find(filter).FirstOrDefault();

            try
            {
                viewModel.Id = result.Id;
                viewModel.categoryId = result.categoryId;
                viewModel.name = result.name;
                viewModel.courseId = result.courseId;
                viewModel.isActive = result.isActive;
                viewModel.categoryName = result.categoryId != 0 ? _categoryService.GetCategoryNameById(result.categoryId) : "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return viewModel;
        }

        public bool Create(CourseViewModel viewModel)
        {
            try
            {
                var previousId = GetAll().OrderBy(x => x.courseId).LastOrDefault() != null ? GetAll().OrderBy(x => x.courseId).LastOrDefault().courseId : 0;

                var newDoc = new Course
                {
                    name = viewModel.name,
                    categoryId = viewModel.categoryId,
                    courseId = previousId + 1,
                    isActive = true,

                };

                _courseCollection.InsertOne(newDoc);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            };

        }
        [HttpPost]
        public bool Update(CourseViewModel viewModel)
        {
            try
            {
                var filter = Builders<Course>.Filter.Eq(x => x.courseId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<Course>.Update
                    .Set(x => x.name, viewModel.name)
                    .Set(x => x.categoryId, viewModel.categoryId);

                _courseCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }
        [HttpPost]
        public bool Delete(CourseViewModel viewModel)
        {
            try
            {
                var filter = Builders<Course>.Filter.Eq(x => x.courseId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<Course>.Update
                    .Set(x => x.isActive, false);

                _courseCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
