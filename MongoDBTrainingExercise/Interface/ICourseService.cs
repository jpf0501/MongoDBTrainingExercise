using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;


namespace MongoDBTrainingExercise.Interface
{
    public interface ICourseService
    {
        IEnumerable<CourseViewModel> Get();
        Task Create(Student student);
        Task Update(Student student);
        Task Delete(string id);
    }
}
