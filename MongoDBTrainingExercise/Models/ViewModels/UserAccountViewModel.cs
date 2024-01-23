namespace MongoDBTrainingExercise.Models.ViewModels
{
    public class UserAccountViewModel : BaseViewModel
    {
        public string? Id { get; set; }

        public int userId { get; set; }

        public string username { get; set; } = null!;
        public string password { get; set; } = null!;
        public string passwordNew { get; set; } = null!;
        public string passwordUpdate { get; set; } = null!;
        public string oldPassword { get; set; } = null!;

        public bool isPasswordUpdate { get; set; }

        public bool isAdmin { get; set; }
    }
}
