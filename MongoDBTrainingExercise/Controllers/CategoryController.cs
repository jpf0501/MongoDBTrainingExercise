using System;
using Microsoft.AspNetCore.Mvc;
using MongoDBTrainingExercise.Models.ViewModels;
using MongoDBTrainingExercise.Services;

namespace MongoDBTrainingExercise.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Categories _categoryService;

        public CategoryController(Categories categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var viewModel = _categoryService.Get();
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryViewModel viewModel)
        {
            var course = _categoryService.Create(viewModel);

            if(course)
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
            var viewModel = _categoryService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel viewModel)
        {
            var course = _categoryService.Update(viewModel);

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
            var viewModel = _categoryService.GetById(id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(CategoryViewModel viewModel)
        {
            var course = _categoryService.Delete(viewModel);

            if (course)
            {
                TempData["PromptCreate"] = "Successfully deleted!";
            }
            else
            {
                TempData["PromptCreate"] = "Failed to deleted!";
            }

            //return View();
            return RedirectToAction("Index");
        }
    }
}
