namespace MongoDBTrainingExercise.Models.ViewModels
{
    public class StudentViewModel
    {
        public string? Id { get; set; }

        public int studentId { get; set; }

        public string firstName { get; set; } = null!;
        public string lastName { get; set; } = null!;
        public int age { get; set; }
        public string address { get; set; } = null!;
    }
}
