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

        public IEnumerable<CategoryViewModel> GetAllInactive()
        {
            var filter = Builders<Category>.Sort.Ascending(x => x.categoryId);
            var matchStage = Builders<Category>.Filter.Eq(x => x.isActive, false);
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

        public CategoryViewModel GetById(int id)
        {
            var viewModel = new CategoryViewModel();
            var filter = Builders<Category>.Filter.Eq(x => x.categoryId, id);
            var result = _categoryCollection.Find(filter).FirstOrDefault();

            try
            {
                viewModel.Id = result.Id;
                viewModel.categoryId = result.categoryId;
                viewModel.categoryName = result.categoryName;
                viewModel.isActive = result.isActive;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return viewModel;
        }

        public bool Create(CategoryViewModel viewModel)
        {
            try
            {
                var previousId = GetAll().OrderBy(x => x.categoryId).LastOrDefault() != null ? GetAll().OrderBy(x => x.categoryId).LastOrDefault().categoryId : 0;

                var newDoc = new Category
                {
                    categoryName = viewModel.categoryName,
                    categoryId = previousId + 1,
                    isActive = true,

                };

                _categoryCollection.InsertOne(newDoc);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            };

        }

        [HttpPost]
        public bool Update(CategoryViewModel viewModel)
        {
            try
            {
                var filter = Builders<Category>.Filter.Eq(x => x.categoryId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<Category>.Update
                    .Set(x => x.categoryName, viewModel.categoryName);

                _categoryCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }
        [HttpPost]
        public bool Delete(CategoryViewModel viewModel)
        {
            try
            {
                var filter = Builders<Category>.Filter.Eq(x => x.categoryId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<Category>.Update
                    .Set(x => x.isActive, false);

                _categoryCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPost]
        public bool Restore(CategoryViewModel viewModel)
        {
            try
            {
                var filter = Builders<Category>.Filter.Eq(x => x.categoryId, viewModel.categoryId);
                var updateSet = Builders<Category>.Update
                    .Set(x => x.isActive, true);

                _categoryCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
