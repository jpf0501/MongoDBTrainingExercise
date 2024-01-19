using MongoDBTrainingExercise.Models;

namespace MongoDBTrainingExercise.Models.ViewModels
{
    public class CourseViewModel : BaseViewModel
    {
        public string? Id { get; set; }
        public int courseId { get; set; }
        public string name { get; set; } = null!;
        public int categoryId { get; set; }
        public IEnumerable<CategoryViewModel> categoryList { get; set; } = null!;
        public string categoryName { get; set; } = null!;
    }
}
