using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface IStudentService
    {
        IEnumerable<StudentViewModel> Get();
        Task Create(Student student);
        Task Update(Student student);
        Task Delete(string id);
    }
}
