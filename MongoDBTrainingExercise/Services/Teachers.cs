using MongoDBTrainingExercise.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var matchStage = Builders<Teacher>.Filter.Eq(x => x.isActive, true);
            var result = _teacherCollection.Aggregate().Match(matchStage).Sort(filter).ToList();
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

        public IEnumerable<TeacherViewModel> GetAll()
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

        public IEnumerable<TeacherViewModel> GetAllInactive()
        {
            var filter = Builders<Teacher>.Sort.Ascending(x => x.teacherId);
            var matchStage = Builders<Teacher>.Filter.Eq(x => x.isActive, false);
            var result = _teacherCollection.Aggregate().Match(matchStage).Sort(filter).ToList();
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



        public TeacherViewModel GetById(int id)
        {
            var viewModel = new TeacherViewModel();
            var filter = Builders<Teacher>.Filter.Eq(x => x.teacherId, id);
            var result = _teacherCollection.Find(filter).FirstOrDefault();

            try
            {
                viewModel.Id = result.Id;
                viewModel.firstName = result.firstName;
                viewModel.lastName = result.lastName;
                viewModel.isActive = result.isActive;
                viewModel.teacherId = result.teacherId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return viewModel;
        }

        public bool Create(TeacherViewModel viewModel)
        {
            try
            {
                var previousId = GetAll().OrderBy(x => x.teacherId).LastOrDefault() != null ? GetAll().OrderBy(x => x.teacherId).LastOrDefault().teacherId : 0;

                var newDoc = new Teacher
                {
                    firstName = viewModel.firstName,
                    lastName = viewModel.lastName,
                    teacherId = previousId + 1,
                    isActive = true,

                };

                _teacherCollection.InsertOne(newDoc);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            };

        }
        [HttpPost]
        public bool Update(TeacherViewModel viewModel)
        {
            try
            {
                var filter = Builders<Teacher>.Filter.Eq(x => x.teacherId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<Teacher>.Update
                    .Set(x => x.firstName, viewModel.firstName)
                    .Set(x => x.lastName, viewModel.lastName);

                _teacherCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }
        [HttpPost]
        public bool Delete(TeacherViewModel viewModel)
        {
            try
            {
                var filter = Builders<Teacher>.Filter.Eq(x => x.teacherId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<Teacher>.Update
                    .Set(x => x.isActive, false);

                _teacherCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPost]
        public bool Restore(TeacherViewModel viewModel)
        {
            try
            {
                var filter = Builders<Teacher>.Filter.Eq(x => x.teacherId, viewModel.teacherId);
                var updateSet = Builders<Teacher>.Update
                    .Set(x => x.isActive, true);

                _teacherCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
