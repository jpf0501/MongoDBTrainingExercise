using System;
using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class StudentController : Controller
    {
        private readonly Students _studentService;

        public StudentController(Students studentService)
        {
            _studentService = studentService;
        }

        public IActionResult Index()
        {
            var viewModel = _studentService.Get();
            return View(viewModel);
        }
    }
}
