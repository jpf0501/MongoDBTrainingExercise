using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface ITeacherService
    {
        IEnumerable<TeacherViewModel> Get();
        bool Create(TeacherViewModel viewModel);
        bool Update(TeacherViewModel viewModel);
        bool Delete(TeacherViewModel viewModel);
    }
}
