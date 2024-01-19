using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModel> Get();
        IEnumerable<CourseViewModel> GetAll();
        CourseViewModel GetById(int id);
        bool Create(CourseViewModel viewModel);
        bool Update(CourseViewModel viewModel);
        bool Delete(CourseViewModel viewModel);
    }
}
