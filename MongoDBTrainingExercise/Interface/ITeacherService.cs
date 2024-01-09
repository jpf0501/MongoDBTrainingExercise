using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface ITeacherService
    {
        IEnumerable<TeacherViewModel> Get();
        Task Create(Student student);
        Task Update(Student student);
        Task Delete(string id);
    }
}
