namespace MongoDBTrainingExercise.Models.ViewModels
{
    public class CategoryViewModel : BaseModel
    {
        public string? Id { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; } = null!;
    }
}
