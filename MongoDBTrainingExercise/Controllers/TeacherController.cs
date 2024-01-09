using System;
using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class TeacherController : Controller
    {
        private readonly Teachers _teacherService;

        public TeacherController(Teachers teacherService)
        {
            _teacherService = teacherService;
        }

        public IActionResult Index()
        {
            var viewModel = _teacherService.Get();
            return View(viewModel);
        }
    }
}
