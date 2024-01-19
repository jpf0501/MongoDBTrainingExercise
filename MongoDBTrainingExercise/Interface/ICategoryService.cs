
using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface ICategoryService
    {
        IEnumerable<CategoryViewModel> Get();
        IEnumerable<CategoryViewModel> GetAll();
        string GetCategoryNameById(int id);
    }
}
