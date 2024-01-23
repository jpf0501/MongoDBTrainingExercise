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
    public class UserAccounts : IUserAccountService
    {
        private readonly IMongoCollection<UserAccount> _accountCollection;
        public UserAccounts(IOptions<MongoDBSetting> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _accountCollection = database.GetCollection<UserAccount>("accounts");
        }

        public IEnumerable<UserAccountViewModel> Get() 
        {
            var filter = Builders<UserAccount>.Sort.Ascending(x => x.userId);
            var matchStage = Builders<UserAccount>.Filter.Eq(x => x.isActive, true);
            var result = _accountCollection.Aggregate().Match(matchStage).Sort(filter).ToList();
            IEnumerable<UserAccountViewModel> viewModel = result.Select(s => new UserAccountViewModel
            {
                    Id = s.Id,
                    userId = s.userId,
                    username = s.username,
                    isAdmin = s.isAdmin,
                }
            );
            return viewModel;
        }

        public IEnumerable<UserAccountViewModel> GetAll()
        {
            var filter = Builders<UserAccount>.Sort.Ascending(x => x.userId);
            var result = _accountCollection.Aggregate().Sort(filter).ToList();
            IEnumerable<UserAccountViewModel> viewModel = result.Select(s => new UserAccountViewModel
            {
                Id = s.Id,
                userId = s.userId,
                username = s.username,
                isAdmin = s.isAdmin,
            }
            );
            return viewModel;
        }

        public IEnumerable<UserAccountViewModel> GetAllInactive()
        {
            var filter = Builders<UserAccount>.Sort.Ascending(x => x.userId);
            var matchStage = Builders<UserAccount>.Filter.Eq(x => x.isActive, false);
            var result = _accountCollection.Aggregate().Match(matchStage).Sort(filter).ToList();
            IEnumerable<UserAccountViewModel> viewModel = result.Select(s => new UserAccountViewModel
            {
                Id = s.Id,
                userId = s.userId,
                username = s.username,
                isAdmin = s.isAdmin,
            }
            );
            return viewModel;
        }

        public UserAccountViewModel GetById(int id)
        {
            var viewModel = new UserAccountViewModel();
            var filter = Builders<UserAccount>.Filter.Eq(x => x.userId, id);
            var result = _accountCollection.Find(filter).FirstOrDefault();

            try
            {
                viewModel.Id = result.Id;
                viewModel.userId = result.userId;
                viewModel.username = result.username;
                viewModel.isAdmin = result.isAdmin;
                viewModel.oldPassword = result.password;
                viewModel.isActive = result.isActive;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return viewModel;
        }

        public UserAccountViewModel GetByUsername(string username, string password)
        {
            var viewModel = new UserAccountViewModel();
            var filter = Builders<UserAccount>.Filter.Eq(x => x.username, username) & Builders<UserAccount>.Filter.Eq(x => x.isActive, true);
            var result = _accountCollection.Find(filter).FirstOrDefault();

            try
            {
                viewModel.Id = result.Id;
                viewModel.userId = result.userId;
                viewModel.username = result.username;
                viewModel.isAdmin = result.isAdmin;
                viewModel.password = result.password;
                viewModel.isActive = result.isActive;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return viewModel;
        }

        public bool Create(UserAccountViewModel viewModel)
        {
            try
            {
                var previousId = GetAll().OrderBy(x => x.userId).LastOrDefault() != null ? GetAll().OrderBy(x => x.userId).LastOrDefault().userId : 0;

                var newDoc = new UserAccount
                {
                    username = viewModel.username,
                    password = viewModel.password,
                    isAdmin = viewModel.isAdmin,
                    userId = previousId + 1,
                    isActive = true,

                };

                _accountCollection.InsertOne(newDoc);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            };
            
        }

        [HttpPost]
        public bool Update(UserAccountViewModel viewModel) 
        {
            try
            {
                var filter = Builders<UserAccount>.Filter.Eq(x => x.userId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<UserAccount>.Update
                    .Set(x => x.username, viewModel.username)
                    .Set(x => x.password, viewModel.password)
                    .Set(x => x.isAdmin, viewModel.isAdmin);

                _accountCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch(Exception e)
            {
                return false;
            }


        }
        [HttpPost]
        public bool Delete(UserAccountViewModel viewModel)
        {
            try
            {
                var filter = Builders<UserAccount>.Filter.Eq(x => x.userId, Convert.ToInt32(viewModel.Id));
                var updateSet = Builders<UserAccount>.Update
                    .Set(x => x.isActive, false);

                _accountCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        [HttpPost]
        public bool Restore(UserAccountViewModel viewModel)
        {
            try
            {
                var filter = Builders<UserAccount>.Filter.Eq(x => x.userId, viewModel.userId);
                var updateSet = Builders<UserAccount>.Update
                    .Set(x => x.isActive, true);

                _accountCollection.UpdateOne(filter, updateSet);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
