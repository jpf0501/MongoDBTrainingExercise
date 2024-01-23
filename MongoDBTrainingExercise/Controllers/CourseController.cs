using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Interface;
using MongoDBTrainingExercise.Models;
using MongoDBTrainingExercise.Models.ViewModels;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class CourseController : BaseController
    {
        private readonly Courses _courseService;
        private readonly Categories _categoryService;

        public CourseController(Courses courseService, Categories categoryService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var viewModel = _courseService.Get();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new CourseViewModel();
            viewModel.categoryList = _categoryService.GetAll();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CourseViewModel viewModel)
        {
            var course = _courseService.Create(viewModel);

            if (course)
            {
                TempData["PromptCreate"] = "Successfully created!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to create!";
            }

            //return View();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var viewModel = _courseService.GetById(id);
            viewModel.categoryList = _categoryService.GetAll();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CourseViewModel viewModel)
        {
            var course = _courseService.Update(viewModel);

            if (course)
            {
                TempData["PromptCreate"] = "Successfully updated!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to update!";
            }

            //return View();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var viewModel = _courseService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(CourseViewModel viewModel)
        {
            var course = _courseService.Delete(viewModel);

            if (course)
            {
                TempData["PromptCreate"] = "Successfully deleted!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to delete!";
            }

            //return View();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Restore()
        {
            var viewModel = _courseService.GetAllInactive();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Restore(int id)
        {
            var viewModel = _courseService.GetById(id);
            var student = _courseService.Restore(viewModel);

            if (student)
            {
                TempData["PromptCreate"] = "Successfully restored!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to restore!";
            }

            //return View();
            return RedirectToAction("Index");
        }
    }
}
