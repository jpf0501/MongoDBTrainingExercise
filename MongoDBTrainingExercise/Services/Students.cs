using MongoDBTrainingExercise.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using Microsoft.AspNetCore.Mvc;

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
            var matchStage = Builders<Student>.Filter.Eq(x => x.isActive, true);
            var result = _studentCollection.Aggregate().Match(matchStage).Sort(filter).ToList();
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
        public StudentViewModel GetById(int id)
        {
            var viewModel = new StudentViewModel();
            var filter = Builders<Student>.Filter.Eq(x => x.studentId, id);
            var result = _studentCollection.Find(filter).FirstOrDefault();

            try
            {
                viewModel.Id = result.Id;
                viewModel.firstName = result.firstName;
                viewModel.lastName = result.lastName;
                viewModel.age = result.age;
                viewModel.address = result.address;
                viewModel.studentId = result.studentId;
                viewModel.isActive = result.isActive;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return viewModel;
        }

        public bool Create(StudentViewModel viewModel)
        {
            try
            {
                var previousId = Get().OrderBy(x => x.studentId).LastOrDefault() != null ? Get().OrderBy(x => x.studentId).LastOrDefault().studentId : 0;

                var newDoc = new Student
                {
                    firstName = viewModel.firstName,
                    lastName = viewModel.lastName,
                    age = viewModel.age,
                    address = viewModel.address,
                    studentId = previousId + 1,
                    isActive = true,

                };

                _studentCollection.InsertOne(newDoc);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            };
            
        }

        [HttpPost]
        public bool Update(StudentViewModel viewModel) 
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq(x => x.studentId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<Student>.Update
                    .Set(x => x.firstName, viewModel.firstName)
                    .Set(x => x.lastName, viewModel.lastName)
                    .Set(x => x.age, viewModel.age)
                    .Set(x => x.address, viewModel.address);

                _studentCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }


        }
        [HttpPost]
        public bool Delete(StudentViewModel viewModel)
        {
            try
            {
                var filter = Builders<Student>.Filter.Eq(x => x.studentId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<Student>.Update
                    .Set(x => x.isActive, false);

                _studentCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
