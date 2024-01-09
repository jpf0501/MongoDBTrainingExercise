namespace MongoDBTrainingExercise.Models.ViewModels
{
    public class CourseViewModel
    {
        public string? Id { get; set; }
        public int courseId { get; set; }
        public string name { get; set; } = null!;
        public string category { get; set; } = null!;
    }
}
