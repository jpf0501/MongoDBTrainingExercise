using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class CourseController : Controller
    {
        private readonly Courses _courseService;

        public CourseController(Courses courseService)
        {
            _courseService = courseService;
        }
        public IActionResult Index()
        {
            var viewModel = _courseService.Get();
            return View(viewModel);
        }
    }
}
