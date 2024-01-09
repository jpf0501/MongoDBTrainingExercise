namespace MongoDBTrainingExercise.Models.ViewModels
{
    public class TeacherViewModel : BaseViewModel
    {
        public string? Id { get; set; }

        public int teacherId { get; set; }

        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
    }
}
