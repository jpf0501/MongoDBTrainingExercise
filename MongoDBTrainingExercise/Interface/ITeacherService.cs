using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface ITeacherService
    {
        IEnumerable<TeacherViewModel> Get();
        IEnumerable<TeacherViewModel> GetAll();
        IEnumerable<TeacherViewModel> GetAllInactive();
        bool Create(TeacherViewModel viewModel);
        bool Update(TeacherViewModel viewModel);
        bool Delete(TeacherViewModel viewModel);
        bool Restore(TeacherViewModel viewModel);
    }
}
