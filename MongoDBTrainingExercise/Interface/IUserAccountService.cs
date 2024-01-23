using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface IUserAccountService
    {
        IEnumerable<UserAccountViewModel> Get();
        IEnumerable<UserAccountViewModel> GetAll();
        IEnumerable<UserAccountViewModel> GetAllInactive();

        UserAccountViewModel GetById(int id);
        UserAccountViewModel GetByUsername(string username, string password);
        bool Create(UserAccountViewModel viewModel);
        bool Update(UserAccountViewModel viewModel);
        bool Delete(UserAccountViewModel viewModel);
        bool Restore(UserAccountViewModel viewModel);
    }
}
