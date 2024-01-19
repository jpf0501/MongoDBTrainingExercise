
using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> Get();
        IEnumerable<CategoryViewModel> GetAll();
        IEnumerable<CategoryViewModel> GetAllInactive();
        string GetCategoryNameById(int id);
        CategoryViewModel GetById(int id);
        bool Create(CategoryViewModel viewModel);
        bool Update(CategoryViewModel viewModel);
        bool Delete(CategoryViewModel viewModel);
        bool Restore(CategoryViewModel viewModel);
    }
}
