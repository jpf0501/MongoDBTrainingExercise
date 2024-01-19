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
    public class Categories : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        public Categories(IOptions<MongoDBSetting> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _categoryCollection = database.GetCollection<Category>("categories");
        }

        public IEnumerable<CategoryViewModel> Get() 
        {
            var filter = Builders<Category>.Sort.Ascending(x => x.categoryId);
            var matchStage = Builders<Category>.Filter.Eq(x => x.isActive, true);
            var result = _categoryCollection.Aggregate().Match(matchStage).Sort(filter).ToList();
            IEnumerable<CategoryViewModel> viewModel = result.Select(s => new CategoryViewModel
                {
                    Id = s.Id,
                    categoryId = s.categoryId,
                    categoryName = s.categoryName,
                }
            );
            return viewModel;
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var filter = Builders<Category>.Sort.Ascending(x => x.categoryId);
            var result = _categoryCollection.Aggregate().Sort(filter).ToList();
            IEnumerable<CategoryViewModel> viewModel = result.Select(s => new CategoryViewModel
            {
                Id = s.Id,
                categoryId = s.categoryId,
                categoryName = s.categoryName
            }
            );
            return viewModel;
        }

        public string GetCategoryNameById(int id)
        {
            var filter = Builders<Category>.Filter.Eq(x => x.categoryId, id);
            var result = _categoryCollection.Find(filter).FirstOrDefault().categoryName;

            return result;
        }

        //public StudentViewModel GetById(int id)
        //{
        //    var viewModel = new StudentViewModel();
        //    var filter = Builders<Student>.Filter.Eq(x => x.studentId, id);
        //    var result = _studentCollection.Find(filter).FirstOrDefault();

        //    try
        //    {
        //        viewModel.Id = result.Id;
        //        viewModel.firstName = result.firstName;
        //        viewModel.lastName = result.lastName;
        //        viewModel.age = result.age;
        //        viewModel.address = result.address;
        //        viewModel.studentId = result.studentId;
        //        viewModel.isActive = result.isActive;
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //    return viewModel;
        //}

        //public bool Create(StudentViewModel viewModel)
        //{
        //    try
        //    {
        //        var previousId = GetAll().OrderBy(x => x.studentId).LastOrDefault() != null ? GetAll().OrderBy(x => x.studentId).LastOrDefault().studentId : 0;

        //        var newDoc = new Student
        //        {
        //            firstName = viewModel.firstName,
        //            lastName = viewModel.lastName,
        //            age = viewModel.age,
        //            address = viewModel.address,
        //            studentId = previousId + 1,
        //            isActive = true,

        //        };

        //        _studentCollection.InsertOne(newDoc);

        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    };

        //}

        //[HttpPost]
        //public bool Update(StudentViewModel viewModel) 
        //{
        //    try
        //    {
        //        var filter = Builders<Student>.Filter.Eq(x => x.studentId, Convert.ToInt32(viewModel.Id));
        //        var updateSet = Builders<Student>.Update
        //            .Set(x => x.firstName, viewModel.firstName)
        //            .Set(x => x.lastName, viewModel.lastName)
        //            .Set(x => x.age, viewModel.age)
        //            .Set(x => x.address, viewModel.address);

        //        _studentCollection.UpdateOne(filter, updateSet);

        //        return true;
        //    }
        //    catch(Exception e)
        //    {
        //        return false;
        //    }


        //}
        //[HttpPost]
        //public bool Delete(StudentViewModel viewModel)
        //{
        //    try
        //    {
        //        var filter = Builders<Student>.Filter.Eq(x => x.studentId, Convert.ToInt32(viewModel.Id));
        //        var updateSet = Builders<Student>.Update
        //            .Set(x => x.isActive, false);

        //        _studentCollection.UpdateOne(filter, updateSet);

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}
    }
}
